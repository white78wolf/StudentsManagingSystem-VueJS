using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public static class SortingService
    {
        public static IEnumerable<Student> SortBy(string parameter, IRepository repository)
        {
            var result = new List<Student>();

            switch (parameter)
            {          
                case "name":
                    result = repository.Students
                        .OrderBy(s => s.Name)
                        .ToList();
                    break;
                case "name_desc":
                    result = repository.Students
                        .OrderByDescending(s => s.Name)
                        .ToList();
                    break;

                case "lastname":
                    result = repository.Students
                    .OrderBy(s => s.LastName)
                    .ToList();
                    break;
                case "lastname_desc":
                    result = repository.Students
                    .OrderByDescending(s => s.LastName)
                    .ToList();
                    break;

                case "middlename":
                    result = repository.Students
                    .OrderBy(s => s.MiddleName)
                    .ToList();
                    break;
                case "middlename_desc":
                    result = repository.Students
                    .OrderByDescending(s => s.MiddleName)
                    .ToList();
                    break;

                case "uniqid":
                    result = repository.Students
                    .OrderBy(s => s.UniqId)
                    .ToList();
                    break;
                case "uniqid_desc":
                    result = repository.Students
                    .OrderByDescending(s => s.UniqId)
                    .ToList();
                    break;

                case "gender":
                    result = repository.Students
                    .OrderBy(s => s.Gender)
                    .ToList();
                    break;
                case "gender_desc":
                    result = repository.Students
                    .OrderByDescending(s => s.Gender)
                    .ToList();
                    break;
            }
            return result;
        }
    }
}
