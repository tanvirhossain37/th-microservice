using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TH.Common.Model;
using TH.EventBus.Messages;
using TH.ShadowMS.App.Services;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.App;

public static class AppDependencyInjection
{
    public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<Shadow>();
        services.AddScoped<ShadowViewModel>();
        services.AddScoped<ShadowFilterModel>();
        services.AddScoped<IShadowService, ShadowService>();
        services.AddScoped<ShadowEventConsumer>();
        services.AddScoped<ShadowEvent>();

        return services;
    }
}