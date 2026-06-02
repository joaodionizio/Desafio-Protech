namespace Ecommerce.Domain.Entities;

public class ItemPedido
{
    public Guid Id { get; set; }

    public Guid PedidoId { get; set; }

    public Pedido Pedido { get; set; } = null!;

    public Guid ProdutoId { get; set; }

    public Produto Produto { get; set; } = null!;

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public decimal CalcularSubtotal()
    {
        return PrecoUnitario * Quantidade;
    }
}