using AuthService.Api.Extensions;
using AuthService.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

// REGISTRO DE SERVICIOS
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


// Inicialización de la base de datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Iniciando la migracion de la base de datos...");

        await context.Database.EnsureCreatedAsync();

        logger.LogInformation("Base de datos migrada exitosamente.");

        await DataSeeder.SeedAsync(context);

        logger.LogInformation("Datos iniciales insertados exitosamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error al inicializar la base de datos.");
        throw;
    }
}

app.Run();