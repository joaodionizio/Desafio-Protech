using System.Text.Json;
using Ecommerce.Application.Exceptions;

namespace Ecommerce.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntidadeNaoEncontradaException ex)
        {
            await EscreverRespostaAsync(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }
        catch (RegraDeNegocioException ex)
        {
            await EscreverRespostaAsync(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Erro inesperado ao processar {Metodo} {Caminho}. TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier);

            await EscreverRespostaAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "Ocorreu um erro interno no servidor.");
        }
    }

    private static async Task EscreverRespostaAsync(
        HttpContext context,
        int statusCode,
        string mensagem)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var resposta = new
        {
            mensagem
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(resposta));
    }
}
