using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Interfaces;
using Monitor.Domain.Repositories;
using Monitor.Infrastructure.Data.AppContext.Models;
using Monitor.Infrastructure.Data.Repositories;
using Monitor.Service.Repositories;
using Moq;

namespace Monitor.Tests
{
    [TestClass]
    public class MonitorServiceTests : TestBase
    {
        [TestMethod]
        public async Task ActivateMonitor_ShouldStartMonitoringNewApps()
        {
            var mockMonitor = new Mock<IMonitor>();
            var apps = new List<App>
            {
                new App { Id = Guid.NewGuid(), Name = "App1", InteractionLink = "www.google.com.br" }
            };

            mockMonitor.Setup(m => m.GetApps()).ReturnsAsync(apps);
            mockMonitor.Setup(m => m.ActiveMonitor(It.IsAny<App>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

            var service = new MonitorService(mockMonitor.Object);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(10*1000);

            _ = service.ActivateMonitor(cts.Token);

            mockMonitor.Verify(m => m.GetApps(), Times.AtLeastOnce());
            mockMonitor.Verify(m => m.ActiveMonitor(It.Is<App>(a => a.Id == apps[0].Id), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task ShouldInsertStatusZeroWhenLinkIsDown()
        {
            var app = new App
            {
                Id = Guid.NewGuid(),
                Name = "App",
                InteractionLink = "http://nonexistentlink.test",
                CreationDate = DateTime.UtcNow
            };
            Context.Apps.Add(app);
            await Context.SaveChangesAsync();

            var appRepo = new AppRepository(Context);
            var logRepo = new LogAppRepository(Context);
            var monitor = new MonitorInst(appRepo, logRepo);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(10000);

            await monitor.ActiveMonitor(app, cts.Token);
            var logInserted = await Context.LogApp.FirstOrDefaultAsync(l => l.AppId == app.Id && l.Status == 0);
            Assert.IsNotNull(logInserted, "O log com status 0 não foi inserido pelo Monitor.");
        }

        [TestMethod]
        public async Task ShouldInsertStatusOneWhenLinkIsUp()
        {
            var app = new App
            {
                Id = Guid.NewGuid(),
                Name = "AppGoogle",
                InteractionLink = "https://www.google.com",
                CreationDate = DateTime.UtcNow
            };
            Context.Apps.Add(app);
            await Context.SaveChangesAsync();

            var appRepo = new AppRepository(Context);
            var logRepo = new LogAppRepository(Context);
            var monitor = new MonitorInst(appRepo, logRepo);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(10000);

            await monitor.ActiveMonitor(app, cts.Token);

            var logInserted = await Context.LogApp.FirstOrDefaultAsync(l => l.AppId == app.Id && l.Status == 1);
            Assert.IsNotNull(logInserted, "O log com status 1 não foi inserido pelo Monitor.");
        }

    }
}
