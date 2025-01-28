using Model;
using Common;

namespace Repository.Common
{
    public interface IPersonRepository
    {
        public Task<Person?> GetByIdAsync(Guid id);
        public Task<bool> SaveAsync(Person person);
        public Task<bool> UpdateAsync(Guid id, Person person);
        public Task<bool> DeleteAsync(Guid id);
        public Task<List<Person>> GetAllAsync(PersonFilter personFilter, Sorting sorting, Paging pagging);
    }
}