using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using One_Vision.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace One_Vision.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly OneVisionDbContext _context;

        public UsuariosController(OneVisionDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios/Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Usuario usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(u =>
                u.UsuarioNombre == usuario.UsuarioNombre &&
                u.Password == usuario.Password);

            if (user != null)
            {
                // Crear los claims
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UsuarioNombre),
                // Puedes agregar más claims como el rol del usuario, etc.
            };

                // Crear la identidad y la principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Iniciar sesión
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home"); // Redirige al Home o a la página protegida
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(usuario);
        }

        // GET: Usuarios/Create (Registro)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create (Registro)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
