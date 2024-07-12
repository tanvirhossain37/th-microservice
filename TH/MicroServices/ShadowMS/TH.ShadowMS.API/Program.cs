using System.Reflection;
using MassTransit;
using TH.Common.Model;
using TH.ShadowMS.App;
using TH.ShadowMS.Infra;
using TH.EventBus.Messages;

namespace TH.ShadowMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            //Tanvir
            builder.Services.AddControllers(c => c.Filters.Add(new CommonCustomExceptionFilter()));

            //Tanvir
            builder.Services.AddAppDependencyInjection(builder.Configuration);
            builder.Services.AddInfraDependencyInjection(builder.Configuration);
            builder.Services.AddJwtTokenBasedAuthentication(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //RabbitMQ Config
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<ShadowEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetSection("EventBus:Host").Value);
                    cfg.ReceiveEndpoint(EventBus.Messages.EventBus.EmailQueue, c =>
                    {
                        c.ConfigureConsumer<ShadowEventConsumer>(ctx);
                    });
                });
            });

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

            app.UseHttpsRedirection();

            //Tanvir
            app.UseAuthentication(); // MUST be before app.UseAuthorization();
            app.UseCors(CorsPolicy); //tanvir
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}