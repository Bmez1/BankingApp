using SharedKernel;

namespace Domain.Events
{
    public sealed record ClienteRegistradoEvent(Guid ClienteId) : IDomainEvent;
}
