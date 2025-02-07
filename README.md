# 📘 Documentação Geral - Template

## 📖 Visão Geral
... **...** ....

## 🏗 Arquitetura e Tecnologias Utilizadas
O projeto adota uma arquitetura baseada em **Microservices** e **CQRS (Command Query Responsibility Segregation)**, utilizando:

- **ASP.NET Core 8.0** → Framework para desenvolvimento das APIs
- **Entity Framework Core** → ORM para interação com banco de dados relacional
- **YARP (Reverse Proxy)** → Gerenciamento de roteamento de APIs
- **SQL Server** → Banco de dados relacional
- **MongoDB** → Banco de dados NoSQL
- **Redis** → Cache distribuído para otimização de performance
- **RabbitMQ / Kafka** → Mensageria para comunicação assíncrona
- **Docker & Docker Compose** → Contêinerização das aplicações
- **Swagger/OpenAPI** → Documentação interativa da API
- **JWT (JSON Web Token)** → Autenticação e autorização

## 📁 Estrutura do Projeto

```bash
📂 poc.micro-saas.netcore8
├── 📂 Documento
│   ├── 📄 README.md
├── 📂 src
│   ├── ...
├── 📂 docker-compose
│   ├── 📄 .dockerignore
│   ├── 📄 docker-compose.yml
│   ├── 📄 docker-compose.override.yml
│   ├── 📄 launchSettings.json
```

## 📌 Descrição das APIs

### 1️⃣ **API....**
- ... **interface única** ....

## 🚀 Execução do Projeto
O projeto pode ser inicializado utilizando **Docker Compose**:

```bash
docker-compose down
docker-compose up -d --build
Update-Database -Context MainContext
```

### 📡 Serviços Configurados
- **SQL Server** (1433)
- **Redis** (6379)
- **MongoDB** (27017)
- **RabbitMQ** (5672)
- **Kafka** (9092)
- **Kafka UI** (8080)

## 🔍 Testes e Qualidade
### ✅ **Testes Unitários**
Os testes unitários são implementados utilizando **xUnit**:

```bash
dotnet test
```

### 🔄 **Testes de Integração**
Os testes de integração utilizam **TestContainers** e **Postman/Newman** para validação:

```bash
dotnet test --filter Category=IntegrationTests
```

## 📚 **Banco de Dados**
### **SQL Server**
- **Host:** `localhost`
- **Usuário:** `sa`
- **Senha:** `Password!123`

### **MongoDB**
- **Host:** `localhost`
- **Database:** `clinics_db`

## 📦 **Mensageria e Streaming**
### **RabbitMQ**
- **Acesso:** [http://localhost:15672](http://localhost:15672)
- **Usuário:** guest / **Senha:** guest

### **Kafka**
- **Acesso:** [http://localhost:9100](http://localhost:9100)

## 📋 **Comandos Importantes**

```bash
Add-Migration InitialCreate -Context AppDbContext
Update-Database -Context AppDbContext
```

## 🧑‍💻 **Autores**

- **Guilherme Figueiras Maurila**


## 📫 Como me encontrar
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCjy19AugQHIhyE0Nv558jcQ)
[![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila)](https://www.linkedin.com/in/guilherme-maurila)
[![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)


