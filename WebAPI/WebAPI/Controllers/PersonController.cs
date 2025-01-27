using Model;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {
        private readonly string connectionString =
            "Host=ep-purple-mountain-a9g0az1p.gwc.azure.neon.tech;Port=5432;Database=neondb;Username=neondb_owner;Password=npg_tTf6oXQID0Bj;SSL Mode=Require";

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? name = null,
            [FromQuery] string? surname = null, [FromQuery] string? email = null, [FromQuery] int? phoneNumber = null)
        {
            var service = new PersonService();
            var persons = await service.GetAllAsync(name, surname, email, phoneNumber);

            if (persons == null)
                return BadRequest();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var service = new PersonService();
            var dog = await service.GetByIdAsync(id);

            if (dog == null)
                return BadRequest();

            return Ok(dog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var service = new PersonService();
            var success = await service.DeleteAsync(id);

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

            var service = new PersonService();
            var success = await service.SaveAsync(person);

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

            var service = new PersonService();
            var success = await service.UpdateAsync(id, person);

            if (!success)
                return BadRequest();

            return Ok("Updated.");
        }
    }
}
