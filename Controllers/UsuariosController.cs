using Microsoft.AspNetCore.Mvc;
using One_Vision.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text;
using One_Vision.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using One_Vision.Services;

namespace One_Vision.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly OneVisionDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly SendGridSettings _settings;
        private readonly EmailService _emailService;
        public UsuariosController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env, IOptions<SendGridSettings> settings, OneVisionDbContext context, EmailService emailService)
        {
           
            _httpClientFactory = httpClientFactory;
            _env = env;
            _settings = settings.Value;
            _context = context;
            _emailService = emailService;
        }
        private string GetBaseUrl()
        {
            return _env.IsDevelopment() ? _settings.LocalBaseUrl : _settings.ProdBaseUrl;
        }
        // GET: Usuarios/Login
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        // POST: Usuarios/Login (Usando API)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginRequest login)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{GetBaseUrl()}/api/auth/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonData);

                var token = jsonDoc.RootElement.GetProperty("token").GetString();

                ////////////////////Se busca el usuario para obtener el rol
                var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioNombre == login.UsuarioNombre);
                if (usuario == null)
                {
                    ModelState.AddModelError("", "Usuario no encontrado.");
                    return View(login);
                }

                // Crear claims básicos (puedes enriquecerlos si quieres agregar más info desde tu API)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UsuarioNombre),
                    new Claim("JWT", token), // Guardamos el token por si lo ocupamos después para llamadas API desde la vista
                    new Claim(ClaimTypes.Role, usuario.Rango.ToString()), 

                      // ✅ Agrega esta línea para que se incluya el ID del usuario
                      new Claim("ID", usuario.ID.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos o correo no confirmado.");
            return View(login);
        }

        // GET: Usuarios/Create (Registro)
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Usuarios/Create (Registro)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterRequest usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            try
            {
                // Protección: asegurar que no puedan registrarse como admin
                usuario.Rango = RolUsuario.Empleado; // ✅ Usando el nombre del enum

                var client = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{GetBaseUrl()}/api/auth/register", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Opción: mostrar mensaje o redirigir directamente al login
                    TempData["Mensaje"] = "Registro exitoso. Por favor verifica tu correo electrónico.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Opcionalmente podrías leer el error exacto desde la respuesta de la API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error al registrar: {errorContent}");
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ocurrió un error inesperado: {ex.Message}");
                return View(usuario);
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult configUsuario()
        {
            ViewBag.Usuarios = _context.Usuarios.ToList();
            return View();
        }
        public IActionResult ObtenerUsuariosPorId(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.ID == id);
            if (usuario == null)
                return NotFound();

            var dto = new RegisterRequest
            {
                ID = usuario.ID.ToString(),
                Nombre = usuario.Nombre,
                UsuarioNombre = usuario.UsuarioNombre,
                Correo = usuario.Correo,
                Password = usuario.PasswordHash,
                Rango = usuario.Rango
            };
            return Json(dto);
        }
        [HttpPost]
        public IActionResult Edit(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View("configUsuario", model);

            // Obtener el ID del usuario actualmente logueado
            var usuarioActualId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value ?? "0");

            // Buscar al usuario que se está intentando modificar
            var usuario = _context.Usuarios.FirstOrDefault(u => u.ID == int.Parse(model.ID));
            if (usuario == null)
                return NotFound();

            // ✅ Seguridad: impedir que un admin modifique a otro admin (a menos que sea él mismo)
            if (usuario.Rango == RolUsuario.Administrador && usuario.ID != usuarioActualId)
            {
                TempData["Error"] = "No puedes modificar a otro administrador.";
                return RedirectToAction("configUsuario");
            }

            // Aplicar cambios
            usuario.Nombre = model.Nombre;
            usuario.UsuarioNombre = model.UsuarioNombre;
            usuario.Correo = model.Correo;
            usuario.Rango = model.Rango;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                usuario.PasswordHash = model.Password;
            }

            _context.SaveChanges();
            TempData["Mensaje"] = "Usuario actualizado correctamente.";
            return RedirectToAction("configUsuario");
        }

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View(new RecuperarPasswordRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarCorreoRecuperacion(RecuperarPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return View("RecoverPassword", request);

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == request.Correo);

            if (usuario != null)
            {
                usuario.TokenRecuperacion = Guid.NewGuid().ToString();
                usuario.RecuperacionExpira = DateTime.UtcNow.AddHours(1);
                await _context.SaveChangesAsync();

                await _emailService.EnviarCorreoRecuperacion(usuario.Correo, usuario.Nombre, usuario.TokenRecuperacion);
            }

            TempData["Mensaje"] = "Si el correo existe, se ha enviado un enlace.";
            return RedirectToAction("Index"); // o "RecoverPassword" si deseas quedarte en la vista
        }
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token no válido.");

            var model = new ResetPasswordRequest { Token = token };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestablecerPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return View("ResetPassword", request);

            var usuario = _context.Usuarios.FirstOrDefault(u =>
                u.TokenRecuperacion == request.Token &&
                u.RecuperacionExpira > DateTime.UtcNow);

            if (usuario == null)
            {
                ModelState.AddModelError("", "El token es inválido o ha expirado.");
                return View("ResetPassword", request);
            }

            var hasher = new PasswordHasher<Usuario>();
            usuario.PasswordHash = hasher.HashPassword(usuario, request.NuevaPassword);
            usuario.TokenRecuperacion = null;
            usuario.RecuperacionExpira = null;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Tu contraseña fue restablecida correctamente.";
            return RedirectToAction("Index");
        }





    }
}

