using Microsoft.EntityFrameworkCore;

namespace StudentsManagingSystem.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentsContext(DbContextOptions<StudentsContext> options) 
            : base(options)
        { }
    }
}
