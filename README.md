# Csharp_W_CSF_and_CSF_Learn001
Csharp_W_CSF_and_CSF_Learn001

Projeto de aprendizado para acessar bancos de dados MySql ADO.NET framework e Swagger.

Dependências:

    $ dotnet add package MySql.Data    

    $ dotnet add Csharp_W_CSF_and_CSF_Learn001.csproj package Swashbuckle.AspNetCore -v 5.5.0

    $ dotnet add System.Configuration.ConfigurationManager


# OBS.:
    o pacote "System.Configuration.ConfigurationManager" foi instalado pois depois de um tempo com projeto parado e sem alterações, quando rodei novamente começou a apresentar erro nas requisições. depois de fazer debug do código, foi descoberta uma exceção que estava sendo lançada e pedia esse pacote.


Para remover uma Dependência instalada acima use o comando:
    
    dotnet remove package NOME-DO-PACOTE

Por exemolo:

    dotnet remove package MySql.Data

