using System.Reflection;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.AddressMS.API;
using TH.AddressMS.App;
using TH.AddressMS.Infra;
using TH.Common.Model;

namespace TH.AddressMS.Test
{
    [TestClass]
    public class AddressBaseUnitTest
    {
        protected ServiceProvider ServiceProvider;
        protected DataFilter DataFilter;
        protected Mapper Mapper;

        [TestInitialize]
        public virtual void Init()
        {
            var builder = WebApplication.CreateBuilder();

            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            serviceCollection.AddAddressApplicationServices(builder.Configuration);
            serviceCollection.AddAddressInfrastructureServices(builder.Configuration);

            serviceCollection.AddDbContext<AddressDbContext>(options =>
            {
                options.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=AddressDB;User ID=sa;Password=admin123##;Trust Server Certificate=True");
            });

            //RabbitMQ Config
            serviceCollection.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host("amqp://guest:guest@localhost:5672"); }); });
            
            //serviceCollection.AddScoped<IConfiguration, ConfigurationManager>();
            serviceCollection.AddLogging();

            //AutoMapper
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<AddressMappingProfile>());

            Mapper = new Mapper(config);

            DataFilter = new DataFilter
            {
                IncludeInactive = true
            };
        }

        public void LoginAsOwner(IBaseService service)
        {
            var userResolver = new UserResolver();
            userResolver.UserName = "Tanvir.Hossain.05d571270582";
            userResolver.SpaceId = "34e57033-58a7-40b8-a410-a1f47458ab98";
            userResolver.Name = "Tanvir Hossain";
            userResolver.Email = "tanvir.hossain37@gmail.com";

            service.SetUserResolver(userResolver);
        }
    }
}