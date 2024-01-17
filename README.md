# fiap.tech-challenge.fast-food.api

## Tecnologias
* [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

## Arquitetura

## Execução

### PostgreSQL

Para subir o BD local, o recomendado é utilizar o Docker e executar o seguinte comando:

``` shell
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres
```

Caso seja necessário derrubar o BD, basta executar:

``` shell
docker container kill pg-docker
```

Obs.: A *connection string* já está configurada corretamente no arquivo *launchSettings.json*

### .NET CLI

Inicie a Aplicação (API):

``` shell
dotnet run --project .\src\API\API.csproj
```

Acesse o Swagger UI da API: `http://localhost:5054/swagger`

### Docker
