using Monitor.Domain.Interfaces;
using Monitor.Infrastructure.Data.Interfaces;
using Monitor.Infrastructure.DTO;

namespace Monitor.Domain.Repositories
{
    public class Monitor : IMonitor
    {
        private readonly IMonitorDAO _rep;
        public Monitor(IMonitorDAO rep)
        {
            _rep = rep;
        }
        public async Task ActiveMonitor(AppDTO app, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var response = await httpClient.GetAsync(app.LinkInteracao, cancellationToken);
                    int status = response.IsSuccessStatusCode ? 1 : 0;

                    await _rep.InsertStatus(app, status);
                }
                catch (Exception ex)
                {
                    await _rep.InsertStatus(app, 0);
                }

                await Task.Delay(60000, cancellationToken);
            }
        }
        public async Task<List<AppDTO>> GetApps()
        {
            return await _rep.GetApps();
        }
    }
}