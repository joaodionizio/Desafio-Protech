using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Dtos.Requests;

public class PedidoFiltroRequest
{
    private const int TamanhoPaginaPadrao = 10;
    private const int TamanhoPaginaMaximo = 50;
    private int _pagina = 1;
    private int _tamanhoPagina = TamanhoPaginaPadrao;

    public StatusPedido? Status { get; set; }

    public Guid? CompradorId { get; set; }

    public int Pagina
    {
        get => _pagina;
        set => _pagina = value <= 0 ? 1 : value;
    }

    public int TamanhoPagina
    {
        get => _tamanhoPagina;
        set => _tamanhoPagina = value <= 0
            ? TamanhoPaginaPadrao
            : Math.Min(value, TamanhoPaginaMaximo);
    }
}
