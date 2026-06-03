# Ecommerce API

API REST desenvolvida em ASP.NET Core para gerenciamento de compradores, produtos e pedidos de um e-commerce.

O objetivo do projeto foi atender aos requisitos propostos no desafio técnico, implementando regras de negócio, persistência com SQL Server e uma estrutura organizada em camadas.

## Tecnologias Utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* SQL Server 2022
* Docker Compose para SQL Server
* Swagger

## Estrutura Do Projeto

```text
src
├── Ecommerce.Api
├── Ecommerce.Application
├── Ecommerce.Domain
└── Ecommerce.Infrastructure
```

### Ecommerce.Api

Responsável pelos endpoints da API, configuração do Swagger, health check e middleware global de exceções.

### Ecommerce.Application

Contém os DTOs, interfaces e exceções da aplicação.

### Ecommerce.Domain

Contém as entidades, enums e regras do domínio.

### Ecommerce.Infrastructure

Contém o Entity Framework Core, DbContext, migrations e implementações dos serviços.

## Regras De Negócio

### Pedido

* Todo pedido deve possuir um comprador válido.
* Todo pedido deve possuir pelo menos um item.
* Cada item deve possuir quantidade maior que zero.
* O produto informado no item deve existir.
* Apenas pedidos iniciados podem ser excluídos.

### Produto

* Nome obrigatório.
* Preço deve ser maior que zero.
* Nome limitado a 150 caracteres.

### Comprador

* Nome obrigatório.
* E-mail obrigatório e com formato válido.
* Nome limitado a 150 caracteres.
* E-mail limitado a 200 caracteres.
* E-mail não pode ser duplicado.

### Fluxo De Status

Status disponíveis:

* Iniciado
* Processado
* Enviado
* Cancelado

Fluxo permitido:

```text
Iniciado -> Processado -> Enviado
```

Também é permitido:

```text
Iniciado -> Cancelado
Processado -> Cancelado
```

Restrições implementadas:

* Apenas pedidos iniciados podem ser alterados.
* Apenas pedidos iniciados podem ser processados.
* Apenas pedidos processados podem ser enviados.
* Apenas pedidos iniciados ou processados podem ser cancelados.

## Como Executar

### 1. Subir o banco de dados

```bash
docker compose up -d
```

O projeto utiliza SQL Server em Docker.

Credenciais utilizadas:

```text
Usuário: sa
Senha: DesafioProtech@22
```

Essas credenciais são destinadas exclusivamente ao ambiente local do desafio.

### 2. Restaurar dependências

```bash
dotnet restore Ecommerce.sln
```

### 3. Executar a aplicação

```bash
dotnet run --project src/Ecommerce.Api
```

As migrations são aplicadas automaticamente na inicialização da aplicação.

## Testes

Os testes unitários do domínio estão em `tests/Ecommerce.Domain.Tests`.

```bash
dotnet test tests/Ecommerce.Domain.Tests/Ecommerce.Domain.Tests.csproj
```

## Swagger

Após executar a aplicação:

```text
http://localhost:5115/swagger
```

## Health Check

```http
GET /health
```

## Endpoints

### Compradores

| Método | Endpoint                 |
| ------ | ------------------------ |
| POST   | /api/v1/compradores      |
| GET    | /api/v1/compradores      |
| GET    | /api/v1/compradores/{id} |

### Produtos

| Método | Endpoint              |
| ------ | --------------------- |
| POST   | /api/v1/produtos      |
| GET    | /api/v1/produtos      |
| GET    | /api/v1/produtos/{id} |

### Pedidos

| Método | Endpoint                       |
| ------ | ------------------------------ |
| POST   | /api/v1/pedidos                |
| GET    | /api/v1/pedidos                |
| GET    | /api/v1/pedidos/{id}           |
| PUT    | /api/v1/pedidos/{id}           |
| DELETE | /api/v1/pedidos/{id}           |
| PATCH  | /api/v1/pedidos/{id}/processar |
| PATCH  | /api/v1/pedidos/{id}/enviar    |
| PATCH  | /api/v1/pedidos/{id}/cancelar  |

### Filtros De Pedidos

A listagem de pedidos permite filtro por:

* Status
* Comprador

Exemplos:

```http
GET /api/v1/pedidos?status=Iniciado
```

```http
GET /api/v1/pedidos?compradorId={guid}
```

## Tratamento De Erros

A aplicação possui middleware global para tratamento de exceções.
Erros inesperados também são registrados em log com método HTTP, caminho da requisição e TraceId.

Retornos implementados:

| Situação                  | Status |
| ------------------------- | ------ |
| Entidade não encontrada   | 404    |
| Regra de negócio inválida | 400    |
| Erro inesperado           | 500    |

## Diferenciais Implementados

* Arquitetura em camadas
* Entity Framework Core
* SQL Server
* SQL Server disponível via Docker Compose
* Swagger
* Health Check
* Middleware global de exceções
* Filtro de pedidos por status e comprador
* Migrations automáticas na inicialização
* Validações declarativas nos DTOs
* Índice único para e-mail de comprador
* Testes unitários das regras de domínio do pedido

## Observações Técnicas

* As datas são preenchidas com `DateTime.UtcNow` de forma padronizada.
* A API usa `Guid` como identificador das entidades.
* Os serviços acessam o `ApplicationDbContext` diretamente para manter o escopo do projeto simples.
