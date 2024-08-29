using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class BranchServiceUnitTest : CompanyBaseUnitTest
{
    private IBranchService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IBranchService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new BranchInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<BranchInputModel, Branch>(model), DataFilter);
            var viewModel = Mapper.Map<Branch, BranchViewModel>(entity);
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
            var model = new BranchInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<BranchInputModel, Branch>(model), DataFilter);
            var viewModel = Mapper.Map<Branch, BranchViewModel>(entity);
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
            var model = new BranchInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<BranchInputModel, Branch>(model), DataFilter);
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
            var model = new BranchInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<BranchInputModel, Branch>(model), DataFilter);
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
            var filter = new BranchFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Branch, BranchViewModel>(entity);
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
            var filter = new BranchFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Branch>, List<BranchViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}