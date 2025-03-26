using Domain.Interfaces.Infrastructure.User;
using Infrastructure.Repositories.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InjectionDependenciesInfrastructure
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Registro de repositorios (implementaciones de las interfaces)
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
