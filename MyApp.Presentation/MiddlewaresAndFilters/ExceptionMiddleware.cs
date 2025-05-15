using MyApp.Shared.DTOs;
using MyApp.Shared.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MyApp.Presentation.MiddlewaresAndFilters
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
            // Manejo de excepciones específicas
            switch (ex)
            {
                case ValidationException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    if (validationEx.ValidationResult != null)
                    {
                        errors = validationEx.ValidationResult.MemberNames.ToList();
                    }
                    break;

                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case AlreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    break;

                case InvalidOperationException invalidOpEx:
                    statusCode = HttpStatusCode.Conflict;
                    errorMessage = invalidOpEx.Message;
                    break;

                case ConflictException conflictEx:
                    statusCode = HttpStatusCode.Conflict;
                    errorMessage = conflictEx.Message;
                    break;

                case CustomException customEx:
                    statusCode = (HttpStatusCode)customEx.StatusCode;
                    errorMessage = customEx.ErrorMessage;
                    details = customEx.Details;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
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
