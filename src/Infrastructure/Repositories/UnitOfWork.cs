using Domain.Repositories;

using Infrastructure.Database;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
