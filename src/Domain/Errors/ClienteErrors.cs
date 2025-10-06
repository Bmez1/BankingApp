using SharedKernel;

namespace Domain.Errors
{
    public static class ClienteErrors
    {
        public static Error IdentificacionNotUnique(string identificacion) => Error.Conflict(
            "Cliente.IdentificacionNotUnique",
        $"El cliente con el Id = '{identificacion}' ya existe.");

        public static Error NotFound(string identificacion) => Error.NotFound(
            "Cliente.NotFound",
        $"El cliente con el Id = '{identificacion}' no existe.");
    }
}
