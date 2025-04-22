using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using One_Vision.Models;


namespace One_Vision.Controllers
{
    public class PuntoVentaController : Controller
    {

        // Método que devuelve la lista base
        private List<Paciente> ObtenerPacientesBase()
        {
            return new List<Paciente>
            {
              new Paciente { Id = 1, Nombre = "Juan Pérez", Edad = 35, Telefono = 1234567890, Correo = "juan@example.com", Direccion = "Esperanza Iris" },
              new Paciente { Id = 2, Nombre = "Ana López", Edad = 28, Telefono = 0987654321, Correo = "ana@example.com", Direccion = "Esperanza Iris" },
              new Paciente { Id = 3, Nombre = "Carlos Gómez", Edad = 40, Telefono = 555555555, Correo = "carlos@example.com", Direccion = "Esperanza Iris" }
           };
        }
        // Esto es para pacientes
        public IActionResult Index()
        {

            var pacientes = ObtenerPacientesBase();
            var productos = ObtenerProductosBase(); 
            Add(pacientes);
            AddProduct(productos);

            var viewModel = new PacienteProductoViewModel
            {
                Pacientes = pacientes,
                Productos = productos
            };

            return View(viewModel); 
        }
        public void Add(List<Paciente> pacientes)
        {
            // Si hay un nuevo paciente en TempData, lo agregamos a la lista
            if (TempData["NuevoPaciente"] is string json && !string.IsNullOrEmpty(json))
            {
                var nuevoPaciente = JsonSerializer.Deserialize<Paciente>(json);
                if (nuevoPaciente != null)
                {
                    nuevoPaciente.Id = pacientes.Max(p => p.Id) + 1;
                    pacientes.Add(nuevoPaciente);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                // Guardamos temporalmente el nuevo paciente como JSON
                TempData["NuevoPaciente"] = JsonSerializer.Serialize(paciente);
            }

            return View(paciente);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pacientes = new List<Paciente>
            {
              new Paciente { Id = 1, Nombre = "Juan Pérez", Edad = 35, Telefono = 1234567890, Correo = "juan@example.com", Direccion = "Esperanza Iris" },
              new Paciente { Id = 2, Nombre = "Ana López", Edad = 28, Telefono = 987654321, Correo = "ana@example.com", Direccion = "Esperanza Iris" },
              new Paciente { Id = 3, Nombre = "Carlos Gómez", Edad = 40, Telefono = 555555555, Correo = "carlos@example.com", Direccion = "Esperanza Iris" }
           };

            var paciente = pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        [HttpPost]
        public IActionResult Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría la lógica de actualización real
                TempData["Mensaje"] = $"Paciente {paciente.Nombre} editado correctamente.";
                return RedirectToAction("Index");
            }

            return View(paciente); // Si hay errores, vuelve a mostrar el formulario
        }

        ////////////////////////////////////////////////////////////////////
        //Productos
        private List<Producto> ObtenerProductosBase()
        {
            return new List<Producto>
            {
              new Producto { ID_Producto = 1, Nombre_Producto = "Juan Pérez", Precio = 35, Descripción_Producto = "Esperanza Iris" },
              new Producto { ID_Producto = 2, Nombre_Producto = "Ana López", Precio = 28, Descripción_Producto = "Esperanza Iris" },
              new Producto { ID_Producto = 3, Nombre_Producto = "Carlos Gómez", Precio = 40, Descripción_Producto = "Esperanza Iris" }
           };
        }
        public void AddProduct(List<Producto> productos)
        {
            
            if (TempData["NuevoProducto"] is string json && !string.IsNullOrEmpty(json))
            {
                var nuevoProducto = JsonSerializer.Deserialize<Producto>(json);
                if (nuevoProducto != null)
                {
                    nuevoProducto.ID_Producto = productos.Max(p => p.ID_Producto) + 1;
                    productos.Add(nuevoProducto);
                }
            }
        }


    }
}
