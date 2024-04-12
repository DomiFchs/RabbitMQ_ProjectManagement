using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;

namespace Domain.Repositories.Implementations;


public abstract class ARepository<TEntity>(IDbContextFactory<ProjectsDbContext> contextFactory) : IRepository<TEntity>
    where TEntity : class {
    public virtual async Task<List<TEntity>> ReadAsync(CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().ToListAsync(ct);
    }

    public virtual async Task<TEntity?> ReadAsync(int id, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.FindAsync(new object[] { id }, ct);
    }

    public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().Where(filter).ToListAsync(ct);
    }


    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().FirstOrDefaultAsync(filter, ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.FindAsync(new object[] { id }, ct) != null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().IgnoreAutoIncludes().AnyAsync(filter, ct);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        await table.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        await table.AddRangeAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        table.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        table.UpdateRange(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        table.Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        table.RemoveRange(entities);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await GetContextAsync();
        var table = context.Set<TEntity>();
        var entities = await table.AsNoTracking().IgnoreAutoIncludes().Where(filter).ToListAsync(ct);

        table.RemoveRange(entities);
        await context.SaveChangesAsync(ct);
    }

    protected async Task<ProjectsDbContext> GetContextAsync() {
        var context = await contextFactory.CreateDbContextAsync();
        return context;
    }
}