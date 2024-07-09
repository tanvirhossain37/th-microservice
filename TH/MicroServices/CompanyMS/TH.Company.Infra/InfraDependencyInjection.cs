using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TH.CompanyMS.App;
using TH.CompanyMS.infra;
using TH.CompanyMS.Infra;
using TH.Repo;

namespace TH.CompanyMS.Infra;

public static class InfraDependencyInjection
{
    public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CompanyDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CompanyDB"));
        });

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
        //Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClaimBasedPolicy", policy =>
            {
                policy.Requirements.Add(new CompanyConventionBasedRequirement());
                //policy.RequireClaim("Test");
            });

            options.AddPolicy("ReadPolicy", policy => { policy.RequireClaim("Company", "Read"); });
            options.AddPolicy("WritePolicy", policy => { policy.RequireClaim("Company", "Write"); });
            options.AddPolicy("UpdatePolicy", policy => { policy.RequireClaim("Company", "Update"); });
            options.AddPolicy("SoftDeletePolicy", policy => { policy.RequireClaim("Company", "SoftDelete"); });
            options.AddPolicy("DeletePolicy", policy => { policy.RequireClaim("Company", "Delete"); });
        });


        //services.AddSingleton<TestRequirement>();
        //services.AddSingleton<ConventionBasedRequirement>();
        services.AddHttpContextAccessor();
        services.AddSingleton<HttpContextAccessor>();
        services.AddSingleton<IAuthorizationHandler, CompanyConventionBasedRequirementHandler>();

        //Authentication
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value))
                };
            });

        return services;
    }
}