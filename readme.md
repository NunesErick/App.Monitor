# 🚀 WorkerService - Monitoramento de Serviços

![.NET](https://img.shields.io/badge/.NET-9-%23512bd4)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-14-%23316192)

O **WorkerService** é um serviço backend robusto e eficiente projetado para monitorar a disponibilidade de serviços e links em tempo real, com armazenamento de dados em PostgreSQL.

## ✨ Funcionalidades Principais

✔ **Monitoramento Periódico** - Verificação automática e regular do status dos serviços cadastrados  
✔ **Armazenamento Inteligente** - Registro do status (ativo/inativo) em banco de dados PostgreSQL  
✔ **Background Processing** - Execução contínua em segundo plano como serviço  
✔ **Fácil Integração** - Pronto para conectar-se com sistemas que necessitam de monitoramento em tempo real  
✔ **Configurável** - Intervalos de verificação personalizáveis via configuração  

## 🛠️ Pré-requisitos

- [.NET 9+](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (14 ou superior)
- Docker (opcional para execução via container)

## ⚡ Configuração Rápida

### 1. Clone o repositório

git clone https://github.com/seu-usuario/app.monitor.git
cd app.monitor

### 2. Configure o Banco de Dados
Edite o arquivo appsettings.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=monitor_db;Username=seu_usuario;Password=sua_senha"
  }
}
### 3. Inicialize o PostgreSQL (via Docker)
docker run --name monitor-db -e POSTGRES_PASSWORD=sua_senha -p 5432:5432 -d postgres:latest

#### 3.1 Crie as tabelas (Em breve será substituído por EF)
CREATE TABLE IF NOT EXISTS public.aplicacoes
(
    id integer NOT NULL,
    nome character varying(255) COLLATE pg_catalog."default" NOT NULL,
    descricao text COLLATE pg_catalog."default",
    link_interacao character varying(255) COLLATE pg_catalog."default" NOT NULL,
    data_criacao timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT aplicacoes_pkey PRIMARY KEY (id)
)

CREATE TABLE IF NOT EXISTS public.log_apps
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    data_medicao timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    id_aplicacao integer NOT NULL,
    status integer NOT NULL,
    CONSTRAINT log_apps_pkey PRIMARY KEY (id)
)
### 4. Execute o Serviço
dotnet run

🔍 Arquitetura do Sistema
O WorkerService segue um fluxo otimizado para monitoramento:
```mermaid
graph TD
    A[Início do Worker] --> B[Consultar serviços no BD]
    B --> C[Para cada serviço]
    C --> D[Verificar disponibilidade]
    D --> E[Registrar status no BD]
    E --> F[Aguardar intervalo configurado]
    F --> B
