using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
        public async Task<IActionResult> Index(int paginaPacientes = 1, int paginaProductos = 1)
        {
            int tamanioPagina = 5;
           // var pacientesQuery = _context.Pacientes.OrderBy(p => p.ID); // o como ordenes normalmente
            var productosQuery = _context.Productos.OrderBy(p => p.CodigoDeBarra);
            var productos = await Paginacion<Producto>.CrearAsync(productosQuery, paginaProductos, tamanioPagina);
            return View(productos); // ✔️ este es del tipo correcto
        }
        [Authorize]
        // GET: Inventario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: Inventario/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
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


        // GET: Inventario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize]
        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        // GET: Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize]
        // POST: Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
