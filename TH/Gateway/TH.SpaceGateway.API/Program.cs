
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace TH.SpaceGateway.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //tanvir
            builder.Services.AddOcelot(builder.Configuration.GetSection("ocelot"));

            const string CorsPolicy = "_corsPolicy";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //tanvir
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors(CorsPolicy); //tanvir

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", () => "Hello Space!");


            app.UseWebSockets();
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}