# fiap.tech-challenge.fast-food.api

## Tecnologias
* [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

## Arquitetura

## Execução

### Docker Compose

Iniciar o banco de dados (_Postgres_) e a aplicação:
```shell
dotnet compose up
```

Remover os _containers_ e o _volume_:
```shell
docker compose down && docker volume rm fastfood_data
```

Swagger UI da API: `http://localhost:8080/swagger`

### CLI

Para subir o BD local, o recomendado é utilizar o Docker e executar o seguinte comando:

```shell
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres
```

Obs.: A *connection string* já está configurada corretamente no arquivo *launchSettings.json*

Inicie a Aplicação (API):

```shell
dotnet run --project .\src\API\API.csproj
```

Caso seja necessário derrubar o BD, basta executar:

```shell
docker container kill pg-docker
```