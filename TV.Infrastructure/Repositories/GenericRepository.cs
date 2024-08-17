using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TV.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
             
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.Where(t=> EF.Property<bool>(t, "IsDeleted") == false).ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                var propertyInfo = entity.GetType().GetProperty("IsDeleted");
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(entity, true);
                    _dbSet.Update(entity);
                    _context.SaveChanges();
                }
            }
        }
        public int GetNumberOfList()
        {
            return _dbSet.Count();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
