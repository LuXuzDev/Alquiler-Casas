using Microsoft.EntityFrameworkCore;
using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.Controladora;
using SistemaAlquiler.API.Utilidades.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using SistemaAlquiler.API.Utilidades.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SistemaAlquiler.Entidades;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores y servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste your token here:"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de Identity y JWT
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<DB_Context>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("TuClaveSecretaMuySeguraTanSeguraQueNoHayPeligroDeUnAtaqueInformatico"))
    };
});

// Configuración de roles y políticas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("1"));
    options.AddPolicy("RequireAdmin_Gestor", policy => policy.RequireRole("1", "2"));
    options.AddPolicy("RequireGestor", policy => policy.RequireRole("2"));
});

builder.Services.AddAuthorization();

// Inyección de dependencias y mapeos
builder.Services.inyectarDependencia(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Método para inicializar roles
async Task roles(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var rolControlador = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "1", "2", "3" };

    foreach (var rol in roles)
    {
        if (!await rolControlador.RoleExistsAsync(rol))
            await rolControlador.CreateAsync(new IdentityRole(rol));
    }
}

var app = builder.Build();

// Habilitar archivos estáticos desde wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "" // Archivos accesibles directamente desde la raíz
});

// Habilitar CORS
app.UseCors();

// Configuración de Swagger en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Crear roles iniciales
await roles(app.Services);

// Mapear controladores
app.MapControllers();

app.Run();
