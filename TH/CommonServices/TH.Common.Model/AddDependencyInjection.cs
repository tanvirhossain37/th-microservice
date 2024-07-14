using System.Reflection;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            //company
            options.AddPolicy("ReadPolicy", policy => { policy.RequireClaim("Company", "Read"); });
            options.AddPolicy("WritePolicy", policy => { policy.RequireClaim("Company", "Write"); });
            options.AddPolicy("UpdatePolicy", policy => { policy.RequireClaim("Company", "Update"); });
            options.AddPolicy("SoftDeletePolicy", policy => { policy.RequireClaim("Company", "SoftDelete"); });
            options.AddPolicy("DeletePolicy", policy => { policy.RequireClaim("Company", "Delete"); });

            //shadow
            options.AddPolicy("ReadPolicy", policy => { policy.RequireClaim("Shadow", "Read"); });
            options.AddPolicy("WritePolicy", policy => { policy.RequireClaim("Shadow", "Write"); });
            options.AddPolicy("UpdatePolicy", policy => { policy.RequireClaim("Shadow", "Update"); });
            options.AddPolicy("SoftDeletePolicy", policy => { policy.RequireClaim("Shadow", "SoftDelete"); });
            options.AddPolicy("DeletePolicy", policy => { policy.RequireClaim("Shadow", "Delete"); });
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

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services,
        IConfiguration configuration)
    {
        //RabbitMQ Config
        services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration.GetSection("EventBus:Host").Value); }); });

        return services;
    }
}