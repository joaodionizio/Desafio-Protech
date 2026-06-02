using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Dtos.Requests;

public class PedidoFiltroRequest
{
    public StatusPedido? Status { get; set; }

    public Guid? CompradorId { get; set; }
}