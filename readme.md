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
## 🔧 Tecnologias Utilizadas
- ⚡ **.NET 8** (Minimal API + Blazor WebAssembly)
- 🛢 **SQL Server 2022**
- 🐳 **Docker & Docker Compose**
- 📊 **Grafana**
- ⏱️ **Prometheus**

---
## 🗂️ Modelagem de dados

Para facilitar o entendimento dos armazenamentos dos dados na plataforma Health&Med, abaixo temos nosso diagramas de tabelas explicativa com a definição de cada entidade. Descrevendo como estruturamos e armazenamos os dados no nosso sistema.

![Diagrama](https://github.com/user-attachments/assets/e595453b-4dfa-4a33-a81d-4162633c513f)

| Tabela            | Descrição                                                                                                                                                     |
|-------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Usuário           | Tabela que contém os dados referentes aos usuários (médicos e pacientes cadastrados na plataforma).                                                           |
| Paciente          | Tabela que contém os dados referentes aos pacientes, contendo os dados dos mesmos.                                                                            |
| Médico            | Tabela que contém os dados referentes aos médicos, contendo os dados dos mesmos.                                                                              |
| Consulta          | Tabela que contém os dados referentes às consultas, incluindo dia, horário de início e fim, além de onde é armazenado o status caso a consulta seja cancelada. |
| Especialidade     | Tabela onde ficam registradas as especialidades de cada médico.                                                                                               |
| Horário Disponível| Tabela onde é possível validar os horários disponíveis de cada médico, os quais ficam armazenados para controle e gerenciamento.                              |


---
## 🚀 Como Executar o Projeto

#### 🛠️ Rodando Apenas o SQL Server (Modo Desenvolvimento)
Para rodar apenas o banco de dados em um container, permitindo o desenvolvimento local da API no Visual Studio, utilize o comando:
```sh
docker-compose -f 5- Docker/docker-compose.dev.yml up -d
```
Isso iniciará um container com o **SQL Server 2022**, e você poderá rodar a API manualmente no Visual Studio.

Após rodar o projeto a iteração pode ser feita via Swagger pelo link abaixo.:

https://localhost:7272/swagger/index.html

![Captura de tela_8-5-2025_21319_localhost](https://github.com/user-attachments/assets/6664d154-5f1e-420d-8a98-9775239f9ac3)



---
### 🔄 Rodando Todo o Projeto em Containers
Se quiser rodar **todo o projeto no Docker**, incluindo a API, utilize:
```sh
docker-compose -f 5- Docker/docker-compose.full.yml up -d
```
Esse comando iniciará tanto o **SQL Server 2022** quanto a **Minimal API** em containers.

---

## 🔄 CI/CD Pipeline

A API desenvolvida para a Health&Med conta com uma pipeline CI/CD a qual criamos para que os processos de automação
sejam mais otimizados, facilitando a integração do código feito pela equipe CI e garantir que a entrega eficiente ao time de (CD).
Durante esse processo incluimos a execução de testes unitários.

![image](https://github.com/user-attachments/assets/cbe4445c-8fd3-44dd-9d48-14975f4d4a5a)

Com maior detalhes é possivel visualizar os testes unitários e a publicação das imagens no container:

![image](https://github.com/user-attachments/assets/a2da70fd-d395-407d-93c0-f72c62a8860e)



---

## 📦 Imagens publicadas no Container

Além disso nossa pipe de CI/CD realiza a publicação da imagem do Container via Docker Hub, foi criado uma .bat onde as imagens do Container após a exexucução são publicadas automaticamente.

Além disso é realizado o monitoramento dos dados, onde o Monitoramento foi realizado de duas formas.


- **Prometheus, onde é gerado as métricas da aplicação:**

http://localhost:31197/targets

![image](https://github.com/user-attachments/assets/392f7d60-5fcd-4e64-803e-0ddadece94be)

- **Grafana , gerando os gráfico e organizando as métricas enviadas pelo prometheus**

http://localhost:31824/

![image](https://github.com/user-attachments/assets/14738250-c85e-4dba-9ae1-8fae2a171c6a)


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









