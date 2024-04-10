using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using h2oAPI.Models;
namespace h2oAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
  
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Question> Questions{ get; set; }
    public DbSet<Score> Scores{ get; set; }
    public DbSet<LoginRequest> LoginRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define primary key for LoginRequest entity
        modelBuilder.Entity<LoginRequest>().HasKey(lr => lr.Id);

        // Additional configurations for other entities
        // ...

        base.OnModelCreating(modelBuilder);
    }

}
