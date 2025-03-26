using Application.DTOs.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pasa al siguiente middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Determinar el código de estado por defecto
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorMessage = ex.Message;
            string details = ex.InnerException?.Message ?? "";
            // Inicializar la lista de errores de validación
            List<string> errors = [];

            // Manejar casos específicos de excepciones
            if (ex is ValidationException validationEx)
            {
                // Si la excepción contiene un ValidationResult, accedemos a los errores
                if (validationEx.ValidationResult != null)
                {
                    errors = validationEx.ValidationResult.MemberNames.ToList();
                }
                // Si es una excepción de validación, usamos BadRequest (400)
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (ex is CustomException customEx)
            {
                // Si es una CustomException, usamos el statusCode y errorMessage de la excepción personalizada
                statusCode = (HttpStatusCode)customEx.StatusCode;
                errorMessage = customEx.ErrorMessage;
                details = customEx.Details;
            }

            // Crear la respuesta de error con el formato específico
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                ErrorMessage = errorMessage,
                Details = details,
                Errors = errors
            };

            // Configurar la respuesta HTTP
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // Serializar la respuesta en JSON
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
