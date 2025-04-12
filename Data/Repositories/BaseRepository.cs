using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
  public abstract class BaseRepository<TEntity, TModel>(DataContext context) : IBaseRepository<TEntity, TModel> where TEntity : class
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

    public async Task<RepositoryResult<bool>> CreateAsync(TEntity entity)
    {
      if (entity == null)
        return RepositoryResult<bool>.BadRequest("Entity");

      try
      {
        await _dbSet.AddAsync(entity);
        return RepositoryResult<bool>.Created();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        return RepositoryResult<bool>.InternalServerError(ex.Message);
      }
    }
    public virtual async Task<RepositoryResult<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
      IQueryable<TEntity> query = _dbSet;
      if (filterBy != null)
        query = query.Where(filterBy);

      if (includes != null && includes.Length != 0)
        foreach (var include in includes)
          query = query.Include(include);

      if (sortBy != null)
        query = orderByDescending
          ? query.OrderByDescending(sortBy)
          : query.OrderBy(sortBy);

      var entities = await query.ToListAsync();
      var result = entities.Select(entity => entity.MapTo<TModel>());
      return RepositoryResult<IEnumerable<TModel>>.Ok(result);
    }
    public async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
      IQueryable<TEntity> query = _dbSet;
      if (filterBy != null)
        query = query.Where(filterBy);

      if (includes != null && includes.Length != 0)
        foreach (var include in includes)
          query = query.Include(include);

      if (sortBy != null)
        query = orderByDescending
          ? query.OrderByDescending(sortBy)
          : query.OrderBy(sortBy);

      var entities = await query.Select(selector).ToListAsync();
      var result = entities.Select(entity => entity!.MapTo<TSelect>());
      return RepositoryResult<IEnumerable<TSelect>>.Ok(result);
    }
    public async Task<RepositoryResult<TModel>> GetAsync(Expression<Func<TEntity, bool>> filterBy, params Expression<Func<TEntity, object>>[] includes)
    {
      try
      {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null && includes.Length != 0)
          foreach (var include in includes)
            query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(filterBy);
        if (entity == null)
          return RepositoryResult<TModel>.NotFound("Entity");

        var result = entity.MapTo<TModel>();
        return RepositoryResult<TModel>.Ok(result);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entity: {ex.Message}");
        return RepositoryResult<TModel>.InternalServerError(ex.Message);
      }
    }
    public async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
      var exists = await _dbSet.AnyAsync(findBy);
      return exists
        ? RepositoryResult<bool>.AlreadyExists()
        : RepositoryResult<bool>.NotFound("Entity");
    }
    public RepositoryResult<bool> Update(TEntity entity)
    {
      if (entity == null)
        return RepositoryResult<bool>.BadRequest("Entity");
      try
      {
        _dbSet.Update(entity);
        return RepositoryResult<bool>.Ok();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        return RepositoryResult<bool>.InternalServerError(ex.Message);
      }
    }
    public RepositoryResult<bool> Delete(TEntity entity)
    {
      if (entity == null)
        return RepositoryResult<bool>.BadRequest("Entity");
      try
      {
        _dbSet.Remove(entity);
        return RepositoryResult<bool>.Ok();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        return RepositoryResult<bool>.InternalServerError(ex.Message);
      }
    }
    public async Task<int> SaveAsync()
    {
      return await _context.SaveChangesAsync();
    }
  }
}
