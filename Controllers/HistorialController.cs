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
        public async Task<IActionResult> Index(string buscadorPacientes, int paginaPacientes = 1)
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

            var viewModel = new PacienteProductoViewModel
            {
                Pacientes = pacientes
            };

            return View("Index", viewModel);
        }


        [Authorize(Roles = "Doctor, Administrador")]
        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int id)
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
         [Authorize(Roles = "Doctor, Administrador")]
        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [Authorize(Roles = "Doctor, Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        {
            // ⚠️ Ignora validaciones — solo con fines de prueba
            _context.Add(paciente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
          [Authorize(Roles = "Doctor, Administrador")]
        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int id)
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

         [Authorize(Roles = "Doctor, Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        {
            if (id != paciente.ID)
            {
                return NotFound(); // Verificación básica de consistencia
            }

            try
            {
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(paciente.ID))
                {
                    return NotFound();
                }
                throw; // Relanza la excepción si no es un error de concurrencia
            }
        }
          [Authorize(Roles = "Doctor, Administrador")]
        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int id)
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
         [Authorize(Roles = "Doctor, Administrador")]
        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }

        [Authorize(Roles = "Doctor, Administrador")]
        public IActionResult Index()
        {
            return View();
        }
          [Authorize(Roles = "Doctor, Administrador")]
        [HttpGet]
        public IActionResult CreateTest(int id)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            var model = new Historial
            {
                PacienteID = paciente.ID,
                Paciente = paciente // Esto es solo para mostrar el nombre en la vista si quieres
            };

            return View(model);
        }



          [Authorize(Roles = "Doctor, Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTest(Historial model,
    string[] AntecedenteFamiliar,
    string[] AntecedentePersonal,
    string[] CVPC_OD, string[] CVPC_OI,
    string[] RA_OD, string[] RA_OI,
    string[] H_OD, string[] H_OI)
        {
            // Validar que el paciente exista
            if (!_context.Pacientes.Any(p => p.ID == model.PacienteID))
            {
                return BadRequest("Paciente no encontrado.");
            }

            // Unir los checkbox múltiples como cadenas
            model.Antecedente_Familiar = AntecedenteFamiliar != null ? string.Join(", ", AntecedenteFamiliar) : null;
            model.Antecedente_Personal = AntecedentePersonal != null ? string.Join(", ", AntecedentePersonal) : null;
            model.CVPC_OD = CVPC_OD != null ? string.Join(", ", CVPC_OD) : null;
            model.CVPC_OI = CVPC_OI != null ? string.Join(", ", CVPC_OI) : null;
            model.RA_OD = RA_OD != null ? string.Join(", ", RA_OD) : null;
            model.RA_OI = RA_OI != null ? string.Join(", ", RA_OI) : null;
            model.H_OD = H_OD != null ? string.Join(", ", H_OD) : null;
            model.H_OI = H_OI != null ? string.Join(", ", H_OI) : null;
            
            // Prevenir errores de IDENTITY_INSERT
            model.ID = 0;

            // Guardar en base de datos
            _context.Historiales.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Doctor, Administrador")]
        public IActionResult ListaHistorial(int id)
        {
            // Obtener los historiales del paciente con ese ID
            var historial = _context.Historiales
                .Where(h => h.PacienteID == id)
                .OrderByDescending(h => h.FECHA)
                .ToList();

            // Verificar que el paciente exista
            var paciente = _context.Pacientes.FirstOrDefault(p => p.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            // Pasar el nombre del paciente a la vista (opcional)
            ViewBag.NombrePaciente = paciente.Nombre;

            // Mostrar la vista con la lista de historiales
            return View(historial);
        }
        [Authorize(Roles = "Administrador, Doctor")]
        [HttpGet("Historial/DetailsExamen/{id}")] // Ruta explícita para que funcione bien la URL
        public IActionResult DetailsExamen(int id)
        {
            var examen = _context.Historiales.FirstOrDefault(h => h.ID == id);

            if (examen == null)
            {
                return NotFound();
            }

            return View("DetailsExamen", examen);
        }



    }
}
