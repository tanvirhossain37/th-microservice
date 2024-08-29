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
                SpaceId = "34e57033-58a7-40b8-a410-a1f47458ab98"
            };
            model.Branches.Add(new BranchInputModel
            {
                Name = "Banani Branch",
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

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
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
            var filter = new CompanyFilterModel
            {
                SpaceId = "7efdd3d9-1520-44f2-88a7-a097049254f6"
            };
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