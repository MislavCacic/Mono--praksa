using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Npgsql;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {
        private readonly string connectionString = "Host=ep-purple-mountain-a9g0az1p.gwc.azure.neon.tech;Port=5432;Database=neondb;Username=neondb_owner;Password=npg_tTf6oXQID0Bj;SSL Mode=Require";

        [HttpGet]
        public IActionResult Get()
        {
            var persons = new List<Person>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\", \"Surname\", \"Email\", \"PhoneNumber\" FROM \"Person\"";
                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = Guid.Parse(reader[0].ToString()!);
                            var name = reader[1].ToString();
                            var surname = reader[2].ToString();
                            var email = reader[3].ToString();
                            var phoneNumber = int.TryParse(reader[4].ToString(), out int result) ? result : 0;

                            //var person = new Person() { Id = id, Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber };
                            //persons.Add(person);
                        }
                    }
                    else
                    {
                        return NotFound();
                    }

                    connection.Close();

                    return Ok(persons);
                }
            }

            catch (Exception exception)
            {
                return BadRequest(
                    new
                    {
                        error = "Bad request",
                        message = exception.Message
                    });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var person = new Person();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT " +
                        "\"Person\".\"Id\", \"Name\", \"Surname\".\"Email\", \"PhoneNumber\"" +
                        "FROM \"Person\" " +
                        "WHERE \"Person\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);

                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        person.Id = Guid.Parse(reader[0].ToString()!);
                        person.Name = reader[1].ToString();
                        person.Surname = reader[2].ToString();
                        person.Email = reader[3].ToString();
                        person.PhoneNumber = int.TryParse(reader[4].ToString(), out int result) ? result : 0;
                    }

                    connection.Close();

                    return Ok(person);
                }
            }

            catch (Exception exception)
            {
                return BadRequest(
                    new
                    {
                        error = "Bad request",
                        message = exception.Message
                    });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "DELETE FROM \"Person\"" +
                        "WHERE \"Id\" = @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var affectedRows = command.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return NotFound();
                    }

                    connection.Close();

                    return Ok("Person deleted.");
                }
            }

            catch (Exception exception)
            {
                return BadRequest(
                    new
                    {
                        error = "Something went wrong.",
                        message = exception.Message
                    });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "INSERT INTO \"Person\" (\"Id\", \"Name\", \"Surname\", \"Email\", \"PhoneNumber\") VALUES (@id, @name, @surname, @email, @phoneNumber);";

                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                        command.Parameters.AddWithValue("name", person.Name);
                        command.Parameters.AddWithValue("surname", person.Surname);
                        command.Parameters.AddWithValue("email", person.Email);
                        command.Parameters.AddWithValue("mobile", person.PhoneNumber);

                        var rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                            return BadRequest();

                        connection.Close();

                        return Ok("Person saved.");
                    }
                }
            }

            catch (Exception exception)
            {
                return BadRequest(new
                {
                    error = "Something went wrong!",
                    message = exception.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\", \"Surname\", \"Email\", \"PhoneNumber\" " +
                        "FROM \"Person\" " +
                        "WHERE \"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = command.ExecuteReader();
                    if(!reader.HasRows)
                    {
                        connection.Close();
                        return NotFound();
                    }

                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        return BadRequest();

                    connection.Close();

                    return Ok("Person updated.");
                }
            }

            catch (Exception exception)
            {
                return BadRequest(new
                {
                    error = "Something went wrong.",
                    message = exception.Message
                });
            }
        }
    }
}
