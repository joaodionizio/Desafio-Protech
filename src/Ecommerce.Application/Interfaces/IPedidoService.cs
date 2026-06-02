using Ecommerce.Application.Dtos.Requests;
using Ecommerce.Application.Dtos.Responses;

namespace Ecommerce.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoResponse> CriarAsync(CriarPedidoRequest request);

    Task<List<PedidoResponse>> ListarAsync(PedidoFiltroRequest filtro);

    Task<PedidoResponse> BuscarPorIdAsync(Guid id);

    Task AtualizarAsync(Guid id, AtualizarPedidoRequest request);

    Task ProcessarAsync(Guid id);

    Task EnviarAsync(Guid id);

    Task CancelarAsync(Guid id);

    Task ExcluirAsync(Guid id);
}