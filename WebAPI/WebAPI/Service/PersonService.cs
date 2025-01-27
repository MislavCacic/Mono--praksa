using Model;
using Repository;
using Service.Common;

namespace Service
{
    public class PersonService : IPersonService
    {
        public async Task<bool> SaveAsync(Person person)
        {
            var repository = new PersonRepository();
            return await repository.SaveAsync(person);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var repository = new PersonRepository();
            return await repository.DeleteAsync(id);
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            var repository = new PersonRepository();
            return await repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, Person person)
        {
            var repository = new PersonRepository();
            return await repository.UpdateAsync(id, person);
        }

        public async Task<List<Person>?> GetAllAsync(string? name, string? surname, string? email,
            int? phoneNumber)
        {
            var repository = new PersonRepository();
            return await repository.GetAllAsync();
        }
    }
}