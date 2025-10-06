using Application.Abstractions.Messaging;

using Domain.Dtos;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;

using SharedKernel;

namespace Application.Clientes.Obtener;

public class ObtenerClienteQueryHandler(IClienteRepository repository) : IQueryHandler<ObtenerClienteQuery, ClienteResponse>
{
    public async Task<Result<ClienteResponse>> Handle(ObtenerClienteQuery query, CancellationToken cancellationToken)
    {
        Cliente? cliente = await repository.GetByIdAsync(query.Id, cancellationToken: cancellationToken);

        if (cliente is not null)
        {
            return Result.Failure<ClienteResponse>(ClienteErrors.NotFound(query.Id.ToString()));
        }

        return ClienteResponse.From(cliente!);
    }
}
