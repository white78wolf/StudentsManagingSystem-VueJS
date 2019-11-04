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
        private IStudentsRepository _repository;

        public UpdateController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        public IActionResult Put([FromBody]Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            if (!_repository.Students.Any(x => x.Id == student.Id))
            {
                return NotFound();
            }

            if (!UniqIdService.IsChangedAndUniq(student, _repository))
            {
                ModelState.AddModelError("", "Такой идентификатор уже есть");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.SaveStudent(student);

            return Ok(student);
        }
    }
}