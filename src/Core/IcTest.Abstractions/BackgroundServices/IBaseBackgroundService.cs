using Microsoft.Extensions.Hosting;

namespace IcTest.Abstractions.BackgroundServices
{
    public interface IBaseBackgroundService : IHostedService
    {
        string Status { get; }
    }
}
