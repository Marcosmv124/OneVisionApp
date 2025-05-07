
using Microsoft.AspNetCore.Mvc;
using One_Vision.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace One_Vision.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly OneVisionDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();


        public UsuariosController(OneVisionDbContext context)
        {
            _context = context;
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

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Usuario usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(u =>
                u.UsuarioNombre == usuario.UsuarioNombre /*&&
                u.Password == usuario.Password*/);

            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, usuario.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                      new Claim(ClaimTypes.Name, user.UsuarioNombre),
                      // Puedes agregar más claims como el rol del usuario, etc.
                    };
                    // Crear la identidad y la principal
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Iniciar sesión
                    //  await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    return RedirectToAction("Index", "Home"); // Redirige al Home o a la página protegida
                }
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(usuario);
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
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password); 
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

        // MÉTODO TEMPORAL PARA HASHEAR CONTRASEÑAS YA REGISTRADAS
        //public IActionResult HashPasswords()
        //{
        //    var usuarios = _context.Usuarios.ToList();

        //    foreach (var user in usuarios)
        //    {
        //        // Detectamos si la contraseña aún no está hasheada (ej. por longitud o patrón)
        //        if (user.Password.Length < 60) // Los hashes usualmente son más largos
        //        {
        //            user.Password = _passwordHasher.HashPassword(user, user.Password);
        //        }
        //    }

        //    _context.SaveChanges();
        //    return Content("Contraseñas actualizadas correctamente.");
        //}

        public IActionResult ConfigUsuario()
        {
            return View(); 
        }
    }
}

