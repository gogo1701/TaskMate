using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMate.Domain.Entities;

namespace TaskMate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Device-User relationship: One ApplicationUser can have many Devices
            modelBuilder.Entity<Device>()
                .HasOne(d => d.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict instead of Cascade

            modelBuilder.Entity<Domain.Entities.Task>()
                .HasOne(t => t.Device)
                .WithMany(d => d.Tasks)
                .HasForeignKey(t => t.DeviceId)
                .OnDelete(DeleteBehavior.Cascade); // Use Cascade for Task-Device relationship

            // Ensure correct table mapping
            modelBuilder.Entity<Device>().ToTable("Devices");
            modelBuilder.Entity<Domain.Entities.Task>().ToTable("Task");
        }
    }
}
