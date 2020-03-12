using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Services
{
    public static class SortingService
    {
        public static IEnumerable<Student> SortBy(string parameter, IUnitOfWork uow)
        {
            List<Student> result = new List<Student>();

            Dictionary<string, Func<Student, object>> studprops = new Dictionary<string, Func<Student, object>>(5)
            {
                { "name",       s => s.Name       },
                { "lastname",   s => s.LastName   },
                { "middlename", s => s.MiddleName },
                { "uniqid",     s => s.UniqId     },
                { "gender",     s => s.Gender     }
            };

            if (parameter.EndsWith("_desc"))
                result = uow.Students
                    .GetAll()
                    .OrderByDescending(studprops[parameter.Replace("_desc", "")])
                    .ToList();
            else result = uow.Students
                    .GetAll()
                    .OrderBy(studprops[parameter])
                    .ToList();

            return result;
        }
    }
}
