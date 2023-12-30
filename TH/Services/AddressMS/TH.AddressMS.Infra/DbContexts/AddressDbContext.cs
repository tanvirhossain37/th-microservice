using Microsoft.Extensions.Configuration;
using MongoRepo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AddressMS.Infra
{
    public class AddressDbContext : ApplicationDbContext
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        static string connectionString = configuration.GetConnectionString("AddressConStr");
        static string databaseName = configuration.GetValue<string>("DatabaseName");

        public AddressDbContext() : base(connectionString, databaseName)
        {
        }
    }
}