using ContractMonthlyClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet for Claims
    public DbSet<Claim> Claims { get; set; }

    // DbSet for Users
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring Claim entity
        modelBuilder.Entity<Claim>()
            .Property(c => c.Amount)
            .HasColumnType("decimal(18,2)");  // Specify precision and scale

        // Establish relationship between Claim and User (e.g., a user can submit many claims)
        modelBuilder.Entity<Claim>()
            .HasOne(c => c.User)
            .WithMany(u => u.Claims)  
            .HasForeignKey(c => c.UserId);  // Foreign key property in Claim

        // Seed initial data 
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "lecturer", Password = "lecturer123", Role = "Lecturer" }
        );
    }
}
