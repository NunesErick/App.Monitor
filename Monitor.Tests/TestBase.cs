using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure.Data.AppContext;

namespace Monitor.Tests
{
    public class TestBase
    {
        protected AppDbContext Context { get; private set; }

        protected TestBase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            Context = new AppDbContext(options);

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
    }
}
