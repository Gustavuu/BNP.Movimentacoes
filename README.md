# Projeto de Movimentações Manuais - Teste Técnico

Esta aplicação foi desenvolvida como solução para o teste técnico, com o objetivo de gerenciar movimentações manuais de produtos financeiros.

## Desenvolvido por
Gustavo Irentti

## 💻 Tecnologias Utilizadas

* **.NET 8 (LTS)**
* **ASP.NET Core (MVC + API):** Utilizado para servir tanto a interface visual quanto os endpoints REST da API.
* **Entity Framework Core (Code-First):** ORM para o mapeamento MER e gerenciamento do banco de dados.
* **SQL Server LocalDB:** Banco de dados utilizado para desenvolvimento e execução local.
* **Testes Unitários:** xUnit, Moq e Shouldly.
* **Front-end:** Razor Pages com HTML, CSS e JavaScript.

## 🏛️ Decisões de Arquitetura

A solução foi estruturada seguindo as melhores práticas de desenvolvimento para garantir um código limpo, testável e de fácil manutenção, aplicando os princípios do **SOLID** e conceitos do **DDD (Domain-Driven Design)**.

A decisão de utilizar uma aplicação unificada (MVC + API) com Razor Pages foi tomada para facilitar a execução do teste em um único projeto, agilizando a entrega e a avaliação.

### Clean Architecture

O projeto foi dividido em camadas de responsabilidade, seguindo os princípios da Clean Architecture:

* **Dominio:** Contém as entidades de negócio (`Produto`, `ProdutoCosif`, `MovimentoManual`).
* **Aplicacao:** Orquestra a lógica de negócio através de uma camada de serviço (`MovimentoManualService`) e define os contratos (interfaces) para a camada de infraestrutura.
* **Infraestrutura:** Implementa os contratos de acesso a dados (Repositórios) utilizando o Entity Framework Core.
* **Apresentacao:** Projeto ASP.NET Core que contém os `ApiControllers` (JSON,  documentados pelo Swagger) e os `Controllers` MVC (que servem a interface visual).
* **Testes:** Projeto dedicado aos testes unitários com xUnit.

### Entity Framework Core (Code-First com Migrations)

A abordagem **Code-First** foi utilizada, permitindo que o modelo de banco de dados seja gerado e versionado diretamente a partir das classes em C#. As **Migrations** do EF Core garantem que o banco de dados possa ser criado e atualizado de forma automática.

## 🚀 Como Executar o Projeto

**Pré-requisitos:**
* Visual Studio 2022, que inclui o SQL Server LocalDB.
* .NET 8 SDK.

**Passos:**
1.  Clone este repositório para o seu computador.
2.  Abra o arquivo de solução (`BNP.Movimentacoes.sln`) no Visual Studio.
3.  Defina o projeto `BNP.Movimentacoes.Apresentacao` como o projeto de inicialização (clique com o botão direito no projeto > "Set as Startup Project").
4.  Verifique se está selecionado `https` para executar a aplicação.
5.  Pressione **F5** ou clique no botão de "Start" (▶️).

A aplicação irá iniciar. **Na primeira execução**, o Entity Framework Migrations criará o banco de dados `BNP_MovimentacoesDB` no seu SQL Server LocalDB e o populará com os dados iniciais de domínio automaticamente. O navegador será aberto na página principal da aplicação.

O banco de dados pode ser visualizado e consultado utilizando o SQL Server Object Explorer em: 
`View > SQL Server Object Explorer`

**Links Úteis:**

URL Aplicação rodando local em https: https://localhost:7180/

URL Swagger rodando local em https: https://localhost:7180/swagger

## 🧪 Como Executar os Testes

* **Via Visual Studio:**
    1.  Abra o "Gerenciador de Testes" no menu `Testar > Gerenciador de Testes`.
    2.  Clique em "Executar Todos os Testes".

* **Via Linha de Comando:**
    1.  Abra um terminal na pasta raiz da solução.
    2.  Execute o comando: `dotnet test`.