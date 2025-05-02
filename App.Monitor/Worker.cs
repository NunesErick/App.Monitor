using Microsoft.Extensions.Options;
using Monitor.Service.Interfaces;

namespace App.Monitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMonitorService _monitor;

        public Worker(ILogger<Worker> logger, IMonitorService monitor)
        {
            _logger = logger;
            _monitor = monitor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Monitoramento iniciado às {data}", DateTimeOffset.Now);
            await _monitor.ActivateMonitor(stoppingToken);
        }
    }
}