using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_Vision.Models
{
    public class VentaProducto
    {
        [Key]
        public int ID_VentaProducto { get; set; }

        [Required]
        public int ID_Venta { get; set; }

        [Required]
        public int CodigoDeBarra { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [ForeignKey("ID_Venta")]
        public Venta Venta { get; set; }

        [ForeignKey("CodigoDeBarra")]
        public Producto Producto { get; set; }
    }
}