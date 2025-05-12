using Microsoft.AspNetCore.Mvc;
using One_Vision.Models;
using System.Text.Json;

namespace One_Vision.Controllers
{
    public class HistorialController : Controller
    {
        //public List<Paciente> ObtenerPacientesBase()
        //{
        //    return new List<Paciente>
        //    {
        //      //new Paciente { Id = 1, Nombre = "Juan Pérez", Edad = 35, Telefono = 1234567890, Correo = "juan@example.com", Direccion = "Esperanza Iris" },
        //      //new Paciente { Id = 2, Nombre = "Ana López", Edad = 28, Telefono = 0987654321, Correo = "ana@example.com", Direccion = "Esperanza Iris" },
        //      //new Paciente { Id = 3, Nombre = "Carlos Gómez", Edad = 40, Telefono = 555555555, Correo = "carlos@example.com", Direccion = "Esperanza Iris" }
        //   };
        //}
        //// Esto es para pacientes
        //public IActionResult Index()
        //{

        //    var pacientes = ObtenerPacientesBase();
        //    var examenes = ObtenerExamenesBase(); 
        //    Add(pacientes);
        //    AddExamen(examenes); 
           

        //    var viewModel = new PacienteProductoViewModel
        //    {
        //        Pacientes = pacientes,
        //        Examenes = examenes
        //    };

        //    return View(viewModel);
        //}
        //public void Add(List<Paciente> pacientes)
        //{
        //    // Si hay un nuevo paciente en TempData, lo agregamos a la lista
        //    if (TempData["NuevoPaciente"] is string json && !string.IsNullOrEmpty(json))
        //    {
        //        var nuevoPaciente = JsonSerializer.Deserialize<Paciente>(json);
        //        if (nuevoPaciente != null)
        //        {
        //            //nuevoPaciente.Id = pacientes.Max(p => p.Id) + 1;
        //            pacientes.Add(nuevoPaciente);
        //        }
        //    }
        //}

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var pacientes = new List<Paciente>
        //    {
        //      //new Paciente { Id = 1, Nombre = "Juan Pérez", Edad = 35, Telefono = 1234567890, Correo = "juan@example.com", Direccion = "Esperanza Iris" },
        //      //new Paciente { Id = 2, Nombre = "Ana López", Edad = 28, Telefono = 987654321, Correo = "ana@example.com", Direccion = "Esperanza Iris" },
        //      //new Paciente { Id = 3, Nombre = "Carlos Gómez", Edad = 40, Telefono = 555555555, Correo = "carlos@example.com", Direccion = "Esperanza Iris" }
        //   };

        //    //var paciente = pacientes.FirstOrDefault(p => p.Id == id);
        //    //if (paciente == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Edit(Paciente paciente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Aquí iría la lógica de actualización real
        //        TempData["Mensaje"] = $"Paciente {paciente.Nombre} editado correctamente.";
        //        return RedirectToAction("Index");
        //    }

        //    return View(paciente); // Si hay errores, vuelve a mostrar el formulario
        //}

        //////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        ////Examen
        //private List<Examen> ObtenerExamenesBase()
        //{
        //    return new List<Examen>
        //    {
        //      new Examen { Id = 1, Fecha = "Juan Pérez", Resultados = "Esperanza Iris"},
        //      new Examen { Id = 2, Fecha = "Ana López",   Resultados = "Esperanza Iris"},
        //      new Examen { Id = 3, Fecha = "Ana López",   Resultados = "Esperanza Iris"}
        //   };
        //}
        //public void AddExamen(List<Examen> examenes)
        //{

        //    if (TempData["CrearExamen"] is string json && !string.IsNullOrEmpty(json))
        //    {
        //        var nuevoExamen = JsonSerializer.Deserialize<Examen>(json);
        //        if (nuevoExamen != null)
        //        {
        //            nuevoExamen.Id = examenes.Max(p => p.Id) + 1;
        //            examenes.Add(nuevoExamen);
        //        }
        //    }
        //}
    }
}
