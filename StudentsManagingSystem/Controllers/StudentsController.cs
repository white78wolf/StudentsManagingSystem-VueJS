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
        //private IRepository _repository;
        private IUnitOfWork _uow;

        public StudentsController(IUnitOfWork uow)
        {

            //_repository = repository;
            _uow = uow;

            // initializing the table by fake students
            if (_uow.Students == null)
            {
                //_repository.SaveStudent(new Student { Name = "Роман", LastName = "Ойра-Ойра", Gender = "male" });
                //_repository.SaveStudent(new Student { Name = "Александр", LastName = "Привалов", Gender = "male" });
                //_repository.SaveStudent(new Student { Name = "Ольга", LastName = "Онучкина", Gender = "female" });
                //_repository.SaveStudent(new Student { Name = "Стелла", LastName = "Иванова", Gender = "female" });                
            }
        }        

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            //return _repository.Students.ToList();
            return _uow.Students.GetAll();
        }

        // method to fill up client's form by response, using student's id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            //Student student = _repository.Students.FirstOrDefault(x => x.Id == id);            
            Student student = _uow.Students.Get(id);
            if (student == null)
                return NotFound();
            return new ObjectResult(student);
        }
    }
}
