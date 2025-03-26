using Application.Interfaces.Services;
using Application.Services;
using Domain.Interfaces.Infrastructure.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class InjectionDependenciesApplicationServices
    {
        public static IServiceCollection AddApplicationServicesDependencies(this IServiceCollection services)
        {
            // Registro de servicios (implementaciones de las interfaces)
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
