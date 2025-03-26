using Api.Middleware;
using Application;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuraci�n desde el archivo correspondiente
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Registra el DbContext en el contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configura el swagger para manejar la autenticaci�n con token JWT (Bearer)
builder.Services.AddSwaggerGen(options =>
{
    // Configura el titulo de la documentaci�n API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clean arquitecture",
        Version = "v1",
        Description = "Esta API sigue los principios de la arquitectura limpia, " +
        "separando responsabilidades en capas y promoviendo la mantenibilidad, " +
        "escalabilidad y testabilidad. Incluye autenticaci�n JWT y autorizaci�n para proteger los recursos."
    });

    // Configura c�mo Swagger maneja la autenticaci�n
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese un token v�lido en el formato Bearer {token}", // Explica c�mo se debe proporcionar el token
        Type = SecuritySchemeType.Http, // indica que el token ser� enviado mediante HTTP.
        BearerFormat = "JWT", // Indica que el formato del token sigue el est�ndar JWT
        Scheme = "bearer" // define el esquema de autenticaci�n
    });

    // Registra el filtro personalizado para rutas p�blicas
    // Si son p�blicas, elimina la necesidad de un token. Si no, agrega el esquema de seguridad.
    options.OperationFilter<PublicFilter>();
});

// Registrar AutoMapper para cargar todos los perfiles en el ensamblado de Application
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Agregar dependencia de infraestructura
builder.Services.AddInfrastructureDependencies();

// Agregar dependencia de los servicios de la capa de applicati�n
builder.Services.AddApplicationServicesDependencies();

// Agregar dependencia de los casos de uso de la capa de applicati�n
builder.Services.AddApplicationUseCasesDependencies();

var app = builder.Build();

// Agregar el middleware personalizado
app.UseMiddleware<Api.Middleware.ExceptionMiddleware>();

app.UseMiddleware<TokenValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean arquitecture");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
