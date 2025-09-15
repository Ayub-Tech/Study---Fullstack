using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Study.Application.Common.Interfaces;
using Study.Domain.Common;
using Study.Infrastructure.Persistence;

namespace Study.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _db;
    public Repository(AppDbContext db) => _db = db;

    public IQueryable<T> Query() => _db.Set<T>().AsQueryable();

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Set<T>().FindAsync([id], ct);

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
    {
        var q = _db.Set<T>().AsQueryable();
        if (predicate != null) q = q.Where(predicate);
        return await q.ToListAsync(ct);
    }

    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
