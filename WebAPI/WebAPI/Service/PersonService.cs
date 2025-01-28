using Model;
using Repository.Common;
using Service.Common;
using Common;

namespace Service
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;

        public PersonService(IPersonRepository repository)
        {
            _personRepository = repository;
        }

        public async Task<bool> SaveAsync(Person person)
        {
            return await _personRepository.SaveAsync(person);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _personRepository.DeleteAsync(id);
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, Person person)
        {
            return await _personRepository.UpdateAsync(id, person);
        }

        public async Task<List<Person>?> GetAllAsync(PersonFilter personFilter, Sorting sorting, Paging paging)
        {
            return await _personRepository.GetAllAsync(personFilter, sorting, paging);
        }
    }
}