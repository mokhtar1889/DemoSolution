using Demo.DAL.Models.Shared;
using System.Linq.Expressions;


namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T,TResult>> selector);
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        T? GetById(int id);
        void Remove(T entity);
        void Update(T entity);

        //IEnumerable<T> GetEnumerable();
        //IQueryable<T> GetQueryable();

    }
}
