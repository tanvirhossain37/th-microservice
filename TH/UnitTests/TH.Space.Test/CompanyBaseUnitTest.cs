using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.Common.Model;
using TH.CompanyMS.API;
using TH.CompanyMS.App;
using TH.CompanyMS.Infra;
using Microsoft.EntityFrameworkCore;

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
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            serviceCollection.AddCompanyAppDependencyInjection(builder.Configuration);
            serviceCollection.AddCompanyInfraDependencyInjection(builder.Configuration);

            serviceCollection.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=CompanyDB;User ID=sa;Password=admin123##;Trust Server Certificate=True");
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
                cfg.AddProfile<CompanyMappingProfile>());

            Mapper = new Mapper(config);

            DataFilter = new DataFilter
            {
                IncludeInactive = true
            };
        }

        public void LoginAsOwner(IBaseService service)
        {
            var userResolver = new UserResolver();
            userResolver.UserName = "Tanvir.Hossain.f9dbd9f6de56";
            userResolver.SpaceId = "906d8ef9-4883-46e9-9c24-ade95ccc241c";
            userResolver.Name = "Tanvir Hossain";
            userResolver.Email = "tanvir.hossain37@gmail.com";

            service.SetUserResolver(userResolver);
        }
    }
}