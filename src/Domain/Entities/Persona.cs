namespace Domain.Entities;

public abstract class Persona : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Genero { get; set; } = default!;
    public DateTime FechaNacimiento { get; set; } = default!;
    public string Identificacion { get; set; } = default!;
    public string Direccion { get; set; } = default!;
    public string Telefono { get; set; } = default!;
}
