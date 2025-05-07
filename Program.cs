using Microsoft.EntityFrameworkCore;
using One_Vision.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

////// Permitir conexiones desde cualquier IP (remoto)
//builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

// Configurar DbContext con SQL Server remoto
builder.Services.AddDbContext<OneVisionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticaci�n con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuarios/Index";
        options.LogoutPath = "/Usuarios/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// Configurar CORS (abierto para pruebas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Leer dominio del frontend (solo para mostrar)
var frontendDomain = builder.Configuration["CustomSettings:FrontendDomain"];
Console.WriteLine($"Frontend Domain: {frontendDomain}");

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Activar CORS
app.UseCors("AllowAll");

// Autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

// Archivos est�ticos + rutas MVC
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Ejecutar la aplicaci�n
app.Run();
