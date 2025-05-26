using  One_Vision.DTOs; 
namespace One_Vision.Models
{
    public class VentaViewModel
    {
       public int ID_Paciente { get; set; }

        public decimal PrecioTotal { get; set; }

        public decimal Anticipo { get; set; }

        public bool AnticipoEnDolar { get; set; }

        public decimal ValorDolar { get; set; }

        public List<VentaProductoDTO> Productos { get; set; }
    }

}