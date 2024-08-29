using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.AuthMS.App;
using TH.Common.Model;

namespace TH.AuthMS.Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddAuthInfraDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("AuthDB")); });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    //options.Password.RequireNonAlphanumeric = true;
                    //options.Password.RequireDigit = true;
                    //options.Password.RequireLowercase = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequiredLength = 8;

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AuthDbContext>();
            services.AddScoped<IAuthRepo, AuthRepo>();
            //services.AddScoped<UserManager<User>>();
            //services.AddScoped<RoleManager<User>>();

            //services.AddIdentityCore<ApplicationUser>();

            return services;
        }

        public static IServiceCollection AddAuthJwtTokenBasedAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtAuthorizationPolicies(configuration);
            ////Authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ClaimBasedPolicy", policy =>
            //    {
            //        policy.Requirements.Add(new AuthConventionBasedRequirement());
            //        //policy.RequireClaim("Test");
            //    });

            //    options.AddPolicy("TestPolicy", policy =>
            //    {
            //        policy.RequireClaim("Test", "1");
            //    });

            //    options.AddPolicy("ReadPolicy", policy =>
            //    {
            //        policy.RequireClaim("Test", "1");
            //    });
            //    options.AddPolicy("WritePolicy", policy =>
            //    {
            //        policy.RequireClaim("Test", "2");
            //    });
            //    options.AddPolicy("UpdatePolicy", policy =>
            //    {
            //        policy.RequireClaim("Test", "3");
            //    });
            //    options.AddPolicy("DeletePolicy", policy =>
            //    {
            //        policy.RequireClaim("Test", "4");
            //    });
            //});


            ////services.AddSingleton<TestRequirement>();
            ////services.AddSingleton<ConventionBasedRequirement>();
            //services.AddHttpContextAccessor();
            //services.AddSingleton<HttpContextAccessor>();
            //services.AddSingleton<IAuthorizationHandler, AuthConventionBasedRequirementHandler>();

            ////Authentication
            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateActor = true,
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            RequireExpirationTime = true,
            //            ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
            //            ValidAudience = configuration.GetSection("Jwt:Audience").Value,
            //            ClockSkew = TimeSpan.Zero,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value))
            //        };
            //    });

            //services.AddScoped<JwtConfiguration>();

            //services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

            return services;
        }
    }
}