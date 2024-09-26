using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using TH.AddressMS.Core;
using TH.Io;

namespace TH.AddressMS.App
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAddressApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Address>();
            services.AddScoped<Country>();

            services.AddScoped<AddressFilterModel>();
            services.AddScoped<CountryFilterModel>();

            services.AddScoped<AddressInputModel>();
            services.AddScoped<CountryInputModel>();

            services.AddScoped<AddressViewModel>();
            services.AddScoped<CountryViewModel>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IExcelRepo, ExcelRepo>();
            services.AddScoped<CustomFile>();
            services.AddScoped<CountryExcel>();

            return services;
        }

        public static IServiceCollection AddAddressAppEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddEventBus(configuration);
            //RabbitMQ Config
            services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration.GetSection("EventBus:Host").Value); }); });

            return services;
        }
    }
}