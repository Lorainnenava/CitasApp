using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.UseCases.Common;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Interfaces.UseCases.UserSessions;
using MyApp.Application.Services;
using MyApp.Application.UseCases.Common;
using MyApp.Application.UseCases.Users;
using MyApp.Application.UseCases.UserSessions;

namespace MyApp.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationUseCasesDependencies(this IServiceCollection services)
        {
            // Registro de casos de uso (implementaciones de las interfaces)
            services.AddScoped<IUserCreateUseCase, UserCreateUseCase>();
            services.AddScoped<IUserGetByIdUseCase, UserGetByIdUseCase>();
            services.AddScoped<IUserDeleteUseCase, UserDeleteUseCase>();
            services.AddScoped<IUserUpdateUseCase, UserUpdateUseCase>();
            services.AddScoped<IUserGetAllUseCase, UserGetAllUseCase>();
            services.AddScoped<IUserSessionsCreateUseCase, UserSessionCreateUseCase>();

            services.AddScoped(typeof(IGenericCreateUseCase<,>), typeof(GenericCreateUseCase<,>));
            services.AddScoped(typeof(IGenericGetAllUseCase<,>), typeof(GenericGetAllPaginatedUseCase<,>));
            services.AddScoped(typeof(IGenericGetOneUseCase<,>), typeof(GenericGetOneUseCase<,>));
            services.AddScoped(typeof(IGenericUpdateUseCase<,,>), typeof(GenericUpdateUseCase<,,>));
            services.AddScoped(typeof(IGenericDeleteUseCase<>), typeof(GenericDeleteUseCase<>));

            services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();

            return services;
        }
    }
}
