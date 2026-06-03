using Ecommerce.Api.Middlewares;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    var mensagens = options.ModelBindingMessageProvider;

    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) =>
        $"O valor '{valor}' não é válido para o campo {campo}.");
    mensagens.SetMissingBindRequiredValueAccessor(campo =>
        $"O campo {campo} é obrigatório.");
    mensagens.SetMissingKeyOrValueAccessor(() =>
        "Valor obrigatório não informado.");
    mensagens.SetUnknownValueIsInvalidAccessor(campo =>
        $"O valor informado para o campo {campo} não é válido.");
    mensagens.SetValueIsInvalidAccessor(valor =>
        $"O valor '{valor}' não é válido.");
    mensagens.SetValueMustBeANumberAccessor(campo =>
        $"O campo {campo} deve ser um número.");
    mensagens.SetValueMustNotBeNullAccessor(_ =>
        "O campo é obrigatório.");
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var detalhes = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Ocorreram um ou mais erros de validação.",
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Instance = context.HttpContext.Request.Path
        };

        detalhes.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

        return new BadRequestObjectResult(detalhes);
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
