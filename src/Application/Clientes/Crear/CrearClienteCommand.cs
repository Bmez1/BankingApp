using Application.Abstractions.Messaging;

using Domain.Enums;

namespace Application.Clientes.Crear;

public record CrearClienteCommand(
    string Nombre,
    Genero Genero,
    string Identificacion,
    DateTime FechaNacimiento,
    string Direccion,
    string Telefono,
    string Contrasena
) : ICommand<Guid>;
