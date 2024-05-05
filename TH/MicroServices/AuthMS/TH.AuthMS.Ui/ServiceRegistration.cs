using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TH.AuthMS.Ui.Data;

namespace TH.AuthMS.Ui;

public static class ServiceRegistration
{
    public static IServiceCollection AddAllDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

        services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
        //Identity
        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 0;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    //public static async Task<IApplicationBuilder> SeedDataAsync(this WebApplication app)
    //{
    //    using (var scope=app.Services.CreateScope())
    //    {
    //        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
    //        await context.Database.EnsureDeletedAsync();
    //        //if (await context.Database.EnsureCreatedAsync())
    //        //{

    //        //}
    //    }

    //    return app;
    //}
}