using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.Repo;

namespace TH.CompanyMS.Infra;

public static class InfraDependencyInjection
{
    public static IServiceCollection AddCompanyInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBranchRepo, BranchRepo>();
        services.AddScoped<IBranchUserRepo, BranchUserRepo>();
        services.AddScoped<ICompanyRepo, CompanyRepo>();
        services.AddScoped<IModuleRepo, ModuleRepo>();
        services.AddScoped<IPermissionRepo, PermissionRepo>();
        services.AddScoped<IRoleRepo, RoleRepo>();
        services.AddScoped<ISpaceSubscriptionRepo, SpaceSubscriptionRepo>();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IUserCompanyRepo, UserCompanyRepo>();
        services.AddScoped<IUserRoleRepo, UserRoleRepo>();
        services.AddScoped<IUow, Uow>();

        services.AddScoped<CompanyDbContext>();
        services.AddScoped<ICustomSort, CustomSort>();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CompanyDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CompanyDB"));
            //.UseLazyLoadingProxies()
            //.UseChangeTrackingProxies();
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        });

        return services;
    }

    public static IServiceCollection AddCompanyJwtTokenBasedAuthentication(this IServiceCollection services,
        IConfiguration configuration)

    {
        services.AddJwtAuthorizationPolicies(configuration);

        return services;
    }
}