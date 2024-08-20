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

            ////invitation
            //var email = "milon.roy@rite.com.bd";
            //var model = new SignUpInputModel
            //{
            //    Name = "Tanvir Hossain",
            //    UserName = Util.TryGenerateUserName(email.Split("@")[0]),
            //    Password = Util.TryGenerateCode(),
            //    Email = email,
            //    ReferralId = "Tanvir.Hossain.343c2cdfb836"
            //};

            //invitation
            var email = "milon.roy@rite.com.bd";
            var model = new SignUpInputModel
            {
                Name = "Rizu Bhai",
                UserName = Util.TryGenerateUserName(email.Split("@")[0]),
                Password = Util.TryGenerateCode(),
                Email = email,
                ReferralId = "Rizu.Bhai.343c2cdfb836"
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
                Password = "admin123##"
            };

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
                Token = "CfDJ8OpASa8Zst5Nh5yOH2tPx1YuHI8Bm6hV28QBAAB1uqIU1BLVURK/1EfTpAL2D7ivu8wGcvjkF/KfxAqH/OE4tg/TAFXp4blapHYB8a27CEq2lfIj9+tPriQ7eU7xVW919EeE2xOMLbG32aOqLzEaAiRpfXRHe/ev694qzqALt9yGLmuDOAE2gd0LxMm4krzw1E4rd13w7DKyTwdlDfl9zsMw/wpc9x3nLSit9hDuEtEG",
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