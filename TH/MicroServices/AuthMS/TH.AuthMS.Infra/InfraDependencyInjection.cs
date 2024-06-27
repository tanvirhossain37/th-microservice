using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TH.AuthMS.App;
using TH.AuthMS.Core;

namespace TH.AuthMS.Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("AuthDB")); });
            services.AddIdentity<User, IdentityRole>(options =>
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

            services.AddIdentityCore<User>();

            return services;
        }

        public static IServiceCollection AddJwtTokenBasedAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
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

            services.AddScoped<JwtConfiguration>();

            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

            return services;
        }
    }
}