using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Requests;

public class CriarPedidoRequest
{
    [Required(ErrorMessage = "O comprador é obrigatório.")]
    public Guid CompradorId { get; set; }

    [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
    [MinLength(1, ErrorMessage = "O pedido deve possuir pelo menos um item.")]
    public List<ItemPedidoRequest> Itens { get; set; } = [];
}
