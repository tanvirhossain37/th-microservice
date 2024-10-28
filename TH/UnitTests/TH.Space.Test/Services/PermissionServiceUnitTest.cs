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
    public async Task ArchiveAsyncUnitTest()
    {
        try
        {
            var model = new PermissionInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
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

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
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
            filter.ByTree = true;
            filter.SpaceId = "4d3fe2d5-1047-4fb4-a4a3-f6b2bd1e73d6";
            filter.CompanyId = "01123375-abc5-4f62-8358-f973b53b10d0";
            filter.UserName = "Tanvir.Hossain.b2298f03e4b0";
            //filter.IsLastLevel = true;

            filter.SortFilters.Add(new SortFilter { PropertyName = "MenuOrder", Operation = OrderByEnum.Ascending });

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Permission>, List<PermissionViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}