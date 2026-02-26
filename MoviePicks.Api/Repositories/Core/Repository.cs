namespace MovieRatingsBackendWebApi.Repositories.Core;

using Microsoft.EntityFrameworkCore;
using MovieRatingsBackendWebApi.Infrastructure.Exceptions;
using MovieRatingsBackendWebApi.Models;
using MR.Models;
using System.Linq.Expressions;

public class Repository<T> : IRepository<T>
        where T : class, IIdentifiable
{
    private readonly DbMovieContext db;
    private DbSet<T> dbSet;

    public Repository(DbMovieContext db)
    {
        this.db = db;

        // For example, if T is MovieRepository, then dbSet = db.Movies
        dbSet = db.Set<T>();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await this.dbSet.AnyAsync(predicate);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null)
    {
        IQueryable<T> query = this.dbSet;
        query = query.Where(predicate);
        query = this.EagerLoadNavigationProperties(query, includeProperties);
        return await query.ToListAsync();
    }

    public void Add(T entity)
    {
        this.dbSet.Add(entity);
    }

    public bool Delete(int id)
    {
        T? entity = this.FindById(id);
        if (entity == null)
        {
            return false;
        }

        try
        {
            this.dbSet.Remove(entity);
        }
        catch (Exception ex)
        {
            // TODO: log the exception or add additional context?
            throw new EntityDeletionException(entity.Id, ex);
        }

        return true;
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        this.dbSet.RemoveRange(entities);
    }

    /// <summary>
    /// Finds a single entity by its ID, with optional inclusion of related entities through eager loading.
    /// </summary>
    /// <param name="id">The ID of the entity to find. If null, the method returns null immediately.</param>
    /// <param name="includeProperties">Comma-separated list of navigation properties for eager loading. Optional. If provided, related entities specified will be included in the query result.</param>
    /// <returns>
    /// The entity that matches the given ID, or null if no such entity is found. If more than one entity matches the ID (which should be unique), an <see cref="InvalidOperationException"/> is thrown.
    /// </returns>
    public T? FindById(int? id, string? includeProperties = null)
    {
        if (id == null)
        {
            return null;
        }
        else
        {
            IQueryable<T> query = this.dbSet;
            query = this.EagerLoadNavigationProperties(query, includeProperties);
            return query.SingleOrDefault(x => x.Id == id);
        }
    }

    public IQueryable<T> FindAll(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = this.dbSet;
        query = query.Where(filter);
        return this.EagerLoadNavigationProperties(query, includeProperties);
    }

    public Task<T?> FindOneAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = this.dbSet;
        query = query.Where(filter);
        query = this.EagerLoadNavigationProperties(query, includeProperties);
        return query.SingleOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<T> query = this.dbSet;
        query = this.EagerLoadNavigationProperties(query, includeProperties);
        return await query.ToListAsync();
    }

    public IQueryable<T> GetAsQueryable()
    {
       return this.dbSet;
    }

    private IQueryable<T> EagerLoadNavigationProperties(IQueryable<T> query, string? includeProperties)
    {
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                // Uses Include for eager loading to fetch related entities. For instance, suppose 'Product' is the
                // entity being queried and "Category" its related navigation property. Then the direct code
                // equivalent is: _db.Products.Include(p => p.Category). This avoids the N+1 query problem.
                // "Product" and "Category" serve as illustrative placeholders.
                query = query.Include(includeProperty);
            }
        }

        return query;
    }
}