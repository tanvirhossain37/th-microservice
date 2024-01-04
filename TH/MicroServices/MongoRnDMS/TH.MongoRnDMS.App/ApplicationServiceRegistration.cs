using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.App
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGardenService, GardenService>();
            services.AddScoped<ITreeService, TreeService>();
            services.AddScoped<IFruitService, FruitService>();

            return services;
        }
    }
}