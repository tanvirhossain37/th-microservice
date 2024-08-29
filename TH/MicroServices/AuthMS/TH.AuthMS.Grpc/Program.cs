using System.Reflection;
using MassTransit;
using TH.AuthMS.App;
using TH.AuthMS.Grpc.Services;
using TH.AuthMS.Infra;
using TH.AuthMS.API;

namespace TH.AuthMS.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Tanvir
            builder.Services.AddAuthAppDependencyInjection(builder.Configuration);
            builder.Services.AddAuthInfraDependencyInjection(builder.Configuration);
            builder.Services.AddAuthJwtTokenBasedAuthentication(builder.Configuration);
            //builder.Services.AddCookieBasedAuthentication(builder.Configuration);

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add services to the container.
            builder.Services.AddGrpc();

            //RabbitMQ Config
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<UserCreateEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetSection("EventBus:Host").Value);
                    cfg.ReceiveEndpoint(EventBus.Messages.EventBus.UserCreateQueue, c =>
                    {
                        c.ConfigureConsumer<UserCreateEventConsumer>(ctx);
                    });
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<AuthGrpcServerService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}