using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TH.Common.App;
using TH.Common.Model;
using TH.Repo;
using TH.ShadowMS.App;
using TH.ShadowMS.Infra.DbContexts;

namespace TH.ShadowMS.Infra;

public static class InfraDependencyInjection
{
    public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        //Singleton
        //services.AddSingleton<ShadowDBContext>(new ShadowDBContext(configuration.GetConnectionString("MongoDB"),
        //    configuration.GetValue<string>("DatabaseName")));

        services.AddScoped<IShadowRepo, ShadowRepo>();
        services.AddSingleton<IDatabase, ShadowDBContext>();

        return services;
    }

    public static IServiceCollection AddJwtTokenBasedAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtAuthorizationPolicies(configuration);

        return services;
    }
}