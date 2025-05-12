using SendGrid; 
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using One_Vision.Models; // Para que reconozca SendGridSettings
using Microsoft.AspNetCore.Hosting;


namespace One_Vision.Services{
public class EmailService
{
    private readonly SendGridSettings _settings;
    private readonly IWebHostEnvironment _env;

        public EmailService(IOptions<SendGridSettings> settings, IWebHostEnvironment env)
        {
            _settings = settings.Value;
            _env = env;
        }

    public async Task EnviarCorreoConfirmacion(string destino, string nombre, string token)
    {
        var client = new SendGridClient(_settings.ApiKey);
        var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
        var to = new EmailAddress(destino, nombre);
        var asunto = "Confirma tu cuenta - One Vision";
        var baseUrl = _env.IsDevelopment() ? _settings.LocalBaseUrl : _settings.ProdBaseUrl;
            var url = $"{baseUrl}/api/auth/confirmar?token={token}";
        var html = $@"
            <p>Hola {nombre},</p>
            <p>Gracias por registrarte. Confirma tu cuenta haciendo clic en el siguiente enlace:</p>
            <a href='{url}'>{url}</a>";

        var msg = MailHelper.CreateSingleEmail(from, to, asunto, "", html);
        await client.SendEmailAsync(msg);
    }
}

}

