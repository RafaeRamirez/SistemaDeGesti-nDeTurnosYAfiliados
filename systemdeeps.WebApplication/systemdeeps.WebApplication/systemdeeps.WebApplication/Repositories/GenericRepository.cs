using Microsoft.EntityFrameworkCore;
using systemdeeps.WebApplication.Data;
using systemdeeps.WebApplication.Interfaces;

namespace systemdeeps.WebApplication.Repositories
{
    // EF Core generic repository implementation
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public IQueryable<T> GetAll() => _set.AsQueryable();

        public T? GetById(int id) => _set.Find(id);

        public void Add(T entity) => _set.Add(entity);

        public void Update(T entity) => _set.Update(entity);

        public void Delete(int id)
        {
            var entity = _set.Find(id);
            if (entity != null) _set.Remove(entity);
        }

        public void Save() => _context.SaveChanges();
    }
}