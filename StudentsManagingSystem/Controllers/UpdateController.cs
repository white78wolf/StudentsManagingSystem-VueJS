using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using Services;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        //private IRepository _repository;
        IUnitOfWork _uow;

        public UpdateController(IUnitOfWork uow)
        {
            //_repository = repository;
            _uow = uow;
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

            //_repository.SaveStudent(student);

            return Ok(student);
        }
    }
}