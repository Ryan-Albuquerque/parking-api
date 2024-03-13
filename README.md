# Projeto de Estacionamento

Este � um projeto de exemplo para um sistema de estacionamento. Ele foi desenvolvido utilizando ASP.NET Core e Entity Framework Core para o acesso aos dados.

## Requisitos

Antes de come�ar, certifique-se de ter instalado em sua m�quina:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (vers�o 3.1 ou superior)
- [Visual Studio Code](https://code.visualstudio.com/) (opcional, mas recomendado)
- [PostgreSQL](https://www.postgresql.org/download/) ou outro provedor de banco de dados suportado pelo Entity Framework Core

## Configura��o do Banco de Dados

1. Certifique-se de ter o PostgreSQL instalado e configurado corretamente em sua m�quina ou em um servidor acess�vel.
2. Abra o arquivo `appsettings.json` no projeto.
3. Verifique e, se necess�rio, atualize a string de conex�o no `DefaultConnection` para apontar para o seu banco de dados PostgreSQL. A sintaxe da string de conex�o deve seguir o padr�o `Server=myServerAddress;Port=myPort;Database=myDatabase;User Id=myUsername;Password=myPassword;`.

## Executando as Migra��es

1. Abra um terminal na pasta raiz do projeto.
2. Execute o comando `dotnet ef database update` para aplicar as migra��es e criar o banco de dados no PostgreSQL.

## Executando o Projeto

1. No terminal, navegue at� a pasta raiz do projeto.
2. Execute o comando `dotnet run` para iniciar o servidor web.
3. Abra um navegador e acesse `https://localhost:5001` para ver a aplica��o em execu��o.

## Documenta��o da API

A documenta��o da API est� dispon�vel na rota `/swagger`. Voc� pode acess�-la em `https://localhost:5001/swagger`.

## Contribuindo

Sinta-se � vontade para enviar pull requests e propor melhorias para o projeto.
