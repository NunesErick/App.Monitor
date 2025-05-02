using App.Monitor;
using Monitor.Domain.Interfaces;
using Monitor.Infrastructure.Data.AppContext.Interfaces;
using Monitor.Infrastructure.Data.AppContext.Repositories;
using Monitor.Infrastructure.Data.Interfaces;
using Monitor.Infrastructure.Data.Repositories;
using Monitor.Service.Interfaces;
using Monitor.Service.Repositories;
using System.Data;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMonitor, Monitor.Domain.Repositories.Monitor>();
builder.Services.AddSingleton<IMonitorService, MonitorService>();
builder.Services.AddSingleton<IMonitorDAO, MonitorDAO>();
builder.Services.AddSingleton<IDbContext, DbContext>();
var host = builder.Build();
host.Run();