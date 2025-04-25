using System.ComponentModel.DataAnnotations;

namespace One_Vision.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioNombre { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        public int Rango { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }
    }
}
