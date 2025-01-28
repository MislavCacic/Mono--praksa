using Model;
using Common;

namespace Service.Common
{
    public interface IPersonService
    {
        public Task<List<Person>?> GetAllAsync(PersonFilter personFilter, Sorting sorting, Paging paging);
        public Task<Person?> GetByIdAsync(Guid id);
        public Task<bool> SaveAsync(Person person);
        public Task<bool> UpdateAsync(Guid id, Person person);
        public Task<bool> DeleteAsync(Guid id);
    }
}