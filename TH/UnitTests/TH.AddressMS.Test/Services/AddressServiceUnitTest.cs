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
    public async Task SoftDeleteAsyncUnitTest()
    {
        try
        {
            var model = new AddressInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<AddressInputModel, Address>(model), DataFilter);
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