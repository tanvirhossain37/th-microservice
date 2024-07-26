using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.AuthMS.App.GrpcServices;
using TH.Grpc.Protos;

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

            services.AddGrpcClient<SpaceProtoService.SpaceProtoServiceClient>(
                    options => options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:GrpcUrl")))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                    return handler;
                });
            services.AddScoped<CompanyGrpcClientService>();

            services.AddEventBus(configuration);

            return services;
        }
    }
}