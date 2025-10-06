using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;

using Domain.Entities;
using Domain.Errors;
using Domain.Events;
using Domain.Repositories;

using SharedKernel;

namespace Application.Clientes.Crear;

public class CrearClienteCommandHandler(IUnitOfWork unitOfWork,
    IClienteRepository clienteRepository,
    IPasswordHasher passwordHasher) : ICommandHandler<CrearClienteCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CrearClienteCommand request, CancellationToken cancellationToken)
    {
        if (await clienteRepository.ExistsByConditiondAsync(cl
            => cl.Identificacion == request.Identificacion, cancellationToken: cancellationToken))
        {
            return Result.Failure<Guid>(ClienteErrors.IdentificacionNotUnique(request.Identificacion));
        }

        var cliente = Cliente.Create(
            request.Nombre,
            request.Identificacion,
            request.Direccion,
            request.Genero.ToString(),
            request.Telefono,
            request.FechaNacimiento,
            passwordHasher.Hash(request.Contrasena));

        // Se genera evento de dominio al crear un cliente nuevo
        cliente.Raise(new ClienteRegistradoEvent(cliente.Id));

        await clienteRepository.AddAsync(cliente, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return cliente.Id;
    }
}
