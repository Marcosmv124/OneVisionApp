using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_Vision.Models
{
    public class Paciente
    {
        [Key]
        public int ID { get; set; }  // Auto-incrementable

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        // Relación uno a muchos: Un paciente puede tener muchos exámenes completos
        public virtual ICollection<Historial> Historiales { get; set; }
    }

}
