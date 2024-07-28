using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using TH.Common.Model;
using TH.CompanyMS.API;
using TH.CompanyMS.App;
using TH.CompanyMS.Infra;
using Microsoft.Extensions.Configuration;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace TH.CompanyMS.Test
{
    [TestClass]
    public class BaseUnitTest
    {
        protected ServiceProvider ServiceProvider;
        protected DataFilter DataFilter;
        protected Mapper Mapper;

        [TestInitialize]
        public virtual void Init()
        {
            var builder = WebApplication.CreateBuilder();

            var serviceCollection = new ServiceCollection();
            builder.Configuration.AddJsonFile("appsettings.json", true);


            serviceCollection.AddAppDependencyInjection(builder.Configuration);
            serviceCollection.AddInfraDependencyInjection(builder.Configuration);

            serviceCollection.AddDbContext<CompanyDbContext>(options => { options.UseSqlServer("Data Source=localhost;Initial Catalog=CompanyDB;User ID=sa;Password=admin123##;Trust Server Certificate=True"); });

            //RabbitMQ Config
            serviceCollection.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host("amqp://guest:guest@localhost:5672"); }); });


            serviceCollection.AddScoped<IConfiguration, ConfigurationManager>();
            serviceCollection.AddLogging();

            //AutoMapper
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());

            Mapper = new Mapper(config);

            DataFilter = new DataFilter
            {
                IncludeInactive = true
            };

        }
    }
}