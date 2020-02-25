using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public static class SortingService
    {
        public static IEnumerable<Student> SortBy(string parameter, IUnitOfWork uow)
        {
            var result = new List<Student>();

            switch (parameter)
            {
                case "name":
                    result = uow.Students
                        .GetAll()
                        .OrderBy(s => s.Name)
                        .ToList();
                    break;
                case "name_desc":
                    result = uow.Students
                        .GetAll()
                        .OrderByDescending(s => s.Name)
                        .ToList();
                    break;

                case "lastname":
                    result = uow.Students
                        .GetAll()
                        .OrderBy(s => s.LastName)
                        .ToList();
                    break;
                case "lastname_desc":
                    result = uow.Students
                        .GetAll()
                        .OrderByDescending(s => s.LastName)
                        .ToList();
                    break;

                case "middlename":
                    result = uow.Students
                        .GetAll()
                        .OrderBy(s => s.MiddleName)
                        .ToList();
                    break;
                case "middlename_desc":
                    result = uow.Students
                        .GetAll()
                        .OrderByDescending(s => s.MiddleName)
                        .ToList();
                    break;

                case "uniqid":
                    result = uow.Students
                        .GetAll()
                        .OrderBy(s => s.UniqId)
                        .ToList();
                    break;
                case "uniqid_desc":
                    result = uow.Students
                        .GetAll()
                        .OrderByDescending(s => s.UniqId)
                        .ToList();
                    break;

                case "gender":
                    result = uow.Students
                        .GetAll()
                        .OrderBy(s => s.Gender)
                        .ToList();
                    break;
                case "gender_desc":
                    result = uow.Students
                        .GetAll()
                        .OrderByDescending(s => s.Gender)
                        .ToList();
                    break;
            }
            return result;
        }
    }
}
