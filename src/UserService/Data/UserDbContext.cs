using Microsoft.EntityFrameworkCore;
using UserService.Models.Entities;

namespace UserService.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "$2a$13$nMC9daSgGPQk8K06km.yTen8kkt8tI1t3jDfLW07u0SdDpylim8fS",
                    FirstName = "System",
                    LastName = "Admin",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 7, 14), 
                    UpdatedAt = new DateTime(2025, 7, 14)
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    Email = "user1@example.com",
                    PasswordHash = "$2a$13$nMC9daSgGPQk8K06km.yTen8kkt8tI1t3jDfLW07u0SdDpylim8fS",
                    FirstName = "John",
                    LastName = "Doe",
                    Role = UserRole.User,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 7, 14), 
                    UpdatedAt = new DateTime(2025, 7, 14)
                }
            );
        }
    }
}