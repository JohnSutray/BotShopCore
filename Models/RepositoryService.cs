using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BotShopCore.Models {
  public abstract class RepositoryService<TEntity> where TEntity : class, IIdentifiable {
    protected RepositoryService(ApplicationContext context) => Context = context;

    protected readonly ApplicationContext Context;
    protected abstract DbSet<TEntity> Set { get; }

    protected async Task SaveChangesAsync() => await Context.SaveChangesAsync();

    private void SaveChanges() => Context.SaveChanges();

    protected TEntity ById(int id) => Set.FirstOrDefault(e => e.Id == id);

    protected TEntity[] ByIdMany(IEnumerable<int> ids) => Set.Where(entity => ids.Contains(entity.Id)).ToArray();

    protected async Task<TEntity> ByIdAsync(int id) => await Set.FirstOrDefaultAsync(e => e.Id == id);

    protected async Task<TEntity> ByPatternAsync(Expression<Func<TEntity, bool>> pattern) =>
      await Set.SingleOrDefaultAsync(pattern);

    protected TEntity ByPattern(Expression<Func<TEntity, bool>> pattern) => Set.SingleOrDefault(pattern);

    protected async Task<List<TEntity>> ByPatternManyAsync(Expression<Func<TEntity, bool>> pattern) =>
      await Set.Where(pattern).ToListAsync();

    protected List<TEntity> ByPatternMany(Expression<Func<TEntity, bool>> pattern) => Set.Where(pattern).ToList();

    protected async Task<List<TEntity>> ByPatternManyAsync<TProperty>(
      Expression<Func<TEntity, bool>> pattern,
      Expression<Func<TEntity, TProperty>> include
    ) => await Set
      .Where(pattern)
      .Include(include)
      .ToListAsync();

    protected List<TEntity> ByPatternMany<TProperty>(
      Expression<Func<TEntity, bool>> pattern,
      Expression<Func<TEntity, TProperty>> include
    ) => Set.Where(pattern)
      .Include(include)
      .ToList();

    protected async Task<PaginationResult<TEntity>> PaginateByPatternAsync(
      Expression<Func<TEntity, bool>> pattern,
      int page,
      int limit
    ) {
      var allItems = Set.Where(pattern);
      var allItemsCount = await allItems.CountAsync();

      return new PaginationResult<TEntity> {
        Items = await allItems.Skip(page * limit).Take(limit).ToListAsync(),
        Limit = limit,
        Page = page,
        TotalPages = (int) Math.Ceiling(allItemsCount / (double) limit)
      };
    }

    protected async Task<PaginationResult<TEntity>> PaginateByPatternAsync(
      Expression<Func<TEntity, bool>> pattern,
      Func<IQueryable<TEntity>, IQueryable<TEntity>> includePattern,
      int page,
      int limit
    ) {
      var allItems = Set.Where(pattern);
      var allItemsCount = await allItems.CountAsync();
      var paginatedQuery = allItems.Skip(page * limit).Take(limit);
      var includedQuery = includePattern(paginatedQuery);

      return new PaginationResult<TEntity> {
        Items = await includedQuery.ToListAsync(),
        Limit = limit,
        Page = page,
        TotalPages = (int) Math.Ceiling(allItemsCount / (double) limit)
      };
    }

    protected async Task<PaginationResult<TEntity>> PaginateAsync(int page, int limit) {
      var allItemsCount = await Set.CountAsync();

      return new PaginationResult<TEntity> {
        Items = await Set.Skip(page * limit).Take(limit).ToListAsync(),
        Limit = limit,
        Page = page,
        TotalPages = (int) Math.Ceiling(allItemsCount / (double) limit)
      };
    }

    protected PaginationResult<TEntity> PaginateByPattern(
      Expression<Func<TEntity, bool>> pattern,
      int page,
      int limit
    ) {
      var allItems = Set.Where(pattern);
      var allItemsCount = allItems.Count();

      return new PaginationResult<TEntity> {
        Items = allItems.Skip(page * limit).Take(limit).ToList(),
        Limit = limit,
        Page = page,
        TotalPages = (int) Math.Ceiling(allItemsCount / (double) limit)
      };
    }

    protected async Task<TEntity> AddEntityAsync(TEntity entity) {
      var entityEntry = await Set.AddAsync(entity);
      await SaveChangesAsync();

      return entityEntry.Entity;
    }

    protected TEntity AddEntity(TEntity entity) {
      var entityEntry = Set.Add(entity);
      SaveChanges();

      return entityEntry.Entity;
    }

    protected async Task AddRangeAsync(IEnumerable<TEntity> entities) {
      await Set.AddRangeAsync(entities);
      await SaveChangesAsync();
    }

    protected void AddRange(IEnumerable<TEntity> entities) {
      Set.AddRange(entities);
      SaveChanges();
    }

    protected async Task RemoveByIdAsync(int id) {
      var entity = await ByIdAsync(id);
      Set.Remove(entity);

      await SaveChangesAsync();
    }

    protected void RemoveById(int id) {
      var entity = ById(id);
      Set.Remove(entity);
      SaveChanges();
    }

    protected void RemoveByPattern(Expression<Func<TEntity, bool>> pattern) {
      Set.Remove(Set.First(pattern));
      SaveChanges();
    }

    protected async Task<List<TEntity>> RemoveManyByPatternAsync(Expression<Func<TEntity, bool>> pattern) {
      var entitiesToRemove = await Set.Where(pattern).ToListAsync();
      Set.RemoveRange(entitiesToRemove);
      await SaveChangesAsync();

      return entitiesToRemove;
    }

    protected List<TEntity> RemoveManyByPattern(Expression<Func<TEntity, bool>> pattern) {
      var entitiesToRemove = Set.Where(pattern).ToList();
      Set.RemoveRange(entitiesToRemove);
      SaveChanges();

      return entitiesToRemove;
    }

    protected async Task<int> CountAsync(Expression<Func<TEntity, bool>> pattern) => await Set.CountAsync(pattern);

    protected int Count(Expression<Func<TEntity, bool>> pattern) => Set.Count(pattern);

    protected async Task UpdateByIdAsync(int id, Action<TEntity> updateCallback) {
      var entity = await ByIdAsync(id);
      updateCallback(entity);
      await SaveChangesAsync();
    }

    protected void UpdateById(int id, Action<TEntity> updateCallback) {
      var entity = ById(id);
      updateCallback(entity);
      SaveChanges();
    }

    protected void UpdateByPattern(Expression<Func<TEntity, bool>> pattern, Action<TEntity> updateCallback) {
      var entity = ByPattern(pattern);
      updateCallback(entity);
      SaveChanges();
    }
  }
}