# 🎯 Hackaton-4NETT
---
## 📌 Sobre o Projeto
Este repositório contém o **Hackaton-4NETT**, um projeto desenvolvido como parte do **Tech Challenge** para a quinta e última fase do curso de pós-graduação **Arquitetura de Sistemas .NET com Azure**.

O projeto consiste em um sistema de **Telemedicina** utilizando **Minimal API** com **.NET 8** no backend e **Blazor WebAssembly** no frontend, garantindo escalabilidade e alta disponibilidade.
---
## 🏗️ Estrutura do Projeto
A solução está organizada da seguinte forma:
---
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
---
## 🚀 Como Executar o Projeto
---
### 🛠️ Rodando Apenas o SQL Server (Modo Desenvolvimento)
Para rodar apenas o banco de dados em um container, permitindo o desenvolvimento local da API no Visual Studio, utilize o comando:
```sh
docker-compose -f 5- Docker/docker-compose.dev.yml up -d
```
Isso iniciará um container com o **SQL Server 2022**, e você poderá rodar a API manualmente no Visual Studio.
---
### 🔄 Rodando Todo o Projeto em Containers
Se quiser rodar **todo o projeto no Docker**, incluindo a API, utilize:
```sh
docker-compose -f 5- Docker/docker-compose.full.yml up -d
```
Esse comando iniciará tanto o **SQL Server 2022** quanto a **Minimal API** em containers.
---
## 🔧 Tecnologias Utilizadas
- ⚡ **.NET 8** (Minimal API + Blazor WebAssembly)
- 🛢 **SQL Server 2022**
- 🐳 **Docker & Docker Compose**

---

## 📚 Tópicos

  - [Problema](#problema) 
  - [Funcionalidades](#Funcionalidades)
  - [Descrição do Sistema](#descrição-do-sistema)
  - [Arquitetura](#arquitetura)
    - [Visão Geral](#visão-geral)
    -
---

## 📄Problema

**Health&Med** é uma proposta de startup voltada para inovação no setor da saúde. O sistema foi idealizado para oferecer uma plataforma robusta, segura e escalável para o gerenciamento de agendamentos médicos, conectando pacientes e profissionais de forma prática e moderna.

Com um recente aporte financeiro, o projeto iniciou o desenvolvimento de seu sistema proprietário, com foco em:

- ✅ Qualidade no atendimento  
- 🔒 Segurança dos dados sensíveis dos pacientes  
- 💸 Redução de custos operacionais  

A proposta é aliar tecnologia de ponta com uma experiência digital eficiente, promovendo acessibilidade, organização e agilidade na área da saúde

---

## 🛠️Funcionalidade

Principais funcionalidades:
- 📅 **Agendamento rápido de consultas**  
- 🗂️ **Gestão de agenda personalizada** para profissionais de saúde  
- 🔍 **Busca inteligente** por médicos (por especialidade, localização, disponibilidade)  
- 🔐 **Garantia de segurança e privacidade** dos dados  
- 🌍 **Escalabilidade** para clínicas, consultórios e hospitais  

---

## 📜 Descrição do Sistema

O **Health&Med** é um sistema de gerenciamento de consultas médicas, projetado para modernizar o acesso aos serviços de saúde. Seu objetivo principal é oferecer uma solução eficiente e intuitiva que permita:

- 🧑‍⚕️ **Pacientes localizarem médicos** com facilidade e agendarem atendimentos sem burocracia  
- 🩺 **Médicos organizarem suas agendas** de forma prática e centralizada  

A plataforma atende às necessidades específicas do setor da saúde, com uma estrutura escalável, segura e pronta para integração com funcionalidades futuras, como telemedicina e notificações automatizadas.

---
## Arquitetura

---

## Visão geral

A tela de login a nível do médico deve seguir o padrão abaixo exibido.

![image](https://github.com/user-attachments/assets/89a3e979-bf48-44cd-8012-a97dbe1156ec)

A tela nível paciente deve seguir o padrão abaixo.
![image](https://github.com/user-attachments/assets/9cbf37a2-31fc-447a-a316-7eea8897c318)




