using System.Collections.Generic;
using SMS.Domain.Entities;

namespace SMS.Domain.Abstract
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> Students { get; }
        void SaveStudent(Student student);
        Student DeleteStudent(int Id);
    }
}
