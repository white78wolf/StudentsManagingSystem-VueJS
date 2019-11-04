using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Abstract;
using SMS.Domain.Entities;
namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]    
    public class StudentsController : Controller
    {        
        private IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            _repository = repository;
            // initializing the table by fake students
            if (!_repository.Students.Any())
            {
                _repository.SaveStudent(new Student { Name = "Роман", LastName = "Ойра-Ойра", Gender = "male" });
                _repository.SaveStudent(new Student { Name = "Александр", LastName = "Привалов", Gender = "male" });
                _repository.SaveStudent(new Student { Name = "Ольга", LastName = "Онучкина", Gender = "female" });
                _repository.SaveStudent(new Student { Name = "Стелла", LastName = "Иванова", Gender = "female" });                
            }
        }        

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _repository.Students.ToList();
        }

        // method to fill up client's form by response, using student's id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Student student = _repository.Students.FirstOrDefault(x => x.Id == id);            
            if (student == null)
                return NotFound();
            return new ObjectResult(student);
        }
    }
}
