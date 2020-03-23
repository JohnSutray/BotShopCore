using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ImportShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ImportShopCore.Models {
  public class RepositoryService<TEntity> where TEntity : class, IIdentifiable {
    protected RepositoryService(
      ApplicationContext context,
      Func<ApplicationContext, DbSet<TEntity>> setMapping
    ) {
      Context = context;
      SetMapping = setMapping;
    }

    protected ApplicationContext Context { get; }
    private Func<ApplicationContext, DbSet<TEntity>> SetMapping { get; }
    private DbSet<TEntity> Set => SetMapping(Context);

    protected async Task SaveChangesAsync() =>
      await Context.SaveChangesAsync();

    protected async Task<TEntity> ByIdAsync(int id) =>
      await Set.FirstAsync(e => e.Id == id);

    protected async Task<TEntity> ByPatternAsync(Expression<Func<TEntity, bool>> pattern) =>
      await Set.SingleAsync(pattern);

    protected async Task<List<TEntity>> ByPatternManyAsync(
      Expression<Func<TEntity, bool>> pattern
    ) => await Set.Where(pattern).ToListAsync();
    
    protected async Task<List<TEntity>> ByPatternManyAsync<TProperty>(
      Expression<Func<TEntity, bool>> pattern,
      Expression<Func<TEntity, TProperty>> include
    ) => await Set
      .Where(pattern)
      .Include(include)
      .ToListAsync();

    protected async Task<PaginateResult<TEntity>> PaginateByPatternAsync(
      Expression<Func<TEntity, bool>> pattern,
      int page,
      int limit
    ) {
      var allItems = Set.Where(pattern);
      var allItemsCount = await allItems.CountAsync();

      return new PaginateResult<TEntity> {
        Items = await allItems.Skip(page * limit).Take(limit).ToListAsync(),
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

    protected async Task RemoveByIdAsync(int id) {
      var entity = await ByIdAsync(id);
      Set.Remove(entity);

      await SaveChangesAsync();
    }

    protected async Task<List<TEntity>> RemoveManyByPatternAsync(Expression<Func<TEntity, bool>> pattern) {
      var entitiesToRemove = await Set.Where(pattern).ToListAsync();

      Set.RemoveRange(entitiesToRemove);
      await SaveChangesAsync();

      return entitiesToRemove;
    }

    protected async Task<int> CountAsync(Expression<Func<TEntity, bool>> pattern) => await Set.CountAsync(pattern);

    protected async Task UpdateByIdAsync(int id, Action<TEntity> updateCallback) {
      var entity = await ByIdAsync(id);

      updateCallback(entity);

      await SaveChangesAsync();
    }
  }
}