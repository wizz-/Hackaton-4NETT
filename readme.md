# 🎯 Hackaton-4NETT

## 📌 Sobre o Projeto
Este repositório contém o **Hackaton-4NETT**, um projeto desenvolvido como parte do **Tech Challenge** para a quinta e última fase do curso de pós-graduação **Arquitetura de Sistemas .NET com Azure**.

O projeto consiste em um sistema de **Telemedicina** utilizando **Minimal API** com **.NET 8** no backend e **Blazor WebAssembly** no frontend, garantindo escalabilidade e alta disponibilidade.

## 🏗️ Estrutura do Projeto
A solução está organizada da seguinte forma:

```
📂 Hackaton-4NETT
 ├── 📂 1- Dominio           # Camada de domínio
 ├── 📂 2- Infra             # Infraestrutura e persistência
 ├── 📂 3- Aplicacao         # Regras de negócio
 ├── 📂 4- Apresentacao      # Camada de apresentação
 │   ├── 📂 Hackaton.Api  # Minimal API (.NET 8)
 │   ├── 📂 Hackaton.Web  # Blazor WebAssembly
 ├── 📂 5- Docker         # Arquivos Docker Compose
 │   ├── 📄 docker-compose.dev.yml   # Apenas SQL Server (para desenvolvimento)
 │   ├── 📄 docker-compose.full.yml  # API + SQL Server (ambiente completo)
```

## 🚀 Como Executar o Projeto

### 🛠️ Rodando Apenas o SQL Server (Modo Desenvolvimento)
Para rodar apenas o banco de dados em um container, permitindo o desenvolvimento local da API no Visual Studio, utilize o comando:
```sh
docker-compose -f 5- Docker/docker-compose.dev.yml up -d
```
Isso iniciará um container com o **SQL Server 2022**, e você poderá rodar a API manualmente no Visual Studio.

### 🔄 Rodando Todo o Projeto em Containers
Se quiser rodar **todo o projeto no Docker**, incluindo a API, utilize:
```sh
docker-compose -f 5- Docker/docker-compose.full.yml up -d
```
Esse comando iniciará tanto o **SQL Server 2022** quanto a **Minimal API** em containers.

## 🔧 Tecnologias Utilizadas
- ⚡ **.NET 8** (Minimal API + Blazor WebAssembly)
- 🛢 **SQL Server 2022**
- 🐳 **Docker & Docker Compose**