namespace Ecommerce.Application.Dtos.Requests;

public class ItemPedidoRequest
{
    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }
}