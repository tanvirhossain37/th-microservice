using Microsoft.Extensions.DependencyInjection;
using TH.Common.Lang;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class UserCompanyServiceUnitTest : CompanyBaseUnitTest
{
    private IUserCompanyService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IUserCompanyService>();
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new UserCompanyInputModel
            {
            };

            var entity = await _service.SaveAsync(Mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
            var viewModel = Mapper.Map<UserCompany, UserCompanyViewModel>(entity);
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
            var model = new UserCompanyInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
            var viewModel = Mapper.Map<UserCompany, UserCompanyViewModel>(entity);
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
            var model = new UserCompanyInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
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
            var model = new UserCompanyInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
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
            var filter = new UserCompanyFilterModel
            {
                CompanyId = "64937385-d888-455b-9450-5fa07272ab5a",
                
            };

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<UserCompany, UserCompanyViewModel>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task FindByCompanyIdAsyncUnitTest()
    {
        try
        {
            var filter = new UserCompanyFilterModel
            {
                CompanyId = "b92fd91f-7ffb-49b8-a995-f366706b967d",

            };

            var entity = await _service.FindByCompanyIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<UserCompany, UserCompanyViewModel>(entity);
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
            var filter = new UserCompanyFilterModel
            {
                UserName = "Tanvir.Hossain.1a75866a9f40",
                StatusId = (int)InvitationStatusEnum.Accept
            };
            filter.PageSize = (int)PageEnum.All;
            Lang.SetCultureCode("bn-BD");
            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<UserCompany>, List<UserCompanyViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}