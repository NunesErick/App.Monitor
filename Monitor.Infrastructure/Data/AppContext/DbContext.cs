using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure.Data.AppContext.Models;

namespace Monitor.Infrastructure.Data.AppContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<App> Apps { get; set; }
        public DbSet<LogApp> LogApp { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<App>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.InteractionLink).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreationDate).IsRequired();
            });
            modelBuilder.Entity<LogApp>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AppId).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.DateLogged).IsRequired();
                entity.HasOne(e => e.App)
                      .WithMany()
                      .HasForeignKey(e => e.AppId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}