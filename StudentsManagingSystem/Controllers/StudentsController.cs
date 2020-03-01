using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using Services;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if (student == null)
            {
                ModelState.AddModelError("", "Не указаны данные студента");
                return BadRequest();
            }

            if (!UniqIdService.IsUniq(student, _uow.Students))
            {
                ModelState.AddModelError("", "Такой идентификатор уже есть");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _uow.Students.Create(student);
            _uow.Save();

            return Ok(student);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            if (_uow.Students.Find(x => x.Id == student.Id) == null)
            {
                return NotFound();
            }

            if (!UniqIdService.IsChangedAndUniq(student, _uow.Students))
            {
                ModelState.AddModelError("", "Такой идентификатор уже есть");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = _uow.Students.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            _uow.Students.Delete(student);
            _uow.Save();
            return Ok(student);
        }
    }
}
