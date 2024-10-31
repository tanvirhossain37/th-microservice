using System.Reflection;
using TH.AddressMS.App;
using TH.AddressMS.Grpc.Services;
using TH.AddressMS.Infra;

namespace TH.AddressMS.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add services to the container.
            builder.Services.AddGrpc();

            //tanvir
            //builder.Services.AddScoped<AddressDbContextSeed>();
            builder.Services.AddAddressApplicationServices(builder.Configuration);
            builder.Services.AddAddressAppEventBus(builder.Configuration);
            builder.Services.AddAddressInfrastructureServices(builder.Configuration);
            builder.Services.AddDbContext(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<AddressGrpcServerService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}