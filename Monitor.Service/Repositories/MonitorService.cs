using Monitor.Domain.Interfaces;
using Monitor.Infrastructure.Data.AppContext.Models;
using Monitor.Service.Interfaces;

namespace Monitor.Service.Repositories
{
    public class MonitorService : IMonitorService
    {
        private readonly IMonitor _monitor;
        private List<App> _apps = new List<App>();
        public MonitorService(IMonitor monitor)
        {
            _monitor = monitor;
        }
        public async Task ActivateMonitor(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                var apps = await _monitor.GetApps();
                var newApps = apps.Where(app => !_apps.Any(existingApp => existingApp.Id == app.Id)).ToList();
                foreach (var app in newApps)
                {
                    Console.WriteLine($"Monitoring started for {app.Name}");
                    _apps.Add(app);
                    _ = _monitor.ActiveMonitor(app, stopToken);
                }

                await Task.Delay(10000,stopToken); //TODO: Implement a better way to check for new apps
            }
        }
    }
}