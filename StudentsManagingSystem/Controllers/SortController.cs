using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Entities;
using SMS.Domain.Abstract;
using Services;

namespace StudentsManagingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private IUnitOfWork _uow;

        public SortController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{parameter}")]
        public IEnumerable<Student> Get(string parameter)
        {
            return SortingService.SortBy(parameter, _uow);
        }
    }
}