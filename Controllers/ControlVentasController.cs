using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using One_Vision.Models;
using System.Linq;
using System.Threading.Tasks;

namespace One_Vision.Controllers
{
    [Authorize]
    public class ControlVentasController : Controller
    {
        private readonly OneVisionDbContext _context;

        public ControlVentasController(OneVisionDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Paciente)
                .Include(v => v.Usuario)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            return View(ventas);
        }
        [Authorize]
        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Paciente)
                .Include(v => v.Usuario)
                .Include(v => v.VentaProductos)
                    .ThenInclude(vp => vp.Producto)
                .FirstOrDefaultAsync(m => m.ID_Venta == id);

            if (venta == null)
                return NotFound();

            return View(venta);
        }
    }
}
