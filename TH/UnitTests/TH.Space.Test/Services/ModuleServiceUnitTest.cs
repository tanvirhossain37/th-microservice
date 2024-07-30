using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class ModuleServiceUnitTest : BaseUnitTest
{
    private IModuleService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IModuleService>();
    }

    [TestMethod]
    public async Task InitUnitTest()
    {
        try
        {
            var model = new ModuleInputModel
            {
            };

            await _service.InitAsync(DataFilter);
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
            var model = new ModuleInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<ModuleInputModel, Module>(model), DataFilter);
            var viewModel = Mapper.Map<Module, ModuleViewModel>(entity);
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
            var model = new ModuleInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<ModuleInputModel, Module>(model), DataFilter);
            var viewModel = Mapper.Map<Module, ModuleViewModel>(entity);
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
            var model = new ModuleInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<ModuleInputModel, Module>(model), DataFilter);
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
            var model = new ModuleInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<ModuleInputModel, Module>(model), DataFilter);
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
            var filter = new ModuleFilterModel();

            var entity = await _service.FindAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Module, ModuleViewModel>(entity);
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
            var filter = new ModuleFilterModel();
            filter.PageSize = (int)PageEnum.All;
            filter.ByTree = true;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModel = Mapper.Map<List<Module>, List<ModuleViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task InitAsync()
    {
        try
        {
            var model = new ModuleInputModel
            {
            };

            await _service.InitAsync(DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}