using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using One_Vision.Models;
using One_Vision.Services;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- Servicios MVC y API ---
builder.Services.AddControllersWithViews(); // MVC
builder.Services.AddControllers();          // API

// --- DbContext compartido ---
builder.Services.AddDbContext<OneVisionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));

// --- Servicios personalizados ---
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<PasswordHasher<Usuario>>();

// --- Cliente HTTP para llamadas API desde MVC ---
builder.Services.AddHttpClient();

// --- Autenticación con cookies (predeterminada para MVC) y JWT (usado solo en API cuando se envía el header)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Usuarios/Index";
    options.LogoutPath = "/Usuarios/Logout";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddAuthorization();

// --- CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// --- Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OneVision API",
        Version = "v1",
        Description = "Documentación de la API de One Vision Optical Center"
    });
});

var app = builder.Build();

// --- Manejo de errores y HSTS ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// --- Middleware Swagger ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneVision API v1");
    c.RoutePrefix = "swagger"; // Ir a /swagger para ver la UI
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// --- Rutas MVC ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// --- Rutas API ---
app.MapControllers();

app.Run();
