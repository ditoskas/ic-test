using IcTest.Abstractions.BackgroundServices;
using IcTest.Shared.Constants;
using Microsoft.Extensions.Hosting;

namespace IcTest.Infrastructure.BackgroundServices
{
    public class BlocksImporterService : BackgroundService, IBaseBackgroundService
    {
        private readonly bool _isRunning = false;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public string Status => _isRunning ? BackgroundServiceStatus.Running : BackgroundServiceStatus.Stopped;
    }
}
