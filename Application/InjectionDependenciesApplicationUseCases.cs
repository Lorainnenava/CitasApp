using Application.Interfaces.UseCases;
using Application.UseCases.User;
using Domain.Interfaces.Infrastructure.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class InjectionDependenciesApplicationUseCases
    {
        public static IServiceCollection AddApplicationUseCasesDependencies(this IServiceCollection services)
        {
            // Registro de casos de uso (implementaciones de las interfaces)
            services.AddScoped<IUserCreateUseCase, UserCreateUseCase>();
            services.AddScoped<IUserGetByIdUseCase, UserGetByIdUseCase>();
            services.AddScoped<IUserDeleteUseCase, UserDeleteUseCase>();
            services.AddScoped<IUserUpdateUseCase, UserUpdateUseCase>();
            services.AddScoped<IUserGetAllUseCase, UserGetAllUseCase>();

            return services;
        }
    }
}
