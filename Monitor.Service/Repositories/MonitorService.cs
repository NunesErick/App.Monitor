using Monitor.Domain.Interfaces;
using Monitor.Infrastructure.DTO;
using Monitor.Service.Interfaces;

namespace Monitor.Service.Repositories
{
    public class MonitorService : IMonitorService
    {
        private readonly IMonitor _monitor;
        private List<AppDTO> _apps = new List<AppDTO>();
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
                    Console.WriteLine("Iniciado monitoramento do "+app.Nome);
                    _apps.Add(app);
                    _ = _monitor.ActiveMonitor(app, stopToken);
                }

                await Task.Delay(180000,stopToken);
            }
        }
    }
}