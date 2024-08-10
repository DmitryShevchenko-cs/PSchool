using Microsoft.EntityFrameworkCore;
using PSchool.DAL.Entities;

namespace PSchool.DAL;

public class PSchoolDbContext(DbContextOptions<PSchoolDbContext> options) : DbContext(options)
{

    public DbSet<Student> Students { get; set; }
    public DbSet<Parent> Parents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PSchoolDbContext).Assembly);
    }
}