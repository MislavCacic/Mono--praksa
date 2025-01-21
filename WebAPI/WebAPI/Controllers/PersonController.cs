using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {
        private static List<Person> persons = new List<Person>();

        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine(persons.Count);
            if (!persons.Any())
            {
                return NoContent();
            }
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var resource = persons.FirstOrDefault(p => p.Id == id);

            if (resource == null)
            {
                return NotFound(new
                {
                    error = "Person isn't found",
                    message = $"Person with ID {id} doesn't exist."
                });
            }
            return Ok(resource);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var resource = persons.FirstOrDefault(p => p.Id == id);

            if (resource == null)
            {
                return NotFound(new
                {
                    error = "Person isn't found",
                    message = $"Person with ID {id} doesn't exist."
                });
            }
            persons.Remove(resource);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null || string.IsNullOrEmpty(person.Name) || string.IsNullOrEmpty(person.Surname))
            {
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Name and Surname are required."
                });
            }

            int newId = persons.Any() ? persons.Max(p => p.Id) + 1 : 1;
            person.Id = newId;

            persons.Add(person);

            //Console.WriteLine($"Added person with ID: {newId}");
            return Ok(new
            {
                message = "Person successfully added.",
                data = person
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            var source = persons.FirstOrDefault(p => p.Id == id);

            if (source == null)
            {
                return NotFound(new
                {
                    error = "Person isn't found",
                    message = $"Person with ID {id} doesn't exist."
                });
            }

            if (person == null || person.Name == null || person.Surname == null)
                return BadRequest(new
                {
                    error = "Bad Request",
                    message = "Name and Surname are required."
                });

            source.Name = person.Name;
            source.Surname = person.Surname;

            return Ok(new
            {
                message = "Person successfully updated.",
                data = person
            });
        }
    }
}
