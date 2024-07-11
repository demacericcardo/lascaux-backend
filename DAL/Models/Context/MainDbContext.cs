using DAL.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models.Context
{
    public class MainDbContext(DbContextOptions<MainDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Film> Films { get; set; } = null!;
        public DbSet<Screen> Screens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            this.ChangeTracker.DetectChanges();

            object[] added = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToArray();

            foreach (object entity in added)
            {
                if (entity is IBaseEntity)
                {
                    if (entity is IBaseEntity track)
                    {
                        DateTime utcNow = DateTime.UtcNow;
                        TimeZoneInfo italianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        DateTime italianTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, italianTimeZone);
                        track.CreatedDate = italianTime;
                    }
                }
            }

            object[] modified = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToArray();

            foreach (object entity in modified)
            {
                if (entity is IBaseEntity)
                {
                    if (entity is IBaseEntity track)
                    {
                        DateTime utcNow = DateTime.UtcNow;
                        TimeZoneInfo italianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        DateTime italianTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, italianTimeZone);
                        track.UpdatedDate = italianTime;
                    }
                }
            }

            return base.SaveChangesAsync(cancellation);
        }
    }
}
