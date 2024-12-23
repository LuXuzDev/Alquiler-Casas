using Microsoft.EntityFrameworkCore;
using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.Controladora;
using SistemaAlquiler.API.Utilidades.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using SistemaAlquiler.API.Utilidades.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SistemaAlquiler.Entidades;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



//JWT 

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Past token here:"
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("1"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin_Gestor", policy => policy.RequireRole("1","2"));
   
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireGestor", policy => policy.RequireRole("2"));
});


builder.Services.AddAuthorization();
builder.Services.inyectarDependencia(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



//Cors
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


async Task roles(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var rolControlador = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "1", "2", "3" };
    
    foreach(var rol in roles)
    {
        if (!await rolControlador.RoleExistsAsync(rol))
            await rolControlador.CreateAsync(new IdentityRole(rol));
    }
}

var app = builder.Build();
app.UseCors();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

await roles(app.Services);

app.MapControllers();

app.Run();
