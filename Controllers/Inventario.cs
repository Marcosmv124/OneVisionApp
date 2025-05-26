using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using One_Vision.Models;
using One_Vision.Utils;

namespace One_Vision.Controllers
{
    public class Inventario : Controller
    {

        private readonly OneVisionDbContext _context;

        public Inventario(OneVisionDbContext context)
        {
            _context = context;
        }
        // GET: Inventario

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(int paginaPacientes = 1, int paginaProductos = 1)
        {
            int tamanioPagina = 5;
           // var pacientesQuery = _context.Pacientes.OrderBy(p => p.ID); // o como ordenes normalmente
            var productosQuery = _context.Productos.OrderBy(p => p.CodigoDeBarra);
            var productos = await Paginacion<Producto>.CrearAsync(productosQuery, paginaProductos, tamanioPagina);
            return View(productos); // ✔️ este es del tipo correcto
        }
          [Authorize(Roles = "Administrador")]
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.CodigoDeBarra == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        [Authorize(Roles = "Administrador")]
        // GET: Inventario/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            // Primero verificamos si el código ya existe en la base de datos
            if (_context.Productos.Any(p => p.CodigoDeBarra == producto.CodigoDeBarra))
            {
                ModelState.AddModelError("CodigoDeBarra", "Ya existe un producto con este código de barra.");
            }

            // Luego validamos el modelo completo
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores, regresamos la vista con los mensajes
            return View(producto);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoDeBarra,Nombre,PrecioDeVenta,PrecioDeCompra,Existencia,Categoria,Proveedor,Moda,Diseño,Color,Fecha")] Producto producto)
        {
            if (id != producto.CodigoDeBarra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.CodigoDeBarra))
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
            return View(producto);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.CodigoDeBarra == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.CodigoDeBarra == id);
        }
    }
}
