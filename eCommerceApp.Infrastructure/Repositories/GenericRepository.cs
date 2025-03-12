using eCommerceApp.Application.Exceptions;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories;

/// <summary>
/// Generic repository for basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class GenericRepository<TEntity> :
    IGeneric<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Methods

    /// <summary>
    /// Asynchronously adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the number of state entries written to the database.</returns>
    public async Task<int> AddAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the number of state entries written to the database.</returns>
    /// <exception cref="ItemNotFoundException">Thrown when the entity with the specified identifier is not found.</exception>
    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>()
            .FindAsync(id) ?? throw new
            ItemNotFoundException($"Item Id {id} is not found");

        _dbContext.Set<TEntity>().Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously gets all entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains an enumerable of entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the entity.</returns>
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>()
            .FindAsync(id);

        return entity!;
    }

    /// <summary>
    /// Asynchronously updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the number of state entries written to the database.</returns>
    public async Task<int> UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        return await _dbContext.SaveChangesAsync();
    }
    #endregion
}
