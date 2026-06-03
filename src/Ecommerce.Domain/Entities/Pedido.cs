using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }

    public Guid CompradorId { get; set; }

    public Comprador Comprador { get; set; } = null!;

    public StatusPedido Status { get; set; } = StatusPedido.Iniciado;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public DateTime? DataAtualizacao { get; set; }

    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    public decimal CalcularValorTotal()
    {
        return Itens.Sum(item => item.PrecoUnitario * item.Quantidade);
    }

    public bool PodeSerAlterado()
    {
        return Status == StatusPedido.Iniciado;
    }

    public bool PodeSerProcessado()
    {
        return Status == StatusPedido.Iniciado;
    }

    public bool PodeSerCancelado()
    {
        return Status == StatusPedido.Iniciado ||
               Status == StatusPedido.Processado;
    }

    public bool PodeSerEnviado()
    {
        return Status == StatusPedido.Processado;
    }

    public bool PodeSerExcluido()
    {
        return Status == StatusPedido.Iniciado;
    }
}
