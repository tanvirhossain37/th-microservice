using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TH.Common.App;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.infra;
using TH.CompanyMS.Infra;
using TH.Repo;

namespace TH.CompanyMS.Infra;

public static class InfraDependencyInjection
{
    public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CompanyDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("CompanyDB")); });

        services.AddScoped<IBranchRepo, BranchRepo>();
        services.AddScoped<IBranchUserRepo, BranchUserRepo>();
        services.AddScoped<ICompanyRepo, CompanyRepo>();
        services.AddScoped<IModuleRepo, ModuleRepo>();
        services.AddScoped<IPermissionRepo, PermissionRepo>();
        services.AddScoped<IRoleRepo, RoleRepo>();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IUserRoleRepo, UserRoleRepo>();
        services.AddScoped<IUow, Uow>();

        services.AddScoped<CompanyDbContext>();
        services.AddScoped<ICustomSort, CustomSort>();

        return services;
    }

    public static IServiceCollection AddJwtTokenBasedAuthentication(this IServiceCollection services,
        IConfiguration configuration)

    {
        services.AddJwtAuthorizationPolicies(configuration);

        return services;
    }
}