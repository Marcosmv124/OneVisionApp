using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using One_Vision.Models; // Para que reconozca SendGridSettings
using Microsoft.AspNetCore.Hosting;

namespace One_Vision.Services
{
    public class EmailService
    {
        private readonly SendGridSettings _settings;

        public EmailService(IOptions<SendGridSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task EnviarCorreoConfirmacion(string destino, string nombre, string token)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var to = new EmailAddress(destino, nombre);
            var asunto = "Confirma tu cuenta - One Vision";

            var url = $"{_settings.BaseUrl}/api/auth/confirmar?token={token}";
            var html = $@"
            <p>Hola {nombre},</p>
            <p>Gracias por registrarte. Confirma tu cuenta haciendo clic en el siguiente enlace:</p>
            <a href='{url}'>{url}</a>";

            var msg = MailHelper.CreateSingleEmail(from, to, asunto, "", html);
            await client.SendEmailAsync(msg);
        }

        public async Task EnviarCorreoRecuperacion(string destino, string nombre, string token)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var to = new EmailAddress(destino, nombre);
            var asunto = "Recuperación de Contraseña - One Vision";

            var url = $"{_settings.BaseUrl}/Usuarios/ResetPassword?token={token}";
            var html = $@"
            <p>Hola {nombre},</p>
            <p>Recibimos una solicitud para restablecer tu contraseña. Haz clic en el siguiente enlace:</p>
            <a href='{url}'>{url}</a>
            <p>Si no solicitaste esto, ignora este correo.</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, asunto, "", html);
            await client.SendEmailAsync(msg);
        }

        public async Task EnviarReciboPorCorreo(string destino, string nombre, byte[] pdfBytes, int idVenta)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var to = new EmailAddress(destino, nombre);
            var asunto = $"Recibo de compra - Folio #{idVenta}";

            var htmlContenido = $@"
            <p>Hola {nombre},</p>
            <p>Gracias por tu compra en Óptica One Vision.</p>
            <p>Adjuntamos el recibo en formato PDF.</p>
            <p>Folio: {idVenta}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, asunto, "", htmlContenido);
            msg.AddAttachment($"Recibo_{idVenta}.pdf", Convert.ToBase64String(pdfBytes), "application/pdf");

            await client.SendEmailAsync(msg);
        }
    }
}
