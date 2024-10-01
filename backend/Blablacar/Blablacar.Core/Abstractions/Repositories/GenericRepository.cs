using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using Blablacar.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blablacar.Core.Abstractions.Repositories;

/// <summary>
/// Абстракция репозиториев
/// </summary>
/// <param name="dbContext">Контекст базы даннных</param>
/// <typeparam name="TEntity">Сущность</typeparam>
/// <typeparam name="TId">Тип данных Id</typeparam>
public abstract class GenericRepository<TEntity, TId>(IDbContext dbContext) 
    where TEntity: class, IEntity<TId>
    where TId: notnull
{
    
    /// <inheritdoc cref="DbSet{TEntity}.FindAsync(object?[])"/>
    public async Task<TEntity?> FirstOrDefaultAsync(TId id) =>
        await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    
    /// <summary>
    /// Возращает всю таблицу
    /// </summary>
    public List<TEntity> GetAll() =>
        dbContext.Set<TEntity>().ToList();

    /// <inheritdoc cref="DbSet{TEntity}.AddAsync"/>
    public async Task<int> AddAsync(TEntity obj)
    {
        await dbContext.Set<TEntity>().AddAsync(obj);
        return await dbContext.SaveChangesAsync();
    }
    
    /// <inheritdoc cref="DbSet{TEntity}.AddAsync"/>
    public async Task<int> AddRangeAsync(List<TEntity> objs)
    {
        await dbContext.Set<TEntity>().AddRangeAsync(objs);
        return await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc cref="DbSet{TEntity}.Update"/>
    public async Task<int> Update(TEntity obj)
    {
        dbContext.Set<TEntity>().Update(obj);
        return await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc cref="DbSet{TEntity}.Remove"/>
    public async Task<int> Remove(TEntity obj)
    {
        dbContext.Set<TEntity>().Remove(obj);
        return await dbContext.SaveChangesAsync();
    }
        
}