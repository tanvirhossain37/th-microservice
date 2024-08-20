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
using AppDependencyInjection = TH.AuthMS.App.AppDependencyInjection;

namespace TH.CompanyMS.Test
{
    [TestClass]
    public class CompanyBaseUnitTest
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

            

            serviceCollection.AddCompanyAppDependencyInjection(builder.Configuration);
            serviceCollection.AddCompanyInfraDependencyInjection(builder.Configuration);

            serviceCollection.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=CompanyDB;User ID=sa;Password=admin123##;Trust Server Certificate=True");
            });

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