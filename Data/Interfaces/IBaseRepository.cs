using System.Linq.Expressions;

namespace Data.Interfaces;
public interface IBaseRepository<TEntity> where TEntity : class
{
  Task BeginTransactionAsync();
  Task CommitTransactionAsync();
  Task RollbackTransactionAsync();

  Task<TEntity?> CreateAsync(TEntity entity);
  Task<IEnumerable<TEntity>?> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
  Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
  public void Update(TEntity entity);
  void Delete(TEntity entity);
  Task<int> SaveAsync();
}
