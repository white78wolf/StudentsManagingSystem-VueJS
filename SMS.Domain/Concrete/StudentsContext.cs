using Microsoft.EntityFrameworkCore;
using SMS.Domain.Entities;

namespace SMS.Domain.Concrete
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; } 

        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options)
        {            
            Database.EnsureCreated();
            //Database.Migrate();
        }
    }
}

// Comment the line with EnsureCreated method when migrating with console commands
// and uncomment Database.Migrate().
// After model's changed, rebuild the project with commented Database.Migrate() 
// and uncommented EnsureCreated().
