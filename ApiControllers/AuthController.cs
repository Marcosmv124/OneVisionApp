
// Tu espacio de nombres
using One_Vision.DTOs;
using One_Vision.Models;
using One_Vision.Services;       // Donde está EmailService
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace One_Vision.Controllers{
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly OneVisionDbContext _context;
    private readonly PasswordHasher<Usuario> _hasher;
    private readonly EmailService _emailService;
    private readonly IConfiguration _config;

    public AuthController(OneVisionDbContext context, PasswordHasher<Usuario> hasher, EmailService emailService, IConfiguration config)
    {
        _context = context;
        _hasher = hasher;
        _emailService = emailService;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (_context.Usuarios.Any(u => u.Correo == request.Correo))
            return BadRequest("Ya existe un usuario con ese correo.");

        var user = new Usuario
        {
            Nombre = request.Nombre,
            UsuarioNombre = request.UsuarioNombre,
            Correo = request.Correo,
            Rango = request.Rango,
            PasswordHash = _hasher.HashPassword(null!, request.Password),
            TokenVerificacion = Guid.NewGuid().ToString()
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        await _emailService.EnviarCorreoConfirmacion(user.Correo, user.Nombre, user.TokenVerificacion);
        return Ok("Registrado. Revisa tu correo para confirmar.");
    }

    [HttpGet("confirmar")]
    public async Task<IActionResult> Confirmar(string token)
    {
        var user = _context.Usuarios.FirstOrDefault(u => u.TokenVerificacion == token);
        if (user == null) return NotFound("Token inválido.");

        user.EmailConfirmado = true;
        user.TokenVerificacion = null;
        await _context.SaveChangesAsync();

        return Ok("Cuenta confirmada.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        var user = _context.Usuarios.FirstOrDefault(u => u.UsuarioNombre == login.UsuarioNombre);
        if (user == null) return Unauthorized("Usuario no encontrado.");

        if (!user.EmailConfirmado)
            return Unauthorized("Correo no confirmado.");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
        if (result != PasswordVerificationResult.Success)
            return Unauthorized("Contraseña incorrecta.");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UsuarioNombre),
            new Claim(ClaimTypes.Role, user.Rango.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}
}

