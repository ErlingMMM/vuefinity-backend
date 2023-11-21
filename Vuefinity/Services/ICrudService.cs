using Vuefinity.Data.Exceptions;

namespace Vuefinity.Services
{
    //Interface that represents the basic CRUD (Create, Read, Update, Delete)
    //operations for a data entity in a software application.
    public interface ICrudService<TEntity, TID>
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TID mail);
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
    }
}
