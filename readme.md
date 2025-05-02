WorkerService - Monitoramento de Serviços
WorkerService é um serviço backend dedicado a monitorar a disponibilidade de serviços e links cadastrados em um banco de dados PostgreSQL. Ele é executado em segundo plano, periodicamente verificando o status dos serviços e armazenando os resultados no banco de dados.

🚀 Funcionalidades
Monitoramento Periódico: O WorkerService verifica periodicamente o status dos serviços cadastrados.

Armazenamento de Status: O status dos serviços (ativo ou inativo) é armazenado no banco de dados PostgreSQL.

Execução em Segundo Plano: O serviço é executado em background e pode ser facilmente configurado para rodar de forma contínua.

Facilidade de Integração: Pode ser integrado com qualquer sistema que precise monitorar serviços em tempo real.

⚙️ Como Rodar o WorkerService
Requisitos
.NET 8 ou superior

PostgreSQL

1. Clone o repositório
Primeiro, clone o repositório do seu projeto:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/app.monitor.git
cd app.monitor
2. Configure a Conexão com o Banco de Dados
Abra o arquivo appsettings.json e configure a string de conexão com o banco PostgreSQL:

json
Copiar
Editar
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=monitor_db;Username=seu_usuario;Password=sua_senha"
  }
}
3. Crie a Base de Dados
Caso o banco PostgreSQL ainda não tenha sido configurado, use o seguinte comando para criar o banco de dados (caso esteja usando Docker):

bash
Copiar
Editar
docker run --name monitor-db -e POSTGRES_PASSWORD=sua_senha -p 5432:5432 -d postgres
Depois, execute as migrações para criar as tabelas necessárias:

bash
Copiar
Editar
dotnet ef database update
4. Rode o WorkerService
Para rodar o serviço, execute o comando abaixo no diretório do seu projeto:

bash
Copiar
Editar
dotnet run
O WorkerService agora está monitorando os serviços cadastrados no banco de dados, verificando o status deles a cada intervalo configurado.

🛠️ Como Funciona o Monitoramento
O WorkerService opera da seguinte forma:

Leitura de Serviços: O serviço lê a lista de links ou serviços cadastrados no banco de dados.

Verificação de Status: Periodicamente, o serviço verifica a disponibilidade dos links.

Armazenamento de Status: O status de cada serviço (ativo ou inativo) é atualizado no banco de dados.

O intervalo entre as verificações pode ser ajustado nas configurações da aplicação.