using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Requests;

public class CriarProdutoRequest
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O nome do produto deve possuir no máximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public decimal Preco { get; set; }
}
