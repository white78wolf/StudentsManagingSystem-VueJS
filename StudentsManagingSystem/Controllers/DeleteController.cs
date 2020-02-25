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
        //private IRepository _repository;
        private IUnitOfWork _uow;

        public DeleteController(IUnitOfWork uow)
        {
            //_repository = repository;
            _uow = uow;
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