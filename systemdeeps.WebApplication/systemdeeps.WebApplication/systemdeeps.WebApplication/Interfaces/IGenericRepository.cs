using System.Linq;

namespace systemdeeps.WebApplication.Interfaces
{
    // Generic repository contract for basic CRUD operations
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }
}