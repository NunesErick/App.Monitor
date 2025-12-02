using App.Monitor;
using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Interfaces;
using Monitor.Domain.Repositories;
using Monitor.Infrastructure.Data.AppContext;
using Monitor.Infrastructure.Data.Interfaces;
using Monitor.Infrastructure.Data.Repositories;
using Monitor.Service.Interfaces;
using Monitor.Service.Repositories;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMonitor, MonitorInst>();
builder.Services.AddSingleton<IMonitorService, MonitorService>();
builder.Services.AddSingleton<IAppRepository, AppRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<ILogAppRepository, LogAppRepository>();
builder.Services.AddSingleton<IAppRepository, AppRepository>();
var host = builder.Build();
host.Run();