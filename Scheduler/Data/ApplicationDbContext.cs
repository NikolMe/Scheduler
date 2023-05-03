using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Models;

namespace Scheduler.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder
            .Entity<Service>()
            .HasMany(x => x.Places)
            .WithOne(x => x.Service)
            .HasForeignKey(x => x.ServiceId);

        modelBuilder
            .Entity<Place>();

        modelBuilder
            .Entity<IdentityUser>()
            .HasData(new[]
            {
                new IdentityUser()
                {
                    Id = "d9f7533c-267c-4d5e-9fe3-34d078bac07c",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEMagSRqe13G2UPkJwrYPcO3DrTIBWE8jdtAbo3JgJ/84ZUAuEQb5bKASkE2f5ab5Eg==",
                    UserName = "admin",
                    Email = "admin",
                    NormalizedEmail = "ADMIN",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = "PW7Q4CER4OQTVUXHWZSWLZVKJSCKL2LY",
                    ConcurrencyStamp = "9be049e0-7d71-4f09-8fc6-69071124d73c"
                }
            });
    }
    
    public DbSet<Scheduler.Models.Place> Place { get; set; }
    
    public DbSet<Scheduler.Models.Service> Service { get; set; }
}