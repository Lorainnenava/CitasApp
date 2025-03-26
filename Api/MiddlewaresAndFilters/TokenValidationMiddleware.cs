using System.IdentityModel.Tokens.Jwt;

namespace Api.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Ignorar rutas públicas como Swagger y documentación
            var path = context.Request.Path.Value;
            if (path != null &&
                path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            // Verificar si la ruta actual permite acceso anónimo
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<Microsoft.AspNetCore.Authorization.IAllowAnonymous>() != null)
            {
                await _next(context); // Continuar sin validar el token
                return;
            }

            // Obtener el token del encabezado Authorization
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token is missing");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Validar la expiración del token
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    throw new UnauthorizedAccessException("Token has expired");
                }

                // Continuar con la solicitud si el token es válido
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                throw; // Deja que el middleware de excepciones maneje esto
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }
        }
    }
}
