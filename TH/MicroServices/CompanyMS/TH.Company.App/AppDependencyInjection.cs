using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.AuthMS.Grpc;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public static class AppDependencyInjection
{
    public static IServiceCollection AddCompanyAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<Branch>();
        services.AddScoped<BranchUser>();
        services.AddScoped<Company>();
        services.AddScoped<Module>();
        services.AddScoped<Permission>();
        services.AddScoped<Role>();
        services.AddScoped<User>();
        services.AddScoped<UserCompany>();
        services.AddScoped<UserRole>();

        services.AddScoped<BranchFilterModel>();
        services.AddScoped<BranchUserFilterModel>();
        services.AddScoped<CompanyFilterModel>();
        services.AddScoped<ModuleFilterModel>();
        services.AddScoped<PermissionFilterModel>();
        services.AddScoped<RoleFilterModel>();
        services.AddScoped<UserFilterModel>();
        services.AddScoped<UserCompanyFilterModel>();
        services.AddScoped<UserRoleFilterModel>();


        services.AddScoped<BranchInputModel>();
        services.AddScoped<BranchUserInputModel>();
        services.AddScoped<CompanyInputModel>();
        services.AddScoped<ModuleInputModel>();
        services.AddScoped<PermissionInputModel>();
        services.AddScoped<RoleInputModel>();
        services.AddScoped<UserInputModel>();
        services.AddScoped<UserCompanyInputModel>();
        services.AddScoped<UserRoleInputModel>();

        services.AddScoped<BranchViewModel>();
        services.AddScoped<BranchUserViewModel>();
        services.AddScoped<CompanyViewModel>();
        services.AddScoped<ModuleViewModel>();
        services.AddScoped<PermissionViewModel>();
        services.AddScoped<RoleViewModel>();
        services.AddScoped<UserViewModel>();
        services.AddScoped<UserCompanyViewModel>();
        services.AddScoped<UserRoleViewModel>();


        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IBranchUserService, BranchUserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserCompanyService, UserCompanyService>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        //services.AddGrpc();
        services.AddGrpcClient<AuthProtoService.AuthProtoServiceClient>(
            options => options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:GrpcUrl")));

        services.AddScoped<AuthGrpcClientService>();

        return services;
    }

    public static IServiceCollection AddCompanyAppEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddEventBus(configuration);
        //RabbitMQ Config
        services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration.GetSection("EventBus:Host").Value); }); });

        return services;
    }
}