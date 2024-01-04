using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;

namespace TH.MongoRnDMS.Infra
{
    public static class InfraServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Singleton
            services.AddSingleton<MongoRnDDbContext>(new MongoRnDDbContext(
                configuration.GetConnectionString("MongoRnDConStr"),
                configuration.GetValue<string>("DatabaseName")));


            services.AddScoped<IGardenRepo, GardenRepo>();
            services.AddScoped<ITreeRepo, TreeRepo>();
            services.AddScoped<IFruitRepo, FruitRepo>();
            services.AddScoped<IUoW, UoW>();

            return services;
        }
    }
}