using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.Core;

namespace TH.AuthMS.App
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<User>();
            //services.AddScoped<SignUpInputModel>();
            //services.AddScoped<SignUpViewModel>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}