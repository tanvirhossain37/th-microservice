using System.Reflection;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace TH.Common.Model;

public static class AddDependencyInjection
{
    public static IServiceCollection AddJwtAuthorizationPolicies(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true)
            .Build();

        //Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClaimBasedPolicy", policy =>
            {
                policy.Requirements.Add(new CommonConventionBasedRequirement());
                //policy.RequireClaim("Test");
            });

            options.AddPolicy("BranchReadPolicy", policy => { policy.RequireClaim("Branch", TS.Permissions.Read); });
            options.AddPolicy("BranchWritePolicy", policy => { policy.RequireClaim("Branch", TS.Permissions.Write); });
            options.AddPolicy("BranchUpdatePolicy", policy => { policy.RequireClaim("Branch", TS.Permissions.Update); });
            options.AddPolicy("BranchSoftDeletePolicy", policy => { policy.RequireClaim("Branch", TS.Permissions.SoftDelete); });
            options.AddPolicy("BranchDeletePolicy", policy => { policy.RequireClaim("Branch", TS.Permissions.Delete); });

            options.AddPolicy("BranchUserReadPolicy", policy => { policy.RequireClaim("BranchUser", TS.Permissions.Read); });
            options.AddPolicy("BranchUserWritePolicy", policy => { policy.RequireClaim("BranchUser", TS.Permissions.Write); });
            options.AddPolicy("BranchUserUpdatePolicy", policy => { policy.RequireClaim("BranchUser", TS.Permissions.Update); });
            options.AddPolicy("BranchUserSoftDeletePolicy", policy => { policy.RequireClaim("BranchUser", TS.Permissions.SoftDelete); });
            options.AddPolicy("BranchUserDeletePolicy", policy => { policy.RequireClaim("BranchUser", TS.Permissions.Delete); });

            options.AddPolicy("CompanyReadPolicy", policy => { policy.RequireClaim("Company", TS.Permissions.Read); });
            options.AddPolicy("CompanyWritePolicy", policy => { policy.RequireClaim("Company", TS.Permissions.Write); });
            options.AddPolicy("CompanyUpdatePolicy", policy => { policy.RequireClaim("Company", TS.Permissions.Update); });
            options.AddPolicy("CompanySoftDeletePolicy", policy => { policy.RequireClaim("Company", TS.Permissions.SoftDelete); });
            options.AddPolicy("CompanyDeletePolicy", policy => { policy.RequireClaim("Company", TS.Permissions.Delete); });

            options.AddPolicy("ModuleReadPolicy", policy => { policy.RequireClaim("Module", TS.Permissions.Read); });
            options.AddPolicy("ModuleWritePolicy", policy => { policy.RequireClaim("Module", TS.Permissions.Write); });
            options.AddPolicy("ModuleUpdatePolicy", policy => { policy.RequireClaim("Module", TS.Permissions.Update); });
            options.AddPolicy("ModuleSoftDeletePolicy", policy => { policy.RequireClaim("Module", TS.Permissions.SoftDelete); });
            options.AddPolicy("ModuleDeletePolicy", policy => { policy.RequireClaim("Module", TS.Permissions.Delete); });

            options.AddPolicy("PermissionReadPolicy", policy => { policy.RequireClaim("Permission", TS.Permissions.Read); });
            options.AddPolicy("PermissionWritePolicy", policy => { policy.RequireClaim("Permission", TS.Permissions.Write); });
            options.AddPolicy("PermissionUpdatePolicy", policy => { policy.RequireClaim("Permission", TS.Permissions.Update); });
            options.AddPolicy("PermissionSoftDeletePolicy", policy => { policy.RequireClaim("Permission", TS.Permissions.SoftDelete); });
            options.AddPolicy("PermissionDeletePolicy", policy => { policy.RequireClaim("Permission", TS.Permissions.Delete); });

            options.AddPolicy("RoleReadPolicy", policy => { policy.RequireClaim("Role", TS.Permissions.Read); });
            options.AddPolicy("RoleWritePolicy", policy => { policy.RequireClaim("Role", TS.Permissions.Write); });
            options.AddPolicy("RoleUpdatePolicy", policy => { policy.RequireClaim("Role", TS.Permissions.Update); });
            options.AddPolicy("RoleSoftDeletePolicy", policy => { policy.RequireClaim("Role", TS.Permissions.SoftDelete); });
            options.AddPolicy("RoleDeletePolicy", policy => { policy.RequireClaim("Role", TS.Permissions.Delete); });

            options.AddPolicy("UserReadPolicy", policy => { policy.RequireClaim("User", TS.Permissions.Read); });
            options.AddPolicy("UserWritePolicy", policy => { policy.RequireClaim("User", TS.Permissions.Write); });
            options.AddPolicy("UserUpdatePolicy", policy => { policy.RequireClaim("User", TS.Permissions.Update); });
            options.AddPolicy("UserSoftDeletePolicy", policy => { policy.RequireClaim("User", TS.Permissions.SoftDelete); });
            options.AddPolicy("UserDeletePolicy", policy => { policy.RequireClaim("User", TS.Permissions.Delete); });

            options.AddPolicy("UserRoleReadPolicy", policy => { policy.RequireClaim("UserRole", TS.Permissions.Read); });
            options.AddPolicy("UserRoleWritePolicy", policy => { policy.RequireClaim("UserRole", TS.Permissions.Write); });
            options.AddPolicy("UserRoleUpdatePolicy", policy => { policy.RequireClaim("UserRole", TS.Permissions.Update); });
            options.AddPolicy("UserRoleSoftDeletePolicy", policy => { policy.RequireClaim("UserRole", TS.Permissions.SoftDelete); });
            options.AddPolicy("UserRoleDeletePolicy", policy => { policy.RequireClaim("UserRole", TS.Permissions.Delete); });

        });

        //AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<UserResolver>();
        services.AddScoped<Print>();
        services.AddScoped<SortFilter>();

        services.AddHttpContextAccessor();
        services.AddScoped<HttpContextAccessor>();
        services.AddScoped<IAuthorizationHandler, CommonConventionBasedRequirementHandler>();

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
                    ValidIssuer = configurationRoot.GetSection("Jwt:Issuer").Value,
                    ValidAudience = configurationRoot.GetSection("Jwt:Audience").Value,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationRoot.GetSection("Jwt:Key").Value))
                };
            });

        //geo info
        services.AddScoped<Geo>();
        services.AddScoped<GeoHelper>();

        return services;
    }

    public static WebApplicationBuilder AddLog4NetDependency(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddLog4Net();

        return builder;
    }

    //public static IServiceCollection AddEventBus(this IServiceCollection services,
    //    IConfiguration configuration)
    //{
    //    //RabbitMQ Config
    //    services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration.GetSection("EventBus:Host").Value); }); });

    //    return services;
    //}
}