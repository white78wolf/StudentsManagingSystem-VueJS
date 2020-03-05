using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using SMS.Domain.Abstract;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {        
        private IUnitOfWork _uow;

        public FilterController(IUnitOfWork uow)
        {            
            _uow = uow;
        }

        [HttpGet("{parameter}")]
        public IEnumerable<Student> Get(byte parameter)
        {            
            return _uow.Students
                    .Find(s => (byte)s.Gender == parameter)
                    .ToList();
        }
    }
}