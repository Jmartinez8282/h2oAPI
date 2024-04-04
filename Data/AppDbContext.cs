using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace h2oAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams {get; set;}

    public DbSet<Question> Questions {get; set;}

    public DbSet<Score> Scores {get; set;}

    public AddDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}
