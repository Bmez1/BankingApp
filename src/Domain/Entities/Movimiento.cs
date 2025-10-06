using SharedKernel;

namespace Domain.Entities;

public class Movimiento : BaseEntity
{
    public DateTime Fecha { get; set; }
    public string TipoMovimiento { get; set; } = default!;
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public Guid CuentaId { get; set; }
    public Cuenta Cuenta { get; set; } = default!;
}
