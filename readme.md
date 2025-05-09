# 🎯 Hackaton-5NETT
---
## 📌 Sobre o Projeto

Este repositório contém o **Hackaton-5NETT**, um projeto desenvolvido como parte do **Tech Challenge** para a quinta e última fase do curso de pós-graduação **Arquitetura de Sistemas .NET com Azure**.

O projeto consiste em um sistema de **Telemedicina** utilizando **Minimal API** com **.NET 8** no backend e **Blazor WebAssembly** no frontend, garantindo escalabilidade e alta disponibilidade.

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

---
## 🏗️ Estrutura do Projeto
A solução está organizada da seguinte forma:
---
```
📂 Hackaton-5NETT
├── 📁 1- Dominio
│   └── 📄 Domain.csproj               # Entidades, interfaces de domínio, regras de negócio 
│
├── 📁 2- Infra
│   ├── 📁 Data
│   │   └── 📄 Infra.Data.csproj       # Repositórios, EF Core
│   ├── 📁 DatabaseInitializers       
│   │   └── 📄 Infra.DatabaseInitializers.csproj # Cria o banco de dados, caso ele não exista
│   └── 📁 IoC
│       └── 📄 Infra.IoC.csproj        # Injeção de dependência
│
├── 📁 3- Aplicacao
│   └── 📄 Application.csproj          # Intermediação entre camadas de apresentação e backend da aplicação
│
├── 📁 4- Apresentacao
│   ├── 📁 Hackaton.Api
│   │   └── 📄 Hackaton.Api.csproj     # Minimal API (.NET 8)
│   └── 📁 Hackaton.Web
│       └── 📄 Hackaton.Web.csproj     # Blazor WebAssembly (frontend)
│
├── 📁 5 - Teste
│   └── 📁 Hackaton.UnitTest
│       └── 📄 Hackaton.UnitTest.csproj  # Testes unitários
│
├── 📁 6 - Docker
│   ├── 📄 Dockerfile.Api
│   └── 📄 Dockerfile.Web              # Dockerfiles das aplicações
│
├── 📁 7 - k8s
│   ├── 📄 deploy.yaml
│   └── 📄 hpa.yaml                    # Arquivos de orquestração Kubernetes
│
├── 📁 8 - CI_CD
│   └── 📄 ci_cd.yml                   # Workflow GitHub Actions
│
├── 📁 9 - Grafana
│   └── 📄 Dashboard Model Grafana.json # Modelo de dashboard para observabilidade
│
├── 📄 deploy.bat
├── 📄 deploy.ps1
├── 📄 limpar-bin-obj.bat
├── 📄 readme.md
├── 📄 .gitignore
└── 📄 .gitattributes
```
---
## 🔧 Tecnologias Utilizadas
- ⚡ **.NET 8** (Minimal API + Blazor WebAssembly)
- 🛢 **SQL Server 2022**
- 🐳 **Docker e Kubernetes**
- 📊 **Grafana**
- ⏱️ **Prometheus**

---
## 🗂️ Modelagem de dados

Para proporcionar uma melhor compreensão sobre a forma como os dados são estruturados e armazenados na plataforma Health&Med, apresentamos a seguir o diagrama de entidades acompanhado de uma tabela explicativa. Nela, cada entidade é descrita com sua respectiva função no sistema, evidenciando a organização do modelo de dados adotado.

