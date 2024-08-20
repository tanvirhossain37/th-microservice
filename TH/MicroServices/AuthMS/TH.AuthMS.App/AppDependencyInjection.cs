using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.Grpc.Protos;
using TH.Grpc.Services;

namespace TH.AuthMS.App
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddAuthAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
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

            services.AddGrpc();
            services.AddGrpcClient<SpaceProtoService.SpaceProtoServiceClient>(
                options => options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:GrpcUrl")));
            services.AddScoped<CompanyGrpcClientService>();
            services.AddScoped<AuthGrpcServerService>();

            //services.AddEventBus(configuration);
            ////RabbitMQ Config
            //services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration.GetSection("EventBus:Host").Value); }); });

            return services;
        }
    }
}