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

// It's necessary to comment the line with EnsureCreated method when migrating
