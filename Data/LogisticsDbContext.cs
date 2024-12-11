using Microsoft.EntityFrameworkCore;
using LogisticsApi.Models;

namespace LogisticsApi.Data
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

        public DbSet<Waybill> Waybills { get; set; }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<PostalArea> PostalAreas { get; set; }
        public DbSet<DeliveryTask> DeliveryTasks { get; set; }
        public DbSet<RoutePlan> RoutePlans { get; set; }
        public DbSet<TaskWaybill> TaskWaybills { get; set; }
        public DbSet<User> Users { get; set; } // 用户表


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // 确保主键配置正确
        //     modelBuilder.Entity<Task>()
        //         .HasKey(t => t.Id); // 或 TaskId

        //     modelBuilder.Entity<Task>()
        //         .HasMany(t => t.Waybills)
        //         .WithOne(w => w.Task)
        //         .HasForeignKey(w => w.TaskId);
        // }

    }
}
