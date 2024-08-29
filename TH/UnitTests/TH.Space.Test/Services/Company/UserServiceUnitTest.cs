using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class UserServiceUnitTest : CompanyBaseUnitTest
{
    private IUserService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IUserService>();
        base.LoginAsOwner(_service);
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new UserInputModel
            {
                SpaceId = "34e57033-58a7-40b8-a410-a1f47458ab98",
                CompanyId = "5b64c474-c813-4b8e-a471-ed0b04cf87eb",
                Name = "milon.roy@rite.com.bd",
                UserName = "milon.roy@rite.com.bd",
                AccessTypeId = (int)AccessTypeEnum.TenantAccess,
                UserTypeId = (int)UserTypeEnum.TenantUser
            };

            var entity = await _service.SaveAsync(Mapper.Map<UserInputModel, User>(model),true, DataFilter);
            var viewModel = Mapper.Map<User, UserViewModel>(entity);
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
            var model = new UserInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<UserInputModel, User>(model), DataFilter);
            var viewModel = Mapper.Map<User, UserViewModel>(entity);
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
            var model = new UserInputModel
            {
                Id = "", //todo
            };

            await _service.SoftDeleteAsync(Mapper.Map<UserInputModel, User>(model), DataFilter);
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
            var model = new UserInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<UserInputModel, User>(model), DataFilter);
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
            var filter = new UserFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<User, UserViewModel>(entity);
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
            var filter = new UserFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<User>, List<UserViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}