![Diagrama](https://github.com/user-attachments/assets/e595453b-4dfa-4a33-a81d-4162633c513f)

| Tabela            | Descrição                                                                                                                                                     |
|-------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Usuário           | Tabela que contém os dados referentes ao login usuários (médicos e pacientes cadastrados na plataforma).                                                           |
| Paciente          | Tabela que contém os dados referentes aos pacientes.                                                                        |
| Médico            | Tabela que contém os dados referentes aos médicos.                                                                              |
| Consulta          | Tabela que contém os dados referentes às consultas, incluindo dia, horário de início e fim, além de onde é armazenado o status caso a consulta seja cancelada. |
| Especialidade     | Tabela onde ficam registradas as especialidades da medicina.                                                                                               |
| Horário Disponível| Tabela que registra as informações referentes ao tempo de consulta do médico, ao dia da semana e ao período em que se encontra disponível.                              |


---
## 🚀 Como Executar o Projeto

Para realizar o deploy completo do projeto — incluindo a publicação, criação dos containers e aplicação no Kubernetes — utilize o seguinte comando:

```sh
deploy.bat
```
Esse script executa as seguintes etapas automaticamente:

- Concede permissões e executa o arquivo PowerShell deploy.ps1;
- Publica os projetos Web e API;
- Realiza o build dos Dockerfiles da Web e da API;
- Aplica as configurações no Kubernetes;
- Aplica o HPA (Horizontal Pod Autoscaler);
- Aguarda o container do Blazor ficar pronto para uso.

Após rodar o projeto a iteração pode ser feita via Swagger na Api pelo link abaixo.:

http://localhost:30880/swagger/index.html

![Captura de tela_8-5-2025_21319_localhost](https://github.com/user-attachments/assets/6664d154-5f1e-420d-8a98-9775239f9ac3)

E o front-end da nossa aplicação pelo link:

http://localhost:30881/

![image](https://github.com/user-attachments/assets/c0053758-df18-4704-a0da-6a397a3f3dd4)

---

## 🔄 CI/CD Pipeline

A API desenvolvida conta com a pipeline CI/CD, facilitando a integração do código e garantindo a qualidade das entregas executando os testes unitários.

![image](https://github.com/user-attachments/assets/cbe4445c-8fd3-44dd-9d48-14975f4d4a5a)

![image](https://github.com/user-attachments/assets/a2da70fd-d395-407d-93c0-f72c62a8860e)



---

## 📈 Monitoramento

Para o monitoramento da aplicação, foram utilizadas as seguintes ferramentas:

- **Prometheus, onde é gerado as métricas da aplicação:**

http://localhost:31197/targets

![image](https://github.com/user-attachments/assets/392f7d60-5fcd-4e64-803e-0ddadece94be)

- **Grafana , gerando os gráfico e organizando as métricas enviadas pelo prometheus**

http://localhost:31824/

![Grafana](https://github.com/user-attachments/assets/abca9f48-bbfb-406d-87a1-704fefdb9996)



---

## 📜 Descrição do Sistema

O **Health&Med** é um sistema de gerenciamento de consultas médicas, projetado para modernizar o acesso aos serviços de saúde. Seu objetivo principal é oferecer uma solução eficiente e intuitiva que permita:

- 🧑‍⚕️ **Pacientes localizarem médicos** com facilidade e agendarem atendimentos sem burocracia  
- 🩺 **Médicos organizarem suas agendas** de forma prática e centralizada  

A plataforma atende às necessidades específicas do setor da saúde, com uma estrutura escalável, segura e pronta para integração com funcionalidades futuras, como telemedicina e notificações automatizadas.

---
## 🏛️ Arquitetura

Abaixo exemplificamos como a plataforma vai ser utilizada:

### Visão geral

#### - Tela de Login 

Para dar início à utilização da plataforma Health&Med API, desenvolvemos uma tela de login intuitiva e funcional.
Nessa etapa inicial, tanto pacientes quanto médicos interessados em aderir à plataforma deverão realizar um cadastro prévio, fornecendo as informações necessárias para criar um perfil no sistema.

Após o cadastro, os usuários poderão acessar normalmente suas respectivas áreas — seja como paciente ou como profissional da saúde — por meio da opção de login. Já aqueles que já possuem um registro ativo no sistema poderão simplesmente inserir suas credenciais para entrar diretamente na plataforma e usufruir de todos os recursos disponíveis.

A tela de login a nível do médico deve seguir o padrão abaixo exibido.

![image](https://github.com/user-attachments/assets/89a3e979-bf48-44cd-8012-a97dbe1156ec)

A tela nível paciente deve seguir o padrão abaixo.
![image](https://github.com/user-attachments/assets/9cbf37a2-31fc-447a-a316-7eea8897c318)

#### - Tela de Agendamento Paciente

Na tela de agendamento da plataforma Health&Med API, os usuários contam com uma interface prática e acessível, pensada para facilitar o gerenciamento de suas consultas médicas.
Através dessa funcionalidade, os pacientes tem as opções de agendamento de forma rápida e prática.

Além disso, para maior comodidade e controle, o sistema também permite o cancelamento de agendamentos já realizados, oferecendo flexibilidade caso surjam imprevistos ou mudanças na agenda.

A tela de agendamento a nível do paciente deve seguir o padrão abaixo exibido

![image](https://github.com/user-attachments/assets/b03fd1e1-d7bb-46a9-8978-9e18e4e5df06)


#### - Cadastro/Edição de horários Disponíveis

Na tela de cadastro e edição dos horários da plataforma Health&Med, nessa tela os médicos conseguem otimizar e organizar sua agenda de atendimentos.

Com essa funcionalidade o Médico tem a liberadade de definir seus horários além de editar os horários já definidos, além disso o sistema permite que sejam feitas edições e remoções de horários a qual foram previamente cadastrados. 

![image](https://github.com/user-attachments/assets/dd85963a-8486-47ad-87af-5096937702a8)









