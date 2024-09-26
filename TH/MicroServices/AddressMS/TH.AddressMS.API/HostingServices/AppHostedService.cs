
using TH.AddressMS.Infra;

namespace TH.AddressMS.API
{
    public class AppHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //AddressDbContextSeed.Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
