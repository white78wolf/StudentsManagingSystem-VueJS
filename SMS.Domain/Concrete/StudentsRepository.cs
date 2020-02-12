using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using System.Collections.Generic;

namespace SMS.Domain.Concrete
{
    public class StudentsRepository: IStudentsRepository
    {
        StudentsContext _context;

        public StudentsRepository(StudentsContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> Students => _context.Students;

        public void SaveStudent(Student student)
        {
            if (student.Id == 0)
                _context.Students.Add(student);
            else
            {
                Student dbEntry = _context.Students.Find(student.Id);
                if (dbEntry != null)
                {
                    dbEntry.UniqId = student.UniqId;
                    dbEntry.Name = student.Name;
                    dbEntry.MiddleName = student.MiddleName;
                    dbEntry.LastName = student.LastName;
                    dbEntry.Gender = student.Gender;
                }
            }
            _context.SaveChanges();
        }
        
        public Student DeleteStudent(Student student)
        {            
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }        
    }
}
