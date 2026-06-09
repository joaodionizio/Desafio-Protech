namespace Ecommerce.Application.Dtos.Responses;

public class ResultadoPaginadoResponse<T>
{
    public List<T> Items { get; set; } = [];

    public int PaginaAtual { get; set; }

    public int TamanhoPagina { get; set; }

    public int TotalItens { get; set; }

    public int TotalPaginas { get; set; }
}
