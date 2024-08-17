using System;
using System.Collections.Generic;

namespace TV.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        public int GetNumberOfList();
        void Delete(int id);
        void SaveChanges();
    }
}
