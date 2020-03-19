using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class UniqIdService
    {
        public static bool IsUniq(Student student, IRepository<Student> repository)
        {
            // method to check is uniqid existing already 

            return IsUniqIdNullOrEmpty(student)
                ? IsNullOrEmpty(repository.Find(x => x.UniqId == student.UniqId))
                    ? true 
                    : false
                : true;
        }

        public static bool IsChangedAndUniq(Student student, IRepository<Student> repository)
        {
            // method to check has uniqid changed and then to check its existence in db 

            return repository.Get(student.Id).UniqId != student.UniqId
                && !IsUniqIdNullOrEmpty(student)
                ? IsNullOrEmpty(repository.Find(x => x.UniqId == student.UniqId))
                    ? true 
                    : false
                : true;
        }

        public static bool IsNullOrEmpty<T>(IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static bool IsUniqIdNullOrEmpty(Student student)
        {
            return student.UniqId == null || student.UniqId == "";
        }
    }
}
