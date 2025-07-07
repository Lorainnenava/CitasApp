using Microsoft.AspNetCore.Authorization;

namespace MyApp.Presentation.MiddlewaresAndFilters
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                var endpoint = httpContext.GetEndpoint();
                var requiredPermission = endpoint?.Metadata
                    .GetMetadata<RequiredPermissionAttribute>()?.Permission;

                if (string.IsNullOrEmpty(requiredPermission))
                    return Task.CompletedTask;

                var hasPermission = context.User.HasClaim("Permission", requiredPermission);

                if (hasPermission)
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
