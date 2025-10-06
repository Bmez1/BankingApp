using Domain.Events;

using Serilog;

using SharedKernel;

namespace Application.Clientes.Crear
{
    internal sealed class ClienteRegistradoEventHandler(ILogger logger) : IDomainEventHandler<ClienteRegistradoEvent>
    {
        public async Task Handle(ClienteRegistradoEvent domainEvent, CancellationToken cancellationToken)
        {
            await Task.Delay(20000, cancellationToken);

            logger.Information("Handling Cliente Registrado event for {ClienteId}", domainEvent.ClienteId);
        }
    }
}
