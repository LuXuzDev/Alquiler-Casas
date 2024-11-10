using Microsoft.EntityFrameworkCore;
using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.Controladora;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.inyectarDependencia(builder.Configuration);

var app = builder.Build();

/* Crear Migracion 
 * !!MANTENER COMENTADO!!
 * using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DB_Context>();
    context.Database.Migrate();
}*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
