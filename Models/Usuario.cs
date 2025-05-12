namespace One_Vision.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string UsuarioNombre { get; set; }
        public string Correo { get; set; }
        public string PasswordHash { get; set; }
        public RolUsuario Rango { get; set; }
        public bool EmailConfirmado { get; set; } = false;
        public string? TokenVerificacion { get; set; }
    }

}
