A solução gerarda pelo `.NET Core CLI` utilizando o [SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1). A solução é composta por um projeto `WebAPI` e um projeto `NUnit` para testes unitários.

## Executar em desenvolvimento

Antes de executar o projeto, executar o comando `dotnet restore` no diretorio do projeto para restaurar as dependências.
Para iniciar o servidor dev, execute o comando `dotnet run`. O navegador será iniciado com o site no endereço `http://localhost:5050/`.

## Build

Executar o comando `dotnet build` no diretório do projeto.

## Testes
Os testes unitários foram desenvolvidos utilizando o [NUnit 3.12.0](https://www.nuget.org/packages/NUnit/3.12.0). O projeto backend foi referenciado diretamente no projeto de testes e não existe dependência de execução. Para executar os testes utilize o comando `dotnet test`
