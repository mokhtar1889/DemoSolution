using Demo.DAL.Models.Shared;


namespace Demo.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(int id);
        int Remove(T entity);
        int Update(T entity);
    }
}
