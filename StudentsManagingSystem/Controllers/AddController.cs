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
        private IUnitOfWork _uow;

        public AddController(IUnitOfWork uow)
        {
            _uow = uow;
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
    }
}