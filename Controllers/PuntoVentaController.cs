using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using One_Vision.Models;
using One_Vision.PDF;
using One_Vision.Services; 
using One_Vision.DTOs;
using System.Collections.Generic;
using System.Linq;
using Rotativa.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using One_Vision.Utils;
using QuestPDF.Fluent;

namespace One_Vision.Controllers
{
    public class PuntoVentaController : Controller
    {

        private readonly OneVisionDbContext _context;
        private readonly EmailService _emailServices;

        public PuntoVentaController(OneVisionDbContext context, EmailService emailServices)
        {
            _emailServices = emailServices;
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string buscadorPacientes, string buscadorProductos, int paginaPacientes = 1, int paginaProductos = 1)
        {
            int tamanioPagina = 5;

            // Filtrado de pacientes
            var pacientesQuery = _context.Pacientes.AsQueryable();
            if (!string.IsNullOrEmpty(buscadorPacientes))
            {
                pacientesQuery = pacientesQuery
                    .Where(p => p.Nombre.Contains(buscadorPacientes));
            }

            pacientesQuery = pacientesQuery.OrderBy(p => p.ID);
            var pacientes = await Paginacion<Paciente>.CrearAsync(pacientesQuery, paginaPacientes, tamanioPagina);

            // Filtrado de productos
            var productosQuery = _context.Productos.AsQueryable();
            if (!string.IsNullOrEmpty(buscadorProductos))
            {
                productosQuery = productosQuery
                    .Where(p => p.Nombre.Contains(buscadorProductos) || p.CodigoDeBarra.ToString().Contains(buscadorProductos));
            }

            productosQuery = productosQuery.OrderBy(p => p.CodigoDeBarra);
            var productos = await Paginacion<Producto>.CrearAsync(productosQuery, paginaProductos, tamanioPagina);

            var viewModel = new PacienteProductoViewModel
            {
                Pacientes = pacientes,
                Productos = productos
            };

            return View("Index", viewModel);
        }
        [Authorize]
        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        {
            // ⚠️ Ignora validaciones — solo con fines de prueba
            _context.Add(paciente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        [Authorize]
        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        {
            if (id != paciente.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        [Authorize]
        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [Authorize]
        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> FinalizarVenta(VentaViewModel model)
        {
            if (model.ID_Paciente <= 0 || model.Productos == null || model.Productos.Count == 0)
            {
                TempData["Error"] = "Faltan datos para finalizar la venta.";
                return RedirectToAction("Index");
            }

            // ✅ Obtener el ID del usuario autenticado desde las claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "ID");
            if (userIdClaim == null)
            {
                TempData["Error"] = "No se pudo identificar al usuario.";
                return RedirectToAction("Index");
            }
            int userId = int.Parse(userIdClaim.Value);

            var paciente = await _context.Pacientes.FindAsync(model.ID_Paciente);
            if (paciente == null)
            {
                TempData["Error"] = "Paciente no encontrado.";
                return RedirectToAction("Index");
            }

            var anticipoAbonado = model.AnticipoEnDolar ? model.Anticipo * model.ValorDolar : model.Anticipo;

            // ✅ Asignar ID_Usuario al crear la venta
            var venta = new Venta
            {
                ID_Paciente = model.ID_Paciente,
                ID_Usuario = userId, // ✅ Aquí se asigna el usuario autenticado
                Abonado = anticipoAbonado,
                Total = model.PrecioTotal,
                Fecha = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync(); // Para obtener ID_Venta

            foreach (var item in model.Productos)
            {
                var producto = await _context.Productos.FindAsync(item.CodigoDeBarra);
                if (producto == null || producto.Existencia < item.Cantidad)
                {
                    TempData["Error"] = $"Error con el producto: {item.CodigoDeBarra}";
                    return RedirectToAction("Index");
                }

                producto.Existencia -= item.Cantidad;

                _context.VentaProductos.Add(new VentaProducto
                {
                    ID_Venta = venta.ID_Venta,
                    CodigoDeBarra = item.CodigoDeBarra,
                    Cantidad = item.Cantidad
                });
            }

            await _context.SaveChangesAsync();
            TempData["UltimaVentaId"] = venta.ID_Venta;
            TempData["MostrarRecibo"] = true;
            TempData["ID_Venta"] = venta.ID_Venta;
            TempData["ID_Paciente"] = venta.ID_Paciente;
            TempData["Total"] = venta.Total.ToString("0.00");
            TempData["Abonado"] = venta.Abonado.ToString("0.00");
            TempData["Fecha"] = venta.Fecha.ToString("dd/MM/yyyy");

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> GenerarRecibo(int idVenta)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentaProductos)
                    .ThenInclude(vp => vp.Producto)
                .Include(v => v.Paciente)  // ✅ para datos del paciente
                .Include(v => v.Usuario)   // ✅ para datos del usuario
                .FirstOrDefaultAsync(v => v.ID_Venta == idVenta);

            if (venta == null)
                return NotFound();

            var model = new VentaReciboViewModel
            {
                ID_Venta = venta.ID_Venta,
                ID_Paciente = venta.ID_Paciente,
                Total = venta.Total,
                Abonado = venta.Abonado,
                Fecha = venta.Fecha,

                // ✅ Nuevos campos
                NombrePaciente = venta.Paciente?.Nombre,
                TelefonoPaciente = venta.Paciente?.Telefono,
                DireccionPaciente = venta.Paciente?.Direccion,
                NombreUsuario = venta.Usuario?.Nombre,

                Productos = venta.VentaProductos.Select(vp => new VentaReciboViewModel.VentaProductoDetalle
                {
                    Nombre = vp.Producto.Nombre,
                    Cantidad = vp.Cantidad,
                    Precio = vp.Producto.PrecioDeVenta
                }).ToList()
            };

            var document = new ReciboPdf { Datos = model };
            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", $"Recibo_{venta.ID_Venta}.pdf");
        }

        ////// CORREOS
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EnviarReciboPorCorreo(int idVenta)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentaProductos).ThenInclude(vp => vp.Producto)
                .Include(v => v.Paciente)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.ID_Venta == idVenta);

            if (venta == null || venta.Paciente == null || string.IsNullOrWhiteSpace(venta.Paciente.Correo))
            {
                TempData["Error"] = "No se pudo enviar el recibo. El paciente no tiene correo.";
                return RedirectToAction("Index");
            }

            var model = new VentaReciboViewModel
            {
                ID_Venta = venta.ID_Venta,
                ID_Paciente = venta.ID_Paciente,
                Total = venta.Total,
                Abonado = venta.Abonado,
                Fecha = venta.Fecha,
                NombrePaciente = venta.Paciente.Nombre,
                TelefonoPaciente = venta.Paciente.Telefono,
                DireccionPaciente = venta.Paciente.Direccion,
                NombreUsuario = venta.Usuario?.Nombre,
                Productos = venta.VentaProductos.Select(vp => new VentaReciboViewModel.VentaProductoDetalle
                {
                    Nombre = vp.Producto.Nombre,
                    Cantidad = vp.Cantidad,
                    Precio = vp.Producto.PrecioDeVenta
                }).ToList()
            };

            var pdf = new ReciboPdf { Datos = model };
            var pdfBytes = pdf.GeneratePdf();

            await _emailServices.EnviarReciboPorCorreo(venta.Paciente.Correo, venta.Paciente.Nombre, pdfBytes, venta.ID_Venta);

            TempData["Exito"] = "Recibo enviado al correo del paciente.";
            return RedirectToAction("Index");
        }

    }
}
