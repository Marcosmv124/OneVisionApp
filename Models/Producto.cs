using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_Vision.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoDeBarra { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioDeVenta { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioDeCompra { get; set; }

        [Required]
        public int Existencia { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Proveedor { get; set; }

        [StringLength(50)]
        public string Moda { get; set; }

        [StringLength(50)]
        public string Diseño { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
