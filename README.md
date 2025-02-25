# 📘 Documentação do Projeto Template

## 📖 Visão Geral
Este projeto é um **template** para desenvolvimento de funcionalidades em **.NET Core**, permitindo a expansão e reutilização do código para diversas finalidades. Ele inclui uma estrutura modular com separação entre API, serviços e camadas de domínio, visando uma arquitetura escalável e organizada.

## 🏗 Arquitetura e Tecnologias Utilizadas
O projeto adota uma arquitetura modular baseada em **camadas** e utiliza as seguintes tecnologias:

- **ASP.NET Core 8.0** → Framework principal para desenvolvimento da API
- **Entity Framework Core** → ORM para interação com banco de dados relacional
- **SQL Server** → Banco de dados relacional
- **Kafka** → Plataforma de mensageria para comunicação assíncrona
- **Docker & Docker Compose** → Contênirização das aplicações
- **xUnit** → Framework de testes unitários

## 📁 Estrutura do Projeto

```bash
📂 Template
├── 📂 src
│   ├── 📂 01 - API
│   │   ├── 📂 API.Template
│   ├── 📂 02 - Serviços
│   ├── 📂 03 - Core
│   │   ├── 📂 Template.Application
│   │   ├── 📂 Template.Common.Core
│   │   ├── 📂 Template.Common.Domain
│   │   ├── 📂 Template.Domain
│   │   ├── 📂 Template.Infrastructure
├── 📂 Tests
│   ├── 📂 IntegrationTests
│   │   ├── 📂 API.Template.Tests
│   ├── 📂 UnitTests
│   │   ├── 📂 Template.Common.Domain.Tests
├── 📂 docker-compose
│   ├── 📄 .dockerignore
│   ├── 📄 docker-compose.yml
│   ├── 📄 launchSettings.json
```

## 🚀 Como Executar o Projeto

1. Certifique-se de ter **Docker** e **Docker Compose** instalados.
2. Clone o repositório e acesse a pasta raiz do projeto.
3. Execute os seguintes comandos para iniciar os containers:

```bash
docker-compose down
docker-compose up -d --build
```

4. Aplique as migrations no banco de dados:

```bash
dotnet ef database update --context AppDbContext
```

5. Execute a API:

```bash
dotnet run --project src/01 - API/API.Template
```

## 📡 Configuração dos Serviços

### **Banco de Dados (SQL Server)**
- **Host:** `localhost`
- **Porta:** `1433`
- **Usuário:** `sa`
- **Senha:** `YourStrong!Pass`

### **Kafka**
- **Host:** `localhost`
- **Porta:** `9092`
- **UI:** [http://localhost:8080](http://localhost:8080) (Kafka UI)

## ✅ Testes
Os testes estão divididos em **unitários** e **de integração**:

### **Testes Unitários**

```bash
dotnet test --filter Category=UnitTests
```

### **Testes de Integração**

```bash
dotnet test --filter Category=IntegrationTests
```

## 📋 Comandos Importantes

```bash
# Criar uma migration
dotnet ef migrations add InitialCreate --context AppDbContext

# Aplicar migrations
dotnet ef database update --context AppDbContext
```

## 🧑‍💻 **Autores**

- **Guilherme Figueiras Maurila**

## 📫 Como me encontrar
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCjy19AugQHIhyE0Nv558jcQ)
[![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila)](https://www.linkedin.com/in/guilherme-maurila)
[![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)


