using SharedKernel;

namespace Domain.Entities;

public class Cuenta : BaseEntity
{
    public string NumeroCuenta { get; set; } = default!;
    public string TipoCuenta { get; set; } = default!;
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaApertura { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public ICollection<Movimiento> Movimientos { get; set; } = [];
}
