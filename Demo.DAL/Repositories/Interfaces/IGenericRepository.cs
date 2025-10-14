using Demo.DAL.Models.Shared;
using System.Linq.Expressions;


namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T,TResult>> selector);
        T? GetById(int id);
        int Remove(T entity);
        int Update(T entity);

        //IEnumerable<T> GetEnumerable();
        //IQueryable<T> GetQueryable();

    }
}
