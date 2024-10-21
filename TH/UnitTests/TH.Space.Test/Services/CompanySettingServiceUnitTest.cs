using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class CompanySettingServiceUnitTest : CompanyBaseUnitTest
{
    private ICompanySettingService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<ICompanySettingService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new CompanySettingInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
            var viewModel = Mapper.Map<CompanySetting, CompanySettingViewModel>(entity);
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
            var model = new CompanySettingInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
            var viewModel = Mapper.Map<CompanySetting, CompanySettingViewModel>(entity);
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
            var model = new CompanySettingInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
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
            var model = new CompanySettingInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
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
            var filter = new CompanySettingFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<CompanySetting, CompanySettingViewModel>(entity);
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
            var filter = new CompanySettingFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<CompanySetting>, List<CompanySettingViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}