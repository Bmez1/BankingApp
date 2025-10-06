using Domain.Entities;
using Domain.Repositories;

using Infrastructure.Database;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ClienteRepository(ApplicationDbContext context) : IClienteRepository
    {
        public async Task<Cliente?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<Cliente> query = context.Clientes;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public void Update(Cliente cliente, CancellationToken cancellationToken = default)
        {
            context.Clientes.Update(cliente);
        }

        public void Delete(Cliente cliente, CancellationToken cancellationToken = default)
        {
            context.Clientes.Remove(cliente);
        }

        public async Task AddAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(cliente, cancellationToken);
        }

        public async Task<bool> ExistsByConditiondAsync(Expression<Func<Cliente, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<Cliente> query = context.Clientes;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.AnyAsync(predicate, cancellationToken);
        }
    }
}
