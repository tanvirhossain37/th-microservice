using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.AddressMS.App;
using TH.AddressMS.Core;
using TH.Common.Lang;

namespace TH.AddressMS.Test;

[TestClass]
public class CountryServiceUnitTest : AddressBaseUnitTest
{
    private ICountryService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<ICountryService>();
        base.LoginAsOwner(_service);
    }

    [TestMethod]
    public async Task InitAsyncUnitTest()
    {
        try
        {
            var result = await _service.TryInitAsync("C:\\Users\\Tanvir Hossain\\Desktop\\Countries\\Country.xlsx");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var models = new List<CountryInputModel>();
            models.Add(new CountryInputModel
                { Name = "c_bangladesh", Code = "880", IsoCode = "BD", CurrencyName = "cr_bangladeshi_taka", CurrencyCode = "BDT" });
            models.Add(new CountryInputModel
                { Name = "c_united_states", Code = "1", IsoCode = "US", CurrencyName = "cr_united_states_dollar", CurrencyCode = "USD" });
            models.Add(new CountryInputModel
                { Name = "c_united_states", Code = "1", IsoCode = "US", CurrencyName = "cr_united_states_dollar", CurrencyCode = "USD" });

            foreach (var model in models)
            {
                var entity = await _service.SaveAsync(Mapper.Map<CountryInputModel, Country>(model), DataFilter);
                var viewModel = Mapper.Map<Country, CountryViewModel>(entity);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task UpdateAsyncUnitTest()
    {
        try
        {
            var model = new CountryInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<CountryInputModel, Country>(model), DataFilter);
            var viewModel = Mapper.Map<Country, CountryViewModel>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task ArchiveAsyncUnitTest()
    {
        try
        {
            var model = new CountryInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<CountryInputModel, Country>(model), DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task DeleteAsyncUnitTest()
    {
        try
        {
            var model = new CountryInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<CountryInputModel, Country>(model), DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task FindByIdAsyncUnitTest()
    {
        try
        {
            var filter = new CountryFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Country, CountryViewModel>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task GetAsyncUnitTest()
    {
        try
        {
            var filter = new CountryFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Country>, List<CountryViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}