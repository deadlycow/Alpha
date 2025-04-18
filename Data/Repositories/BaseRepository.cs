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
      IQueryable<TEntity> query = _dbSet.AsNoTracking();
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
    public async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
      try
      {
        var result = await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        return result != null 
          ? RepositoryResult<IEnumerable<TEntity>>.Ok(result)
          : RepositoryResult<IEnumerable<TEntity>>.NotFound("No members");
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entities: {ex.Message}");
        return RepositoryResult<IEnumerable<TEntity>>.InternalServerError(ex.Message);
      }
    }
    public virtual async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filterBy, params Expression<Func<TEntity, object>>[] includes)
    {
      try
      {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (includes != null && includes.Length != 0)
          foreach (var include in includes)
            query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(filterBy);
        if (entity == null)
          return RepositoryResult<TEntity>.NotFound("Entity");

        return RepositoryResult<TEntity>.Ok(entity);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entity: {ex.Message}");
        return RepositoryResult<TEntity>.InternalServerError(ex.Message);
      }
    }
    public async Task<RepositoryResult<TEntity>> GetAsync(object id)
    {
      if (id == null)
        return RepositoryResult<TEntity>.BadRequest("Id cannot be null");

      try
      {
        TEntity? entity = null;
        if (id is int)
          entity = await _dbSet.FindAsync(id);
        else if (id is string)
          entity = await _dbSet.FindAsync(id);
        return entity == null
          ? RepositoryResult<TEntity>.NotFound("Entity not found")
          : RepositoryResult<TEntity>.Ok(entity);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error fetching entity: {ex.Message}");
        return RepositoryResult<TEntity>.InternalServerError(ex.Message);
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
