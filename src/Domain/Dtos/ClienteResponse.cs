using Domain.Entities;

namespace Domain.Dtos
{
    public record ClienteResponse(
        Guid Id,
        string Nombre,
        string Identificacion,
        int Edad,
        DateTime FechaNacimiento,
        string Direccion,
        string Telefono,
        bool Activo
        )
    {
        public static ClienteResponse From(Cliente cliente) => new(cliente.Id, cliente.Nombre, cliente.Identificacion, cliente.GetEdad(), cliente.FechaNacimiento, cliente.Direccion, cliente.Telefono, cliente.Estado);
    };
}
