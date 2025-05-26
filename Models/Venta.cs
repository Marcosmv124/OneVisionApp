using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_Vision.Models
{
    public class Venta
    {
        [Key]
        public int ID_Venta { get; set; }

        [Required]
        public int ID_Paciente { get; set; } // CAMBIO: de string a int

         [Required]
        public int ID_Usuario { get; set; } // NUEVO: Usuario que realiza la venta

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Abonado { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [ForeignKey("ID_Paciente")]
        public Paciente Paciente { get; set; }

         [ForeignKey("ID_Usuario")]
        public Usuario Usuario { get; set; } // Relación con Usuario

        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
