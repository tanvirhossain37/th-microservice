using System.Reflection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Infra;

namespace TH.CompanyMS.API
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

            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

            //tanvir
            app.MapHub<CompanyHub>("/Branch");
            app.MapHub<CompanyHub>("/BranchUser");
            app.MapHub<CompanyHub>("/Company");
            app.MapHub<CompanyHub>("/Module");
            app.MapHub<CompanyHub>("/Permission");
            app.MapHub<CompanyHub>("/Role");
            app.MapHub<CompanyHub>("/User");
            app.MapHub<CompanyHub>("/UserRole");

            
            app.MapControllers();

            app.Run();
        }
    }
}