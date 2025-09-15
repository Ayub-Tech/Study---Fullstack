using System.Linq.Expressions;
using Study.Domain.Common;

namespace Study.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
	Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
	Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);
	Task<T> AddAsync(T entity, CancellationToken ct = default);
	Task UpdateAsync(T entity, CancellationToken ct = default);
	Task DeleteAsync(T entity, CancellationToken ct = default);
	IQueryable<T> Query();
}
