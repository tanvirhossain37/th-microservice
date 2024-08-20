using Google.Api;
using Microsoft.Extensions.DependencyInjection;
using TH.AuthMS.App;
using TH.Common.Util;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Test;

[TestClass]
public class AuthServiceUnitTest : AuthBaseUnitTest
{
    private IAuthService _service;

    [TestInitialize]
    public override void Init()
    {
        base.Init();
        _service = ServiceProvider.GetRequiredService<IAuthService>();
        base.LoginAsOwner(_service);
    }

    [TestMethod]
    public async Task SignUpAsyncUnitTest()
    {
        try
        {
            ////self
            //var model = new SignUpInputModel
            //{
            //    Name = "Tanvir Hossain",
            //    UserName = "",
            //    Password = "admin123##",
            //    Email = "tanvir.hossain37@gmail.com",
            //};

            ////self
            //var model = new SignUpInputModel
            //{
            //    Name = "Rizwan Abedin",
            //    UserName = "",
            //    Password = "admin123##",
            //    Email = "rizwan.abedin@rite.com.bd",
            //};

            ////invitation
            //var email = "milon.roy@rite.com.bd";
            //var model = new SignUpInputModel
            //{
            //    Name = email,
            //    UserName = Util.TryGenerateUserName(email.Split("@")[0]),
            //    Password = Util.TryGenerateCode(),
            //    Email = email,
            //    ReferralId = "Tanvir.Hossain.dc00dbb85a13"
            //};

            //invitation
            var email = "milon.roy@rite.com.bd";
            var model = new SignUpInputModel
            {
                Name = email,
                UserName = Util.TryGenerateUserName(email.Split("@")[0]),
                Password = Util.TryGenerateCode(),
                Email = email,
                ReferralId = "Rizwan.Abedin.1bb6291c3054"
            };

            var entity = await _service.SignUpAsync(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task SignInAsyncUnitTest()
    {
        try
        {
            var model = new SignInInputModel
            {
                Email = "tanvir.hossain37@gmail.com",
                Password = "T@nv!r.2020"//admin123##
            };

            //var model = new SignInInputModel
            //{
            //    Email = "milon.roy@rite.com.bd",
            //    Password = "admin123##"
            //};

            var entity = await _service.SignInAsync(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task ForgotPasswordAsyncUnitTest()
    {
        try
        {
            var model = new ForgotPasswordInputModel
            {
                Email = "tanvir.hossain37@gmail.com"
            };

            await _service.ForgotPasswordAsync(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task UpdatePasswordAsyncUnitTest()
    {
        try
        {
            var model = new ForgotPasswordInputModel
            {
                Email = "tanvir.hossain37@gmail.com",
                Token = "CfDJ8OpASa8Zst5Nh5yOH2tPx1ap04b6lYdAoixFI7H8VovFnuaD8slqUobZriZx9EekB+dZClFkG9rZjMeE4pBBmUQ3NaI+na0MSqpE/WtZkW+mRkPkQjX58aq+knHFYj+ylhuy6XO07EM/QZbASNkVdIGdVVqKuBHnf8YsvVTLcerNwtNXfI2JD3mjdsPq9PSN9F0hMJ9WUIBggCU4iMBPxgCwKvscHboKj3hAUNNCkCkn",
                Password = "T@nv!r.2020"
            };

            await _service.UpdatePasswordAsync(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}