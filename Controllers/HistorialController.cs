using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using One_Vision.Models;
using One_Vision.Utils;
using System.Text.Json;

namespace One_Vision.Controllers
{
    public class HistorialController : Controller
    {
        //Historial Controller
        private readonly OneVisionDbContext _context;

        public HistorialController(OneVisionDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Doctor, Administrador")]
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

        [Authorize(Roles = "Administrador")]
        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }
     
        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
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
        [Authorize(Roles = "Administrador")]
        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(string id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult CreateTest()
        {
            return View();
        }
    }
}
