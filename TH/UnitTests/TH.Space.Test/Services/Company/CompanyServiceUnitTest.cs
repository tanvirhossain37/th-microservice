using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class CompanyServiceUnitTest : CompanyBaseUnitTest
{
    private ICompanyService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<ICompanyService>();
        base.LoginAsOwner(_service);
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new CompanyInputModel
            {
                Name = "Google Inc.",
                SpaceId = "482a7084-3278-46d3-be3f-8f5ced154c34"
            };
            model.Branches.Add(new BranchInputModel
            {
                Name = "LA Branch",
                IsDefault = true
            });

            var entity = await _service.SaveAsync(Mapper.Map<CompanyInputModel, Company>(model), DataFilter);
            var viewModel = Mapper.Map<Company, CompanyViewModel>(entity);
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
            var model = new CompanyInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<CompanyInputModel, Company>(model), DataFilter);
            var viewModel = Mapper.Map<Company, CompanyViewModel>(entity);
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
            var model = new CompanyInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<CompanyInputModel, Company>(model), DataFilter);
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
            var model = new CompanyInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<CompanyInputModel, Company>(model), DataFilter);
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
            var filter = new CompanyFilterModel();

            var entity = await _service.FindAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<Company, CompanyViewModel>(entity);
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
            var filter = new CompanyFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<Company>, List<CompanyViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}