using Common;
using Model;
using Repository.Common;
using Npgsql;

namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string connectionString =
            "Host=ep-purple-mountain-a9g0az1p.gwc.azure.neon.tech;Port=5432;Database=neondb;Username=neondb_owner;Password=npg_tTf6oXQID0Bj;SSL Mode=Require";

//GET ALL
        public async Task<List<Person>> GetAllAsync(PersonFilter personFilter, Sorting sorting, Paging paging)
        {
            var persons = new List<Person>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT \"Id\", \"Name\", \"Surname\", \"Email\", \"PhoneNumber\" " +
                                      "FROM \"Person\"";

                    using var command = new NpgsqlCommand(commandText, connection);

                    AddPersonFilter(personFilter, command);
                    AddPersonSorting(sorting, command);
                    AddPersonPaging(paging, command);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var person = new Person()
                            {
                                Id = Guid.Parse(reader[0].ToString()!),
                                Name = reader[1].ToString()!,
                                Surname = reader[2].ToString()!,
                                Email = reader[3].ToString()!,
                                PhoneNumber = int.TryParse(reader[4].ToString(), out int result) ? result : 0
                            };

                            persons.Add(person);
                        }
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return persons;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

//GET BY ID
        public async Task<Person?> GetByIdAsync(Guid id)
        {
            try
            {
                var person = new Person() { };

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "SELECT " +
                                      "\"Person\".\"Id\", \"Name\", \"Surname\".\"Email\", \"PhoneNumber\"" +
                                      "FROM \"Person\" " +
                                      "WHERE \"Person\".\"Id\" = @id;";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        person.Id = Guid.Parse(reader[0].ToString()!);
                        person.Name = reader[1].ToString();
                        person.Surname = reader[2].ToString();
                        person.Email = reader[3].ToString();
                        person.PhoneNumber = int.TryParse(reader[4].ToString(), out int result) ? result : 0;
                    }

                    else
                    {
                        connection.Close();
                        return null;
                    }

                    connection.Close();

                    return person;
                }
            }

            catch (Exception)
            {
                return null;
            }
        }

//INSERT-SAVE
        public async Task<bool> SaveAsync(Person person)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText =
                        "INSERT INTO \"Person\" (\"Id\", \"Name\", \"Surname\", \"Email\", \"PhoneNumber\") " +
                        "VALUES (@id, @name, @surname, @email, @phoneNumber);";

                    using var command = new NpgsqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                    command.Parameters.AddWithValue("name", person.Name);
                    command.Parameters.AddWithValue("age", person.Surname);
                    command.Parameters.AddWithValue("email", person.Email);
                    command.Parameters.AddWithValue("phoneNumber", person.PhoneNumber);

                    connection.Open();

                    var affectedRows = await command.ExecuteNonQueryAsync();

                    if (affectedRows == 0)
                        return false;

                    connection.Close();

                    return true;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

//DELETE
        public async Task<bool> DeleteAsync(Guid id)
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

                    var reader = await command.ExecuteReaderAsync();
                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();
                    connection.Open();

                    commandText = "DELETE FROM \"Person\" WHERE \"Id\" = @id;";

                    using var deleteCommand = new NpgsqlCommand(commandText, connection);

                    deleteCommand.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

                    connection.Open();

                    var affectedRows = await deleteCommand.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();

                    return true;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }


//UPDATE
        public async Task<bool> UpdateAsync(Guid id, Person person)
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

                    var reader = await command.ExecuteReaderAsync();
                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();
                    connection.Open();

                    commandText =
                        "UPDATE \"Person\" set \"Name\" = @name, \"Surname\" = @surname, \"Email\" = @email, \"PhoneNumber\" = @phoneNumber " +
                        "WHERE \"Id\" = @id;";

                    using var updateCommand = new NpgsqlCommand(commandText, connection);
                    updateCommand.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, id);
                    updateCommand.Parameters.AddWithValue("name", person.Name);
                    updateCommand.Parameters.AddWithValue("surname", person.Surname);
                    updateCommand.Parameters.AddWithValue("email", person.Email);
                    updateCommand.Parameters.AddWithValue("phoneNumber", person.PhoneNumber);

                    var affectedRows = await updateCommand.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                    {
                        connection.Close();
                        return false;
                    }

                    connection.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddPersonFilter(PersonFilter personFilter, NpgsqlCommand command)
        {
            if (personFilter.Name != null)
            {
                command.CommandText += " AND \"Person\".\"Name\" = @name";
                command.Parameters.AddWithValue("name", personFilter.Name);
            }

            if (personFilter.Surname != null)
            {
                command.CommandText += " AND \"Surname\" = @surname";
                command.Parameters.AddWithValue("surname", personFilter.Surname);
            }
        }

        private void AddPersonPaging(Paging paging, NpgsqlCommand command)
        {
            command.CommandText += $" LIMIT {paging.Rpp} OFFSET ({paging.PageNumber} - 1) * {paging.Rpp}";
        }

        private void AddPersonSorting(Sorting sorting, NpgsqlCommand command)
        {
            command.CommandText += $" ORDER BY \"Person\".\"{sorting.OrderBy}\" {sorting.SortOrder}";
        }
    }
}