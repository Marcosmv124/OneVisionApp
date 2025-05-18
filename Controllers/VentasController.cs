/*using Microsoft.AspNetCore.Mvc;
using One_Vision.Models;
using One_Vision.DTOs;

namespace One_Vision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly OneVisionDbContext _context;

        public VentasController(OneVisionDbContext context)
        {
            _context = context;
        }

        [HttpPost("CrearVenta")]
        public async Task<IActionResult> CrearVenta([FromBody] CrearVentaRequest request)
        {
            if (string.IsNullOrEmpty(request.ID_Paciente) || request.Productos == null || !request.Productos.Any())
                return BadRequest("Datos incompletos");

            // Crear la venta
            var venta = new Venta
            {
                ID_Paciente = request.ID_Paciente,
                Total = request.Productos.Sum(p => p.Precio * p.Cantidad),
                Abonado = request.Abonado,
                Fecha = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            // Guardar los productos en la venta
            foreach (var prod in request.Productos)
            {
                _context.VentaProductos.Add(new VentaProducto
                {
                    ID_Venta = venta.ID_Venta,
                    CodigoDeBarra = prod.CodigoDeBarra,
                    Cantidad = prod.Cantidad
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new {venta.Total, venta.Abonado, Adeudo = venta.Total - venta.Abonado });
        }
    }

}*/