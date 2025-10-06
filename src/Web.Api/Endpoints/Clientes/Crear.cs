using Application.Abstractions.Messaging;
using Application.Clientes.Crear;

using Domain.Enums;

using SharedKernel;

using System.ComponentModel.DataAnnotations;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Clientes;

internal sealed class Crear : IEndpoint
{
    public record CreateClienteRequest(
        [Required] string Nombre,
        Genero Genero,
        [Required] string Identificacion,
        [Required] DateTime FechaNacimiento,
        [Required] string Direccion,
        [Required] string Telefono,
        [Required] string Contrasena
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("clientes", async (
            CreateClienteRequest request,
            ICommandHandler<CrearClienteCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CrearClienteCommand(
                request.Nombre,
                request.Genero,
                request.Identificacion,
                request.FechaNacimiento,
                request.Direccion,
                request.Telefono,
                request.Contrasena
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Clientes);
    }
}
