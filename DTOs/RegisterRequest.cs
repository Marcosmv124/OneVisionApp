namespace One_Vision.DTOs{
public class RegisterRequest
{
    public string Nombre { get; set; }
    public string UsuarioNombre { get; set; }
    public string Correo { get; set; }
    public string Password { get; set; }
    public RolUsuario Rango { get; set; }
}
}
