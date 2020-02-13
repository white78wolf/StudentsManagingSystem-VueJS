using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using Services;
using SMS.Domain.Abstract;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private IRepository _repository;

        public AddController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if (student == null)
            {
                ModelState.AddModelError("", "Не указаны данные студента");
                return BadRequest();
            }

            if (!UniqIdService.IsUniq(student, _repository))
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