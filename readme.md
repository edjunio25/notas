# **Sistema de Gerenciamento de Notas Fiscais**

## Integrantes do Grupo
- Douglas Rodrigues Fernandes Filho
- Edson Júnio Bonfim Pinto


## Estatísticas Atuais
**Quantidade de Testes de Unidade**: 41
**Cobertura de Testes**: 72,78% (Backend)

## Apresentação do Projeto

### Conceito
Este é um sistema simples de gerenciamento e armazenamento de notas fiscais.
Temos duas entidades principais que ficam armazenadas em tabelas separadas do banco de dados: **"Empresa"** e **"Nota Fiscal"**.

O usuário pode:
- **Cadastrar uma empresa**
	- Ao cadastrar a empresa, são solicitados a razão social, nome fantasia, CNPJ e endereço.
	- O endereço é uma objeto de valor, que é armazenada junto com a empresa e não possui tabela própria.
	- Integramos a api ViaCep para que seja possível buscar parte do endereço através do CEP.
- **Ver as empresas cadastradas** e editar ou alternar seu status de ativação.
    - O status de ativação é um campo que indica se a empresa está ativa ou inativa. Isso é uma forma de "excluir" a empresa sem realmente removê-la do banco de dados, permitindo que as notas fiscais associadas a ela ainda sejam acessíveis.
- **Cadastrar notas fiscais**.
	- Ao cadastrar uma nota fiscal, pede-se:
      - Empresa emitente e a empresa destinatária (em ambos os casos, a empresa já deve estar cadastrada, pois aceitamos o seu ID neste campo);
      - Número da nota;
      - Série;
      - Chave de acesso;
      - Data de emissão;
      - Tipo da nota (que é armazenado em um ENUM, podendo ser NF-e, NF-s OU CT-e);
      - Valor total;
      - Descrição.
- **Ver as notas fiscais cadastradas** e editá-las.
    - Da mesma forma que fizemos nas empresas, não é possível remover uma nota, com o intuito de manter registros históricos. Será implementado um sistema de status para que seja possível diferenciar as diferentes etapas de processo de uma nota fiscal, como "pendente", "emitida", "cancelada", etc.

### Estrutura
O código fica contido no diretório "Backend", seguindo a seguinte estrutura:
- **Api  **: Contem os controllers e configuração das rotas HTTP;
- **Application**: Contém os servicos que conectam o domínio com a API;
- **Domain**: Contém a lógica de negócios da aplicação;
    - **Entities**: Contém as entidades do domínio, como Empresa e Nota Fiscal;
    - **Enums**: Contém os enums utilizados, como o tipo de nota fiscal e UF;
    - **Interfaces**: Contém as interfaces de repositórios e serviços;
    - **ValueObjects**: Contém os objetos de valor que não possuem tabelas próprias no banco, como o Endereço;
- **Infrastructure**: Contém a configuração do banco de dados, repositórios e integração com serviços externos;
    - **Data**: Contém o contexto do banco de dados, repositórios e as migrações;
    - **Dto**: Contém os Data Transfer Objects utilizados para comunicação entre a API e o domínio;
    - **Services**:Servicos externos, como a integração com a API do ViaCEP;

## Tecnologias Utilizadas
### Backend
- **.NET 8 (C#) + ASP.NET Core**: Framework principal de desenvolvimento;

- **Entity Framework Core**: acesso e manipulação do banco de dados;
    - Cabe menção ao uso do **In Memory Database**, que permite que os testes de Repositório rodem isolados do banco de dados real, utilizando uma simulação em memória;

- **SQLite**: Usado como banco local para desenvolvimento e testes. Escolhido por sua simplicidade, adequada ao projeto;

- **xUnit**: Framework de testes utilizado para cobertura de testes unitários;

- **XPlat Code Coverage**: Utilizadas para geração de relatórios de cobertura de testes automatizados;
  - Comando para rodar os testes e gerar o relatorio de cobertura: 
  ``` dotnet test collect:"XPlat Code Coverage"```;

- **HttpClient + JSON**: Utilizados para integração com a API do ViaCEP;

### Frontend
Cabe mencionar que o foco desta primeira parte do trabalho é o backend, mas o frontend foi implementado de maneira simplificada para facilitar os testes e a visualização do sistema.
- **React**: Framework principal para o frontend;

- **Axios**: Biblioteca para requisições HTTP e comunicação com o Backend;

- **Vite**: Ferramenta utilizada para build do projeto;

### Ferramentas
- **Visual Studio 2022**: IDE utilizada para desenvolvimento do projeto, com suporte a C# e .NET;
- **Github Actions**: CI/CD para automação de testes;
- **Codecov**: Ferramenta para visualização de cobertura de testes;

