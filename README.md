# Projeto de Estacionamento

Este é um projeto de exemplo para um sistema de estacionamento. Ele foi desenvolvido utilizando ASP.NET Core e Entity Framework Core para o acesso aos dados.

## Requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (versão 3.1 ou superior)
- [Visual Studio Code](https://code.visualstudio.com/) (opcional, mas recomendado)
- [PostgreSQL](https://www.postgresql.org/download/) ou outro provedor de banco de dados suportado pelo Entity Framework Core

## Configuração do Banco de Dados

1. Certifique-se de ter o PostgreSQL instalado e configurado corretamente em sua máquina ou em um servidor acessível.
2. Abra o arquivo `appsettings.json` no projeto.
3. Verifique e, se necessário, atualize a string de conexão no `DefaultConnection` para apontar para o seu banco de dados PostgreSQL. A sintaxe da string de conexão deve seguir o padrão `Server=myServerAddress;Port=myPort;Database=myDatabase;User Id=myUsername;Password=myPassword;`.

## Executando as Migrações

1. Abra um terminal na pasta raiz do projeto.
2. Execute o comando `dotnet ef database update` para aplicar as migrações e criar o banco de dados no PostgreSQL.

## Executando o Projeto

1. No terminal, navegue até a pasta raiz do projeto.
2. Execute o comando `dotnet run` para iniciar o servidor web.
3. Abra um navegador e acesse `https://localhost:5001` para ver a aplicação em execução.

## Documentação da API

A documentação da API está disponível na rota `/swagger`. Você pode acessá-la em `https://localhost:5001/swagger`.

## Contribuindo

Sinta-se à vontade para enviar pull requests e propor melhorias para o projeto.
