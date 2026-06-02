using Ecommerce.Application.Dtos.Requests;
using Ecommerce.Application.Dtos.Responses;

namespace Ecommerce.Application.Interfaces;

public interface IProdutoService
{
    Task<ProdutoResponse> CriarAsync(CriarProdutoRequest request);

    Task<List<ProdutoResponse>> ListarAsync();

    Task<ProdutoResponse> BuscarPorIdAsync(Guid id);
}