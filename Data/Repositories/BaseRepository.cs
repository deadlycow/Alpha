using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
  public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
  {
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    public async Task BeginTransactionAsync()
    {
      _transaction ??= await _context.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
      if (_transaction != null)
      {
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
      }
    }
    public async Task RollbackTransactionAsync()
    {
      if (_transaction != null)
      {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null!;
      }
    }

    public Task<TEntity?> CreateAsync(TEntity entity)
    {
      throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
      try
      {
        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
          query = includeExpression(query);

        return await query.ToListAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entities: {ex.Message}");
        return [];
      }
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
      try
      {
        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
          query = includeExpression(query);

        return await query.FirstOrDefaultAsync(predicate);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entity: {ex.Message}");
        return null;
      }
    }

    

    public Task<int> SaveAsync()
    {
      throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
