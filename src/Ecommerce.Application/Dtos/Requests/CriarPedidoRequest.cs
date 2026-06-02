namespace Ecommerce.Application.Dtos.Requests;

public class CriarPedidoRequest
{
    public Guid CompradorId { get; set; }

    public List<ItemPedidoRequest> Itens { get; set; } = [];
}