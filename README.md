# fiap.tech-challenge.fast-food.api

## Introdução
Repositório para o projeto DDD da Pós Graduação FIAP - Software Architecture.
O Tech Challenge Grupo 19 é composto por:
- Danilo Queiroz da Silva
- Elton Douglas Souza
- Leandro da Silva Câmara
- Marcelo Patricio da Silva
- Melchior Felix de Moraes


## Tecnologias
* [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* [ Entity Framework Core 8.0 ](https://devblogs.microsoft.com/dotnet/announcing-ef8/)
* [ OpenAPI - Swashbuckle ](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle)
* [ ReDoc ](https://github.com/Redocly/redoc)
* [ FluentMigrator ](https://fluentmigrator.github.io/)
* [ Postgresql ](https://www.postgresql.org/)

## Arquitetura
A arquitetura da aplicação é a Clean Architecture. Para a camada Entities, foi adotado o Domain Driven Design.
Segue a estrutura da aplicação:

    .
    ├── Drivers                     # Frameworks & Drivers
        ├── API                     # Web API (.NET 8)
            ├── HealthChecks
            └── Routers
        └── External                # External Interfaces & DB
            ├── Clients
            └── Persistence
                ├── Migrations
                └── Repositories
    ├── Adapters                    # Interface Adapters
        ├── Controllers
        └── Gateways
    └── Core                        # Business Rules
        ├── Application
            └── UseCases
        └── Entities (Domain)
            ├── BoundedContext
                └── Model           # Aggregates - entities and value objects
            └── SeedWork            # Reusable classes/interfaces for the domain (by Martin Fowler)

## Execução
O projeto pode ser executado utilizando o Docker.

### AWS (Academy)

Após a execução dos [_Workflows_](https://github.com/leandrocamara/fiap.tech-challenge.fast-food.api/actions) (GitHub Actions), a aplicação é implantada no **_Amazon EKS_** (_Elastic Kubernetes Service_) e se conecta ao **_Amazon RDS_** (_Relational Database Service_).

Pré-requisitos:
1. Executar o [_Workflow_](https://github.com/leandrocamara/fiap.tech-challenge.fast-food.infra.k8s) do repositório do **_EKS_**;
2. Executar o [_Workflow_](https://github.com/leandrocamara/fiap.tech-challenge.fast-food.infra.database) do repositório do **_RDS_**.

Há duas maneiras de executar e implantar a aplicação na _AWS_:

1. Realizando um `push` na `main`, por meio de um `Merge Pull Request`;

2. Executando o [_Manual Deployment_](https://github.com/leandrocamara/fiap.tech-challenge.fast-food.api/actions/workflows/manual-deployment.yaml) (_Workflow_)

    ![Manual Deployment](./docs/manual-deployment.png)

    2.1. Por padrão, o _Workflow_ utilizará as `Secrets` configuradas no projeto. Caso esteja utilizando o `AWS Academy`, recomenda-se informar as credencias da conta. **Obs.:** Cada sessão do _AWS Academy_ dura **4 horas**.

### Docker Compose

Para iniciar o banco de dados (_Postgres_) e a aplicação, utilize o comando abaixo abaixo no seu prompt shell (windows ou linux):
```shell
docker compose up --build
```

Remover os _containers_:
```shell
docker compose down
```

Remover o _volume_ de dados do postgresql:
```shell
docker volume rm fastfood_data
```

Após o build do banco e das aplicações, você pode acessar as informações OpenAPI de duas formas:
Swagger UI da API: `http://localhost:8080/swagger` (***Recomendado para testar as chamadas aos endpoints***)
ReDoc UI da API: `http://localhost:8080/api-docs` (***Recomendado para visualização amigável***)

### CLI
Para iniciar a aplicação e banco de dados separadamente, siga os passos abaixo.

Para subir o BD local, o recomendado é utilizar o Docker e executar o seguinte comando:
```shell
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres
```

Obs.: A *connection string* já está configurada corretamente no arquivo *launchSettings.json*

Inicie a Aplicação (API):
```shell
dotnet run --project .\src\Drivers\API\API.csproj
```

Caso seja necessário derrubar o BD, basta executar:

```shell
docker container kill pg-docker
```


# Como testar

## Schema de Banco de dados
A aplicação conta com uma biblioteca de migrations configurada portanto, ao iniciar a aplicação, o banco de dados será atualizado com as tabelas e dados necessários.

## Autenticação
Na primeira versão da aplicação não há autenticação, portanto basta usar a própria UI do Swagger para fazer as chamadas aos endpoints.

## Endpoints
O detalhe de cada request, como seus parâmetros e tipos estão detalhadas nas interfaces de UI do Swagger e do ReDoc. Nas próximas sessões estão resumidas as operações existentes.

## Produtos
O sistema já possui uma carga de produtos pré cadastrados para serem utilizados nos testes, você pode acessar a lista de produtos por categoria através do método:
`[GET] api/products?category={categoryId}`.

Também é possível criar produtos ou editar, excluir e recuperar produtos pelo seu **id**. Os endpoints são os seguintes:
- `[GET] api/products/{id}`: Deve ser fornecido o id do produto a ser retornado.
- `[POST] api/products`: Criar um novo produto.
- `[PUT] api/products`: Atualizar um produto existente da base de dados.
- `[DELETE] api/products/{id}`: Excluir um produto existente da base de dados.

### Códigos das Categorias de Produtos
- `0`: Lanches
- `1`: Acompanhamentos
- `2`: Bebidas
- `3`: Sobremesas


## Clientes
O sistema dispõe de endpoints para cadastrar clientes para relacioná-los aos pedidos, embora não seja requerido para criação de um pedido.
Não há carga prévia de dados para clientes.

Há dois endpoints para manipulação do cadastro de clientes:
- `[POST] api/customers`: Criar um novo cliente caso o CPF já não se encontre na base de dados.
- `[GET] api/customers?cpf={cpf}`: Retornar um cliente por seu CPF.


## Pedidos
Na versão inicial do sistema, é possível criar um pedido, retornar a lista de pedidos existentes e retornar os detalhes de um pedido por seu **id**.
- `[POST] api/orders`: Criar um novo pedido com o status Pay com os itens e relacionado ao cliente (quando informado).
- `[GET] api/orders/ongoing`: Retorna a lista dos pedidos em andamento, ordenada dos mais antigos para os mais novos.
- `[POST] api/orders/{id}/status`: Atualiza o status de um pedido específico.
- `[GET] api/orders/{id}`: Retorna o detalhe de um pedido informado pelo **{id}**.

### Status dos Pedidos
- `0`: Pagamento Pendente
- `1`: Pagamento Recusado
- `2`: Pedido Recebido na Cozinha
- `3`: Cozinha Preparando Pedido
- `4`: Pedido Pronto
- `5`: Pedido Concluído


## Webhook para atualização de pagamentos
O sistema, em sua versão 2, terá integração com o Mercado Pago (QrCode dinâmico).
Na versão atual existe um endpoint que simula uma chamada do Mercado Pago para o sistema.
Podendo a chamada ser de aceite ou recusa do pagamento.
- `[POST] api/webhook/orders/payment`: Endpoint para representar a chamada do mercado pago informando pagamento aceito ou recusado.

## Fluxo de testes recomendado
Para testar a aplicação recomendamos primeiro:
1. Incluir um produto (podendo também testar atualização e exclusão dos produtos).
2. Incluir um novo cliente (guardando a informação do campo id retornado na inclusão).
3. Buscar lista de produtos por categoria, guardando alguns ids para utilizá-los na criação do pedido.
4. Criar um novo pedido (podendo ou não informar o cliente).
5. Atualizar o status de pagamento de um pedido, simulando a chamada realizada pelo mercado pago ao webhook.
6. Listar todos os pedidos em andamento
7. Atualizar o status do pedido (com pagamento confirmado)
8. Obter informações de um pedido

Todo o fluxo está presente na [Collection do Postman](./docs/postman/FIAP.FastFood.postman_collection.json).

## Arquitetura Kubernetes
Essa é a arquitetura desenhada para a nossa aplicação
![Arquitetura](./docs/DiagramaArquitetura.png)


## Como rodar usando Kubernetes
O projeto possui arquivos YML de configurações para deploy em ambiente Kubernetes.
Ele foi testado usando o Kubernetes incluso com o Docker Desktop.

Os arquivos estão na pasta **/kubernetes** e devem ser executados na seguinte ordem (dentro do diretório **/kubernetes**):
- Criar banco de dados postgresql
  - Criar o persistente volume (PV) para armazenar os dados de maneira persistente
    ```kubectl apply -f .\database\pv.yml```
  - Criar o persistente volume claim (PVC) para armazenar os dados de maneira persistente
    ```kubectl apply -f .\database\pvc.yml```
  - Armazenar as secrets para serem usadas na criação do POD (usuário e senha do banco)
    ```kubectl apply -f .\database\secret.yml```
  - Criar o POD do banco de dados postgresql
    ```kubectl apply -f .\database\pod.yml```
  - Criar o SVC para permitir o acesso da aplicação ao banco de dados através da rede local através da porta **31001**.
    ```kubectl apply -f .\database\service.yml```
- O próximo passo é publicar a aplicação, para isso devem ser seguidas as etapas abaixo:
  - Armazenar as secrets para serem usadas na criação dos PODs da aplicação
    ```kubectl apply -f .\secret.yml```
  - Armazenar as configurações para serem usadas na criação dos PODs da aplicação
    ```kubectl apply -f .\configmap.yml``` 
  - Executar o deploy da aplicação
    ```kubectl apply -f .\deployment.yml``` 
  - Criar o SVC para permitir o acesso a aplicação através da rede local através da porta **31000**.
    ```kubectl apply -f .\service.yml```
  - Por último, criar o auto escalonamento da aplicação.
    ```kubectl apply -f .\hpa.yml```

## Vídeos de apresentação
Alguns vídeos que explicam como implantar a aplicação, uso das APIS e escolhas de design estão em uma [playlist do youtube](https://youtube.com/playlist?list=PLuVYnmmdbgO1ams1lmM4tDwmZxym5vV7w&si=ve6Ck3-chgUc_JFZ):

