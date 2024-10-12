using System.Reflection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Grpc.Services;
using TH.CompanyMS.Infra;

namespace TH.CompanyMS.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Tanvir
            builder.Services.AddCompanyAppDependencyInjection(builder.Configuration);
            builder.Services.AddCompanyAppEventBus(builder.Configuration);
            builder.Services.AddCompanyInfraDependencyInjection(builder.Configuration);
            builder.Services.AddDbContext(builder.Configuration);
            builder.Services.AddCompanyJwtTokenBasedAuthentication(builder.Configuration);
            builder.AddLog4NetDependency(builder.Configuration);

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<CompanyGrpcServerService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}