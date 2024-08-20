using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class PermissionServiceUnitTest : CompanyBaseUnitTest
{
    private IPermissionService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IPermissionService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new PermissionInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
            var viewModel = Mapper.Map<Permission, PermissionViewModel>(entity);
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
            var model = new PermissionInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
            var viewModel = Mapper.Map<Permission, PermissionViewModel>(entity);
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
            var model = new PermissionInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
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
            var model = new PermissionInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
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
            var filter = new PermissionFilterModel();

            var entity = await _service.FindAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Permission, PermissionViewModel>(entity);
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
            var filter = new PermissionFilterModel();
            filter.PageSize = (int)PageEnum.All;
            //filter.ByTree = true;
            filter.SpaceId = "f0f01ad3-d0fc-4baa-9fae-547ecf6cc71d";
            filter.CompanyId = "30b634a6-7c28-42c3-84e4-30afdc06042a";
            filter.UserName = "tanvir";
            filter.IsLastLevel = true;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Permission>, List<PermissionViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}