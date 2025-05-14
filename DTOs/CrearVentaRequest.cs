// DTO para la creación de la venta
namespace One_Vision.DTOs{
   public class CrearVentaRequest
   {
      public string ID_Paciente { get; set; }
      public List<ProductoVentaDto> Productos { get; set; }
      public decimal Abonado { get; set; }
   }
}
