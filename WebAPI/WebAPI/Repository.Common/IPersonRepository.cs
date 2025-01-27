﻿using Model;

namespace Repository.Common
{
    public interface IPersonRepository
    {
        public Task<Person?> GetByIdAsync(Guid id);
        public Task<bool> SaveAsync(Person person);
        public Task<bool> UpdateAsync(Guid id, Person person);
        public Task<bool> DeleteAsync(Guid id);
    }
}