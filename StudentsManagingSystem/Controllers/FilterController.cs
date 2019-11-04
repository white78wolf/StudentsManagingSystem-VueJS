using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using Services;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IStudentsRepository _repository;        

        public FilterController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{parameter}")]
        public IEnumerable<Student> Get(string parameter)
        {
            if (parameter == "male" || parameter == "female")
                return _repository.Students
                    .Where(s => s.Gender == parameter)
                    .ToList();
            else
                return SortingService.SortBy(parameter, _repository);
        }
    }
}