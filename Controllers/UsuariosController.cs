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

namespace One_Vision.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly SendGridSettings _settings;
        public UsuariosController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env, IOptions<SendGridSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
            _settings = settings.Value;
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

                // Crear claims básicos (puedes enriquecerlos si quieres agregar más info desde tu API)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UsuarioNombre),
                    new Claim("JWT", token) // Guardamos el token por si lo ocupamos después para llamadas API desde la vista
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
       [Authorize]
        public IActionResult configUsuario(){
        return View(); 
        }

    }
}

