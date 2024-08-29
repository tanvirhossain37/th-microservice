using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class BranchUserServiceUnitTest : CompanyBaseUnitTest
{
    private IBranchUserService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IBranchUserService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new BranchUserInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
            var viewModel = Mapper.Map<BranchUser, BranchUserViewModel>(entity);
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
            var model = new BranchUserInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
            var viewModel = Mapper.Map<BranchUser, BranchUserViewModel>(entity);
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
            var model = new BranchUserInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
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
            var model = new BranchUserInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
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
            var filter = new BranchUserFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<BranchUser, BranchUserViewModel>(entity);
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
            var filter = new BranchUserFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<BranchUser>, List<BranchUserViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}