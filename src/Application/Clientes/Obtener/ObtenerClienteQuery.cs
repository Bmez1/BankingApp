using Application.Abstractions.Messaging;

using Domain.Dtos;

namespace Application.Clientes.Obtener;

public record ObtenerClienteQuery(Guid Id) : IQuery<ClienteResponse>;
