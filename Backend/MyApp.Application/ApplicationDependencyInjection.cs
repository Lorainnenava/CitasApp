using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;
using MyApp.Application.Interfaces.UseCases.Common;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Application.Interfaces.UseCases.Modules;
using MyApp.Application.Interfaces.UseCases.RefreshTokens;
using MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions;
using MyApp.Application.Interfaces.UseCases.SubModulePermissions;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Interfaces.UseCases.UserSessions;
using MyApp.Application.Services;
using MyApp.Application.UseCases.ChangeHospitalRequests;
using MyApp.Application.UseCases.Common;
using MyApp.Application.UseCases.Hospitals;
using MyApp.Application.UseCases.Modules;
using MyApp.Application.UseCases.RefreshTokens;
using MyApp.Application.UseCases.RoleSubModulePermissions;
using MyApp.Application.UseCases.SubModulePermissions;
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
            services.AddScoped<IUserSetActiveStatusUseCase, UserSetActiveStatusUseCase>();
            services.AddScoped<IUserUpdateUseCase, UserUpdateUseCase>();
            services.AddScoped<IUserGetAllPaginatedUseCase, UserGetAllPaginatedUseCase>();
            services.AddScoped<IUserValidateUseCase, UserValidateUseCase>();
            services.AddScoped<IUserChangePasswordUseCase, UserChangePasswordUseCase>();

            services.AddScoped<IUserSessionsCreateUseCase, UserSessionCreateUseCase>();
            services.AddScoped<IUserSessionRevokedUseCase, UserSessionRevokedUseCase>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IGetModulesWithEverythingUseCase, GetModulesWithEverythingUseCase>();

            services.AddScoped<IAssignPermissionsToSubModuleUseCase, AssignPermissionsToSubModuleUseCase>();
            services.AddScoped<IGetSubModulePermissionsBySubModuleIdUseCase, GetSubModulePermissionsBySubModuleIdUseCase>();
            services.AddScoped<IUpdatePermissionsFromSubModuleUseCase, UpdatePermissionFromSubModuloUseCase>();

            services.AddScoped<IAssignSubModulePermissionsToRoleUseCase, AssignSubModulePermissionsToRoleUseCase>();
            services.AddScoped<IGetSubModulePermissionsByRoleIdUseCase, GetSubModulePermissionsByRoleIdUseCase>();
            services.AddScoped<IUpdateRoleSubModulePermissionsFromRoleUseCase, UpdateRoleSubModulePermissionsFromRoleUseCase>();

            services.AddScoped<IHospitalCreateUseCase, HospitalCreateUseCase>();
            services.AddScoped<IHospitalGetAllPaginatedUseCase, HospitalGetAllPaginatedUseCase>();
            services.AddScoped<IHospitalGetByIdUseCase, HospitalGetByIdUseCase>();
            services.AddScoped<IHospitalToogleIsActiveUseCase, HospitalToogleIsActiveUseCase>();
            services.AddScoped<IHospitalUpdateUseCase, HospitalUpdateUseCase>();

            services.AddScoped<IChangeHospitalRequestChangeStateUseCase, ChangeHospitalRequestChangeStateUseCase>();
            services.AddScoped<IChangeHospitalRequestCreateUseCase, ChangeHospitalRequestCreateUseCase>();
            services.AddScoped<IChangeHospitalRequestGetByIdUseCase, ChangeHospitalRequestGetByIdUseCase>();
            services.AddScoped<IChangeHospitalRequestGetAllPaginatedUseCase, ChangeHospitalRequestGetAllPaginatedUseCase>();

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
