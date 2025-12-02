using Monitor.Domain.Interfaces;
using Monitor.Infrastructure.Data.AppContext.Models;
using Monitor.Infrastructure.Data.Interfaces;

namespace Monitor.Domain.Repositories
{
    public class MonitorInst : IMonitor
    {
        private readonly IAppRepository _appRep;
        private readonly ILogAppRepository _logRep;
        public MonitorInst(IAppRepository appRep, ILogAppRepository logRep)
        {
            _appRep = appRep;
            _logRep = logRep;
        }
        public async Task ActiveMonitor(App app, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int status;
                    try
                    {
                        var response = await httpClient.GetAsync(app.InteractionLink, cancellationToken);
                        status = response.IsSuccessStatusCode ? 1 : 0;
                    }
                    catch (Exception)
                    {
                        status = 0;
                    }

                    var log = new LogApp
                    {
                        Id = Guid.NewGuid(),
                        AppId = app.Id,
                        Status = status,
                        DateLogged = DateTime.UtcNow
                    };

                    await _logRep.InsertLogAsync(log);

                    await Task.Delay(60000, cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                // Monitoring was cancelled
            }
        }

        public async Task<List<App>> GetApps()
        {
            return await _appRep.GetApps();
        }
    }
}