using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyApp.Application;
using MyApp.Application.DTOs.Common;
using MyApp.Infrastructure;
using MyApp.Infrastructure.Context;
using MyApp.Presentation.MiddlewaresAndFilters;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración desde el archivo correspondiente
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

string? connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No se encontró la cadena de conexión a la base de datos.");
}

// Registra el DbContext en el contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configura el swagger para manejar la autenticación con token JWT (Bearer)
builder.Services.AddSwaggerGen(options =>
{
    // Configura el titulo de la documentación API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clean arquitecture",
        Version = "v1",
        Description = "Esta API sigue los principios de la arquitectura limpia, " +
        "separando responsabilidades en capas y promoviendo la mantenibilidad, " +
        "escalabilidad y testabilidad. Incluye autenticación JWT y autorización para proteger los recursos."
    });

    // Configura cómo Swagger maneja la autenticación
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese un token válido en el formato Bearer {token}", // Explica cómo se debe proporcionar el token
        Type = SecuritySchemeType.Http, // indica que el token será enviado mediante HTTP.
        BearerFormat = "JWT", // Indica que el formato del token sigue el estándar JWT
        Scheme = "bearer" // define el esquema de autenticación
    });

    // Registra el filtro personalizado para rutas públicas
    // Si son públicas, elimina la necesidad de un token. Si no, agrega el esquema de seguridad.
    options.OperationFilter<PublicFilter>();
});

// Registrar AutoMapper para cargar todos los perfiles en el ensamblado de Application
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Agregar dependencia de cada capa del proyecto
builder.Services.AddInfrastructureDependencies();
builder.Services.AddApplicationUseCasesDependencies();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Agregar el middleware personalizado
app.UseMiddleware<ExceptionMiddleware>();

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
