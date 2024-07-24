using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using TH.AuthMS.Core;
using TH.Common.Model;

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

            ////RabbitMQ Config
            //services.AddMassTransit(config =>
            //{
            //    config.UsingRabbitMq((ctx, cfg) =>
            //    {
            //        cfg.Host(configuration.GetSection("EventBus:Host").Value);
            //    });
            //});

            //services.AddGrpcClient<PermissionProtoService.PermissionProtoServiceClient>(
            //    options => options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:GrpcUrl")));
            //services.AddScoped<PermissionGrpcClientService>();

            services.AddEventBus(configuration);

            return services;
        }
    }
}