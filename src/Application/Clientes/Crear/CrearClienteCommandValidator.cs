using FluentValidation;

namespace Application.Clientes.Crear;

public class CrearClienteCommandValidator : AbstractValidator<CrearClienteCommand>
{
    public CrearClienteCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty();
        RuleFor(x => x.Genero).NotEmpty();
        RuleFor(x => x.Identificacion).NotEmpty();
        RuleFor(x => x.Direccion).NotEmpty();
        RuleFor(x => x.Telefono).NotEmpty();
        RuleFor(x => x.Contrasena)
            .NotEmpty()
            .MinimumLength(6);
    }
}
