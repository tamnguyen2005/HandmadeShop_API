using System.Linq.Expressions;

namespace HandmadeShop.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>?> FindAsync(Expression<Func<T, bool>> expression);
    }
}