using PortalAdmin.Models;
using System.Net;
using System.Text.Json;

namespace PortalAdmin.Helpers
{
    public static class HandleError
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task HttpErrorHandler(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse? errorResponse;

            try
            {
                errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content, _jsonOptions);
            }
            catch
            {
                throw new ApplicationException($"Error inesperado. Código HTTP: {(int)response.StatusCode}");
            }

            string message = errorResponse?.ErrorMessage ?? "Error desconocido.";
            _ = errorResponse?.Details ?? "";
            var errorList = errorResponse?.Errors;

            // Puedes personalizar esto como prefieras (mostrar lista de errores, etc.)
            string fullMessage = message;
            if (errorList is { Count: > 0 })
                fullMessage += "\n" + string.Join("\n", errorList);

            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest or HttpStatusCode.Conflict => new ApplicationException(fullMessage),
                HttpStatusCode.Unauthorized => new UnauthorizedAccessException(fullMessage),
                HttpStatusCode.NotFound => new KeyNotFoundException(fullMessage),
                HttpStatusCode.InternalServerError => new ApplicationException($"Error del servidor: {fullMessage}"),
                _ => new ApplicationException($"Error HTTP {(int)response.StatusCode}: {fullMessage}"),
            };
        }
    }
}
