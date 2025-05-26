namespace One_Vision.DTOs
{
    public class RecuperarPasswordRequest
    {
        public string Correo { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string NuevaPassword { get; set; }
    }
}
