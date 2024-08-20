using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class RoleServiceUnitTest : CompanyBaseUnitTest
{
    private IRoleService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IRoleService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new RoleInputModel
            {
                Name = "Executive",
                SpaceId = "f0f01ad3-d0fc-4baa-9fae-547ecf6cc71d",
                CompanyId = "be1cca01-ce7f-4512-8ece-fd05a43d12d3"
            };

            var entity = await _service.SaveAsync(Mapper.Map<RoleInputModel, Role>(model), DataFilter);
            var viewModel = Mapper.Map<Role, RoleViewModel>(entity);
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
            var model = new RoleInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<RoleInputModel, Role>(model), DataFilter);
            var viewModel = Mapper.Map<Role, RoleViewModel>(entity);
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
            var model = new RoleInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<RoleInputModel, Role>(model), DataFilter);
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
            var model = new RoleInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<RoleInputModel, Role>(model), DataFilter);
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
            var filter = new RoleFilterModel();

            var entity = await _service.FindAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Role, RoleViewModel>(entity);
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
            var filter = new RoleFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Role>, List<RoleViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}