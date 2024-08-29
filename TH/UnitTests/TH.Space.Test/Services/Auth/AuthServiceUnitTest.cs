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
            //    UserName = "tanvir.hossain37@gmail.com",
            //    Password = "admin123##",
            //    Email = "tanvir.hossain37@gmail.com",
            //    IsAutoUserName = true
            //};

            ////self
            //var model = new SignUpInputModel
            //{
            //    Name = "Rizwan Abedin",
            //    UserName = "rizwan.abedin@rite.com.bd",
            //    Password = "admin123##",
            //    Email = "rizwan.abedin@rite.com.bd",
            //    IsAutoUserName = true
            //};

            //invitation
            var email = "milon.roy@rite.com.bd";
            var model = new SignUpInputModel
            {
                Name = email,
                UserName = Util.TryGenerateUserName(email),
                Password = Util.TryGenerateCode(),
                Email = email,
                ReferralId = "Tanvir.Hossain.05d571270582",//milon.roy.481352d59395
                CompanyName = "Tesla Inc.",
                IsAutoUserName = false
            };

            ////invitation
            //var email = "shiplu.drive7@gmail.com";
            //var model = new SignUpInputModel
            //{
            //    Name = email,
            //    UserName = Util.TryGenerateUserName(email.Split("@")[0]),
            //    Password = Util.TryGenerateCode(),
            //    Email = email,
            //    ReferralId = "Rizwan.Abedin.ed6027e94259",
            //    CompanyName = "Google Inc.",
            //    IsAutoUserName = false
            //};

            var entity = await _service.SignUpAsync(model, DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task ActivateAccountAsyncUnitTest()
    {
        try
        {
            //self
            var model = new ActivationCodeInputModel
            {
                ActivateCode = "1d3a69476f7c"
            };

            var entity = await _service.ActivateAccountAsync(model, DataFilter);
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
                Password = "admin123##"//
            };

            //var model = new SignInInputModel
            //{
            //    Email = "milon.roy@rite.com.bd",
            //    Password = "admin123##"
            //};

            var entity = await _service.SignInAsync(model, DataFilter);
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

            //var model = new ForgotPasswordInputModel
            //{
            //    Email = "milon.roy@rite.com.bd"
            //};

            await _service.ForgotPasswordAsync(model, DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task ResetPasswordAsyncUnitTest()
    {
        try
        {
            var model = new ForgotPasswordInputModel
            {
                //Email = "tanvir.hossain37@gmail.com",
                ActivationCode = "CfDJ8OpASa8Zst5Nh5yOH2tPx1YGmTnHx6LfZaP4SDspFTRT0S2ucn1vVDwSJVXAseK4rfWbKG9xWvsIvckibZ34vzHevrYJjEgiPbkx7KT5uZjcpRzEfp2vY44tm0cXSRdJ3x9NIfq2ZEY5HQk%2bEFV7ZtYUXOPlYH0W4GfEQ%2bj1Hz0begqlO5tizwKY8Im1miECHEzRsl36xOdUzcD05o2XZUpx66T%2fUIF8gxXPNwlUvmQw",
                Password = "T@nv!r.2020"
            };

            await _service.ResetPasswordAsync(model, DataFilter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [TestMethod]
    public async Task FindByEmailAsync()
    {
        try
        {
            var model = new ApplicationUserFilterModel
            {
                Email = "tanvir.hossain37@gmail.com"
            };

            var applicationUser = await _service.FindByEmailAsync(model, DataFilter);
            var viewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(applicationUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}