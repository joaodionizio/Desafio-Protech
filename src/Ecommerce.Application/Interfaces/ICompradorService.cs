using Ecommerce.Application.Dtos.Requests;
using Ecommerce.Application.Dtos.Responses;

namespace Ecommerce.Application.Interfaces;

public interface ICompradorService
{
    Task<CompradorResponse> CriarAsync(CriarCompradorRequest request);

    Task<List<CompradorResponse>> ListarAsync();

    Task<CompradorResponse> BuscarPorIdAsync(Guid id);
}