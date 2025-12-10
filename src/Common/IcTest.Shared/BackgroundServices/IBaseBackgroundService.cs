using Microsoft.Extensions.Hosting;

namespace IcTest.Shared.BackgroundServices
{
    public interface IBaseBackgroundService : IHostedService
    {
        string Status { get; }
    }
}
