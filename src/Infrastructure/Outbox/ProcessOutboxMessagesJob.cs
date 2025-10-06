using Infrastructure.Database;
using Infrastructure.DomainEvents;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using SharedKernel;

namespace Infrastructure.Outbox;

public sealed class ProcessOutboxMessagesJob : BackgroundService
{
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IServiceProvider _serviceProvider;
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public ProcessOutboxMessagesJob(IServiceProvider serviceProvider,
        ILogger<ProcessOutboxMessagesJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Job hospedado iniciado");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessMessagesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task ProcessMessagesAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventsDispatcher>();

        var messages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(30)
            .ToListAsync(stoppingToken);

        foreach (var message in messages)
        {
            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, JsonSerializerSettings);

                if (domainEvent is null)
                {
                    _logger.LogWarning("No se pudo deserializar el evento de dominio para el mensaje {MessageId}", message.Id);
                    message.SetError("Deserialization failed");
                    continue;
                }
                await dispatcher.DispatchAsync(domainEvent, stoppingToken);
                _logger.LogInformation("Mensaje de outbox procesado con éxito {MessageId}", message.Id);

                message.MarkAsProcessed(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando el mensaje de outbox {MessageId}", message.Id);
                message.SetError(ex.ToString());
            }
        }

        if (messages.Count > 0)
        {
            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
