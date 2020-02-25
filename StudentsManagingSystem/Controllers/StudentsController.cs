using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SMS.Domain.Abstract;
using SMS.Domain.Entities;
namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]    
    public class StudentsController : Controller
    {        
        private IUnitOfWork _uow;

        public StudentsController(IUnitOfWork uow)
        {            
            _uow = uow;

            // initializing the table by fake students
            if (!_uow.Students.GetAll().Any())
            {                
                _uow.Students.Create(new Student { Name = "Роман",     LastName = "Ойра-Ойра", Gender = "male"   });
                _uow.Students.Create(new Student { Name = "Александр", LastName = "Привалов",  Gender = "male"   });
                _uow.Students.Create(new Student { Name = "Ольга",     LastName = "Онучкина",  Gender = "female" });
                _uow.Students.Create(new Student { Name = "Стелла",    LastName = "Иванова",   Gender = "female" });
                _uow.Save();
            }
        }        

        [HttpGet]
        public IEnumerable<Student> Get()
        {            
            return _uow.Students.GetAll();
        }

        // method to fill up client's form by response, using student's id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {            
            Student student = _uow.Students.Get(id);
            if (student == null)
                return NotFound();
            return new ObjectResult(student);
        }
    }
}
