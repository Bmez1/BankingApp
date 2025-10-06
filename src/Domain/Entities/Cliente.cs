namespace Domain.Entities;

public class Cliente : Persona
{
    public string Contrasena { get; set; } = default!;
    public bool Estado { get; set; }
    public ICollection<Cuenta> Cuentas { get; set; } = [];
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaActualizacion { get; set; }

    public static Cliente Create(
     string nombre,
     string identificacion,
     string direccion,
     string genero,
     string telefono,
     DateTime fechaNacimiento,
     string contrasena)
    {
        return new Cliente
        {
            Contrasena = contrasena,
            Estado = true,
            FechaRegistro = DateTime.UtcNow,

            Nombre = nombre,
            Genero = genero,
            FechaNacimiento = fechaNacimiento,
            Identificacion = identificacion,
            Direccion = direccion,
            Telefono = telefono
        };
    }

    /// <summary>
    /// Calcula y retorna la edad de la persona en años.
    /// </summary>
    public int GetEdad()
    {
        var hoy = DateTime.Today;
        var edad = hoy.Year - FechaNacimiento.Year;

        if (FechaNacimiento.Date > hoy.AddYears(-edad))
        {
            edad--;
        }
        return edad;
    }
}
