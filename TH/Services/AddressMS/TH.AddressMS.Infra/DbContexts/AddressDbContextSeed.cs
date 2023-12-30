using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.Infra
{
    public class AddressDbContextSeed
    {
        static ICountryService _countryService = new CountryService(new CountryRepo(new AddressDbContext()));

        public async static void Seed()
        {
            var countries = await _countryService.GetAllAsync();
            if (!countries.Any())
            {
                var list = GetPreConfiguredCountries();
                foreach (var country in list)
                {
                    await _countryService.SaveAsync(country);
                }
            }
        }

        private static List<Country> GetPreConfiguredCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Name = "Bangladesh"
                },
                new Country()
                {
                    Name = "Sweden"
                }
            };
        }
    }
}