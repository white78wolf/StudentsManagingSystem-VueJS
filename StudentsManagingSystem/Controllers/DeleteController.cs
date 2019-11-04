using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using SMS.Domain.Abstract;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private IStudentsRepository _repository;

        public DeleteController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = _repository.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            _repository.DeleteStudent(id);
            return Ok(student);
        }
    }
}