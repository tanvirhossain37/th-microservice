using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.AddressMS.App;
using TH.Repo;

namespace TH.AddressMS.Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddAddressInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AddressDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderDb")));
            services.AddScoped<AddressDbContext>();
            services.AddScoped<ICustomSort, CustomSort>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<ICountryRepo, CountryRepo>();
            services.AddScoped<IUow, Uow>();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AddressDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AddressDB"));
                //.UseLazyLoadingProxies()
                //.UseChangeTrackingProxies();
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            return services;
        }
    }
}