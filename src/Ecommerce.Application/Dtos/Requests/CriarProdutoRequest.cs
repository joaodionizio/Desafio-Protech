namespace Ecommerce.Application.Dtos.Requests;

public class CriarProdutoRequest
{
    public string Nome { get; set; } = string.Empty;

    public decimal Preco { get; set; }
}