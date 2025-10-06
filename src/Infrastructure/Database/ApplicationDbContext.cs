using Domain.Entities;

using Infrastructure.DomainEvents;
using Infrastructure.Outbox;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Cliente>().HasIndex(x => x.Identificacion).IsUnique();
        modelBuilder.Entity<Cuenta>().HasIndex(x => x.NumeroCuenta).IsUnique();

        modelBuilder.Entity<OutboxMessage>()
           .HasIndex(x => new { x.OccurredOnUtc, x.ProcessedOnUtc })
           .HasFilter("[ProcessedOnUtc] IS NULL")
           .HasDatabaseName("IX_OutboxMessages_Unprocessed")
           .IncludeProperties(x => new { x.Id, x.Type, x.Content });

        modelBuilder.Entity<OutboxMessage>(x =>
            x.Property(x => x.Content)
             .HasColumnType("nvarchar(max)"));


        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);
        configurationBuilder.Properties<decimal>()
           .HavePrecision(14, 2);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        await PublishDomainEventsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        var outboxMessages = domainEvents.Select(domainEvent => new OutboxMessage(
            domainEvent.GetType().Name,
            JsonConvert.SerializeObject(
                domainEvent,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
            )))
            .ToList();

        // Agregar los mensajes de outbox al DbContext
        await OutboxMessages.AddRangeAsync(outboxMessages, cancellationToken);
    }
}
