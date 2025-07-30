# CRM Arrighi - API Backend

Sistema de Gerenciamento de Relacionamento com Clientes - Backend API

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- SQL Server
- ASP.NET Core Web API

## Estrutura do Projeto

### Modelos (Models)

#### PessoaFisica
- **Id**: Identificador único
- **Nome**: Nome completo (obrigatório)
- **Email**: E-mail válido (obrigatório, único)
- **Codinome**: Apelido (opcional)
- **Sexo**: Sexo (obrigatório)
- **DataNascimento**: Data de nascimento (obrigatório)
- **EstadoCivil**: Estado civil (obrigatório)
- **Cpf**: CPF (obrigatório, único)
- **Rg**: RG (opcional)
- **Cnh**: CNH (opcional)
- **Telefone1**: Telefone principal (obrigatório)
- **Telefone2**: Telefone secundário (opcional)
- **Endereco**: Relacionamento com Endereco (obrigatório)
- **DataCadastro**: Data de cadastro (automático)
- **DataAtualizacao**: Data de atualização (automático)

#### PessoaJuridica
- **Id**: Identificador único
- **RazaoSocial**: Razão social (obrigatório)
- **NomeFantasia**: Nome fantasia (opcional)
- **Cnpj**: CNPJ (obrigatório, único)
- **ResponsavelTecnicoId**: ID da pessoa física responsável (obrigatório)
- **ResponsavelTecnico**: Relacionamento com PessoaFisica (obrigatório)
- **Email**: E-mail válido (obrigatório, único)
- **Telefone1**: Telefone principal (obrigatório)
- **Telefone2**: Telefone secundário (opcional)
- **Endereco**: Relacionamento com Endereco (obrigatório)
- **DataCadastro**: Data de cadastro (automático)
- **DataAtualizacao**: Data de atualização (automático)

#### Endereco
- **Id**: Identificador único
- **Cidade**: Cidade (obrigatório)
- **Bairro**: Bairro (obrigatório)
- **Logradouro**: Logradouro (obrigatório)
- **Cep**: CEP (obrigatório)
- **Numero**: Número (obrigatório)
- **Complemento**: Complemento (opcional)

## Endpoints da API

### Pessoa Física

#### GET /api/PessoaFisica
Retorna todas as pessoas físicas cadastradas.

#### GET /api/PessoaFisica/{id}
Retorna uma pessoa física específica pelo ID.

#### GET /api/PessoaFisica/responsaveis-tecnicos
Retorna uma lista simplificada de pessoas físicas disponíveis para serem responsáveis técnicos.

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "cpf": "123.456.789-00",
    "email": "joao@email.com"
  },
  {
    "id": 2,
    "nome": "Maria Santos",
    "cpf": "987.654.321-00",
    "email": "maria@email.com"
  }
]
```

#### POST /api/PessoaFisica
Cria uma nova pessoa física.

**Exemplo de requisição:**
```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "codinome": "João",
  "sexo": "Masculino",
  "dataNascimento": "1990-01-01",
  "estadoCivil": "Solteiro",
  "cpf": "123.456.789-00",
  "rg": "12.345.678-9",
  "cnh": "12345678901",
  "telefone1": "(11) 99999-9999",
  "telefone2": "(11) 88888-8888",
  "endereco": {
    "cidade": "São Paulo",
    "bairro": "Centro",
    "logradouro": "Rua das Flores",
    "cep": "01234-567",
    "numero": "123",
    "complemento": "Apto 45"
  }
}
```

#### PUT /api/PessoaFisica/{id}
Atualiza uma pessoa física existente.

#### DELETE /api/PessoaFisica/{id}
Remove uma pessoa física.

**Observação:** Não é possível excluir uma pessoa física se ela for responsável técnico de alguma pessoa jurídica.

### Pessoa Jurídica

#### GET /api/PessoaJuridica
Retorna todas as pessoas jurídicas cadastradas.

#### GET /api/PessoaJuridica/{id}
Retorna uma pessoa jurídica específica pelo ID.

#### POST /api/PessoaJuridica
Cria uma nova pessoa jurídica.

**Exemplo de requisição:**
```json
{
  "razaoSocial": "Empresa LTDA",
  "nomeFantasia": "Empresa",
  "cnpj": "12.345.678/0001-90",
  "responsavelTecnicoId": 1,
  "email": "contato@empresa.com",
  "telefone1": "(11) 99999-9999",
  "telefone2": "(11) 88888-8888",
  "endereco": {
    "cidade": "São Paulo",
    "bairro": "Centro",
    "logradouro": "Rua das Flores",
    "cep": "01234-567",
    "numero": "123",
    "complemento": "Sala 100"
  }
}
```

**Observação:** O `responsavelTecnicoId` deve ser o ID de uma pessoa física já cadastrada no sistema.

#### PUT /api/PessoaJuridica/{id}
Atualiza uma pessoa jurídica existente.

#### DELETE /api/PessoaJuridica/{id}
Remove uma pessoa jurídica.

### Endereço

#### GET /api/Endereco
Retorna todos os endereços cadastrados.

#### GET /api/Endereco/{id}
Retorna um endereço específico pelo ID.

#### POST /api/Endereco
Cria um novo endereço.

#### PUT /api/Endereco/{id}
Atualiza um endereço existente.

#### DELETE /api/Endereco/{id}
Remove um endereço.

## Configuração do Banco de Dados

A string de conexão está configurada no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CrmArrighi;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Migrações

Para criar o banco de dados, execute os seguintes comandos:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Executando o Projeto

```bash
dotnet run
```

A API estará disponível em: `https://localhost:7001` ou `http://localhost:5001`

## CORS

A API está configurada para aceitar requisições de qualquer origem (CORS), permitindo que o frontend se conecte sem problemas.

## Validações

- CPF e CNPJ são únicos no sistema
- E-mails são únicos no sistema
- Validação de formato de e-mail
- Campos obrigatórios são validados
- Relacionamentos entre entidades são mantidos
- **Responsável Técnico**: Deve ser uma pessoa física já cadastrada no sistema
- **Exclusão de Pessoa Física**: Não é permitida se ela for responsável técnico de alguma pessoa jurídica

## Funcionalidades Implementadas

### Pessoa Física
- ✅ Criar
- ✅ Editar
- ✅ Excluir
- ✅ Listar todas
- ✅ Buscar por ID
- ✅ Listar responsáveis técnicos disponíveis

### Pessoa Jurídica
- ✅ Criar
- ✅ Editar
- ✅ Excluir
- ✅ Listar todas
- ✅ Buscar por ID
- ✅ Relacionamento obrigatório com Pessoa Física (Responsável Técnico)

### Endereço
- ✅ Criar
- ✅ Editar
- ✅ Excluir
- ✅ Listar todos
- ✅ Buscar por ID 