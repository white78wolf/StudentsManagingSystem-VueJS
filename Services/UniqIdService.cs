using SMS.Domain.Abstract;
using System.Linq;

namespace Services
{
    public static class UniqIdService
    {
        public static bool IsUniq(IStudent student, IRepository repository)
        {            
            // checking is uniqid existing already
            return !(repository.Students.FirstOrDefault(x => x.UniqId == student.UniqId) == null) 
                ? false 
                : true;
        }

        public static bool IsChangedAndUniq(IStudent student, IRepository repository)
        {            
            // checking has uniqid changed and then checking it's existence in db
            return (repository.Students.First(x => x.Id == student.Id).UniqId != student.UniqId)
                && !(repository.Students.FirstOrDefault(x => x.UniqId == student.UniqId) == null)
                ? false
                : true;
        }
    }
}
