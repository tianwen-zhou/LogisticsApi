using Microsoft.EntityFrameworkCore;
using LogisticsApi.Models;

namespace LogisticsApi.Data
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<PostalArea> PostalAreas { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<MergeTask> MergeTasks { get; set; }
        public DbSet<RoutePlan> RoutePlans { get; set; }
    }
}
