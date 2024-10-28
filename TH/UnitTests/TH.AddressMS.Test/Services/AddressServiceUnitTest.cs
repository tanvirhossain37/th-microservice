using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TH.AddressMS.App;
using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.Test;

[TestClass]
public class AddressServiceUnitTest : AddressBaseUnitTest
{
    private IAddressService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IAddressService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new AddressInputModel
            {
                ClientId = "2df17aae-bb75-4603-8ee5-c4d777a493f8",
                Street = "Uttara",
                City = "Dhaka",
                PostalCode = "1229",
                CountryId = "e3805a4f-6eee-4884-b2d2-0dcb728b58a6",//bd
                IsDefault = true
            };

            var entity = await _service.SaveAsync(Mapper.Map<AddressInputModel, Address>(model), DataFilter);
            var viewModel = Mapper.Map<Address, AddressViewModel>(entity);
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
            var model = new AddressInputModel
            {
                Id= "b1856946-a386-4c5a-88ba-1fae11635d0c",
                ClientId = "2df17aae-bb75-4603-8ee5-c4d777a493f8",
                Street = "Gulshan Chattar - 2",
                City = "Dhaka",
                PostalCode = "1206",
                CountryId = "e3805a4f-6eee-4884-b2d2-0dcb728b58a6",//bd
                IsDefault = true
            };

            var entity = await _service.UpdateAsync(Mapper.Map<AddressInputModel, Address>(model), DataFilter);
            var viewModel = Mapper.Map<Address, AddressViewModel>(entity);
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
            var model = new AddressInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<AddressInputModel, Address>(model), DataFilter);
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
            var model = new AddressInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<AddressInputModel, Address>(model), DataFilter);
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
            var filter = new AddressFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Address, AddressViewModel>(entity);
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
            var filter = new AddressFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Address>, List<AddressViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}