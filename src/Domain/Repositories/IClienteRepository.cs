using Domain.Entities;

using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IClienteRepository : IRepository
    {
        Task<Cliente?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<bool> ExistsByConditiondAsync(Expression<Func<Cliente, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);

        Task AddAsync(Cliente cliente, CancellationToken cancellationToken = default);
        void Update(Cliente cliente, CancellationToken cancellationToken = default);
        void Delete(Cliente cliente, CancellationToken cancellationToken = default);
    }
}
