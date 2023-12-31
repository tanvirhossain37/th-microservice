using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;


namespace TH.AddressMS.Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AddressDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderDb")));
            services.AddScoped<AddressDbContext>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<ICountryRepo, CountryRepo>();
            return services;
        }
    }
}