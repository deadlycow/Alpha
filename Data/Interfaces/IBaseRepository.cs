using Data.Models;
using System.Linq.Expressions;

namespace Data.Interfaces
{
  public interface IBaseRepository<TEntity, TModel> where TEntity : class
  {
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<RepositoryResult<bool>> CreateAsync(TEntity entity);
    Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy);
    Task<RepositoryResult<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes);
    Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes);
    Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterBy, params Expression<Func<TEntity, object>>[] includes);
    Task<RepositoryResult<TEntity>> GetAsync(object id);
    RepositoryResult<bool> Update(TEntity entity);
    RepositoryResult<bool> Delete(TEntity entity);
    Task<int> SaveAsync();
  }
}