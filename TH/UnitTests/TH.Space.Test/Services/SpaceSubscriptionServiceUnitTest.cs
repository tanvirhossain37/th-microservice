using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class SpaceSubscriptionServiceUnitTest : CompanyBaseUnitTest
{
    private ISpaceSubscriptionService _service;


    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<ISpaceSubscriptionService>();
        base.LoginAsOwner(_service);
    }

    [TestMethod]
    public async Task SaveAsyncUnitTest()
    {
        try
        {
            var model = new SpaceSubscriptionInputModel
            {
                SpaceId = "906d8ef9-4883-46e9-9c24-ade95ccc241c",
                PlanId = (int)SubscriptionEnum.FreePlan,
                IsCurrent = true
            };

            var entity = await _service.SaveAsync(Mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
            var viewModel = Mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(entity);
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
            var model = new SpaceSubscriptionInputModel
            {
            };

            var entity = await _service.UpdateAsync(Mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
            var viewModel = Mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(entity);
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
            var model = new SpaceSubscriptionInputModel
            {
                Id = "", //todo
            };

            await _service.ArchiveAsync(Mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
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
            var model = new SpaceSubscriptionInputModel
            {
                Id = "" //todo
            };

            await _service.DeleteAsync(Mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
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
            var filter = new SpaceSubscriptionFilterModel();

            var entity = await _service.FindByIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task FindBySpaceIdAsyncUnitTest()
    {
        try
        {
            var filter = new SpaceSubscriptionFilterModel
            {
                SpaceId = "906d8ef9-4883-46e9-9c24-ade95ccc241c"
            };

            var entity = await _service.FindBySpaceIdAsync(filter, DataFilter); //todo
            var viewModel = Mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(entity);
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
            var filter = new SpaceSubscriptionFilterModel();
            filter.PageSize = (int)PageEnum.All;

            var entity = await _service.GetAsync(filter, DataFilter);
            var viewModels = Mapper.Map<List<SpaceSubscription>, List<SpaceSubscriptionViewModel>>(entity.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}