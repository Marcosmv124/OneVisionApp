using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_Vision.Models
{
    public class Venta
    {
        [Key]
        public int ID_Venta { get; set; }

        [Required]
        [StringLength(20)]
        public string ID_Paciente { get; set; }

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

        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
