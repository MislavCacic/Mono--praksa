using Model;
using Microsoft.AspNetCore.Mvc;
using Common;
using Service.Common;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[LogActionFilter]
    public class PersonController : ControllerBase
    {
        private IPersonService _service;

        public PersonController(IPersonService personService)
        {
            _service = personService;
        }

        public async Task<IActionResult> GetAllAsync(
            string? name = null, string? surname = null, string? email = null, int? phoneNumber = null,
            string orderBy = "Id", string sortOrder = "ASC",
            int currentPage = 1, int rpp = 5)
        {
            var dogFilter = new PersonFilter()
            {
                Name = name,
                Surname = surname,
            };

            var sorting = new Sorting()
            {
                OrderBy = orderBy,
                SortOrder = sortOrder
            };

            var paging = new Paging()
            {
                Rpp = rpp,
                PageNumber = currentPage,
            };

            var response = await _service.GetAllAsync(dogFilter, sorting, paging);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var person = await _service.GetByIdAsync(id);

            if (person == null)
                return BadRequest();

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return BadRequest();

            return Ok("Deleted.");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] Person person)
        {
            if (person == null)
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });

            var success = await _service.SaveAsync(person);

            if (!success)
                return BadRequest();

            return Ok("Saved.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Person person)
        {
            if (person == null)
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Invalid data."
                });

            var success = await _service.UpdateAsync(id, person);

            if (!success)
                return BadRequest();

            return Ok("Updated.");
        }
    }
}