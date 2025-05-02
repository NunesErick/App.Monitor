using Monitor.Domain.Interfaces;

namespace Monitor.Domain.Repositories
{
    public class Metrics : IMetrics
    {
        public Task CollectSystemMetricsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
