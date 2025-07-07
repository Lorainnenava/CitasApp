using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyApp.Application;
using MyApp.Application.DTOs.Common;
using MyApp.Infrastructure;
using MyApp.Infrastructure.Context;
using MyApp.Presentation.MiddlewaresAndFilters;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuraci�n desde el archivo correspondiente
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

string? connectionString = null;

if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    if (!string.IsNullOrWhiteSpace(databaseUrl))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        connectionString = $"Host={uri.Host};Port={uri.Port};Username={userInfo[0]};Password={userInfo[1]};Database={uri.AbsolutePath.TrimStart('/')};SSL Mode=Require;Trust Server Certificate=true";
    }
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No se encontr� la cadena de conexi�n a la base de datos.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

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

// Agregar dependencia de cada capa del proyecto
builder.Services.AddInfrastructureDependencies();
builder.Services.AddApplicationUseCasesDependencies();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("HasPermission", policy =>
        policy.Requirements.Add(new PermissionRequirement()));


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
