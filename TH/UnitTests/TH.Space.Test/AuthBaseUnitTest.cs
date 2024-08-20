using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.AuthMS.API;
using TH.Common.Model;
using TH.AuthMS.App;
using TH.AuthMS.Infra;
using System.Configuration;

namespace TH.CompanyMS.Test
{
    [TestClass]
    public class AuthBaseUnitTest
    {
        protected ServiceProvider ServiceProvider;
        protected DataFilter DataFilter;
        protected Mapper Mapper;

        [TestInitialize]
        public virtual void Init()
        {
            var builder = WebApplication.CreateBuilder();

            var serviceCollection = new ServiceCollection();
            //builder.Configuration.AddJsonFile("appsettings.json", true);

            //var configurationRoot = builder.Configuration.AddJsonFile("appsettings.json").Build();
            IConfiguration Configuration =
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables()
                    .Build();

        serviceCollection.AddAuthAppDependencyInjection(builder.Configuration);
            serviceCollection.AddAuthInfraDependencyInjection(builder.Configuration);
            serviceCollection.AddAuthJwtTokenBasedAuthentication(builder.Configuration);

            //RabbitMQ Config
            serviceCollection.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host("amqp://guest:guest@localhost:5672"); }); });

            var builderConfiguration = builder.Configuration;

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

        public void LoginAsOwner(IBaseService service)
        {
            var userResolver = new UserResolver();
            userResolver.UserName = "tanvir";
            userResolver.SpaceId = "f0f01ad3-d0fc-4baa-9fae-547ecf6cc71d";
            userResolver.FullName = "Tanvir Hossain";

            service.SetUserResolver(userResolver);
        }
    }
}