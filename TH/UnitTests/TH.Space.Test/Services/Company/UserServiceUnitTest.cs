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
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new UserInputModel
            {
                SpaceId = "8dd667b5-1080-4f4f-9f24-48aaad6cb8b2",
                CompanyId = "a7255642-384a-4919-a133-4b0eedd450b8",
                Name = "milon.roy@rite.com.bd",
                UserName = "milon.roy@rite.com.bd",
                AccessTypeId = (int)AccessTypeEnum.TenantAccess,
                UserTypeId = (int)UserTypeEnum.TenantUser
            };

            var entity = await _service.SaveAsync(Mapper.Map<UserInputModel, User>(model), DataFilter);
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

            var entity = await _service.FindAsync(filter, DataFilter); //todo
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