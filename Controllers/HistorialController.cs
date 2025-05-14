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
        private readonly OneVisionDbContext _context;

        public HistorialController(OneVisionDbContext context)
        {
            _context = context;
        }
        [Authorize]
        //public async Task<IActionResult> Index(int paginaPacientes = 1, int paginaProductos = 1 )
        //{
        //    int tamanioPagina = 5;
        //    var pacientesQuery = _context.Pacientes.OrderBy(p => p.ID); // o como ordenes normalmente
        //    var productosQuery = _context.Productos.OrderBy(p => p.CodigoDeBarra);

        //    var viewModel = new PacienteProductoViewModel
        //    {
        //        //Pacientes = _context.Pacientes.ToList(),
        //        //Productos = _context.Productos.ToList()
        //        Pacientes = await Paginacion<Paciente>.CrearAsync(pacientesQuery, paginaPacientes, tamanioPagina),
        //        Productos = await Paginacion<Producto>.CrearAsync(productosQuery, paginaProductos, tamanioPagina)
        //    };

        //    return View(viewModel);
        //}
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

        //[Authorize]
        //public IActionResult Index()

        //{ 
        //        var viewModel = new PacienteProductoViewModel
        //        {
        //            Pacientes = _context.Pacientes.ToList(),
        //            Productos = _context.Productos.ToList()
        //        };

        //        return View(viewModel);

        //}

        [Authorize]
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

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [Authorize]
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
        [Authorize]
        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [Authorize]
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
        [Authorize]
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateTest()
        {
            return View();
        }
    }
}
