using SMS.Domain.Abstract;
using SMS.Domain.Entities;

namespace Services
{
    public static class UniqIdService
    {
        public static bool IsUniq(Student student, IRepository<Student> repository)
        {            
            // checking is uniqid existing already
            return student.UniqId != "" && !(repository.Find(x => x.UniqId == student.UniqId) == null) 
                ? false 
                : true;
        }

        public static bool IsChangedAndUniq(Student student, IRepository<Student> repository)
        {            
            // checking has uniqid changed and then checking it's existence in db
            return (repository.Get(student.Id).UniqId != student.UniqId)
                && student.UniqId != "" 
                && !(repository.Find(x => x.UniqId == student.UniqId) == null)
                ? false
                : true;
        }
    }
}
