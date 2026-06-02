namespace Ecommerce.Application.Dtos.Requests;

public class AtualizarPedidoRequest
{
    public List<ItemPedidoRequest> Itens { get; set; } = [];
}