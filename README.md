<h1 align="center">🧠 Projetos em C#</h1>

<p align="center">
  <img src="https://img.shields.io/github/repo-size/AlanOliverDev/Projetos-CSharp?color=blue" alt="Repo Size">
  <img src="https://img.shields.io/github/languages/top/AlanOliverDev/Projetos-CSharp" alt="Top Language">
  <img src="https://img.shields.io/github/last-commit/AlanOliverDev/Projetos-CSharp" alt="Last Commit">
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/AlanOliverDev/Projetos-CSharp/main/banner.png" alt="Banner do Projeto" width="100%">
</p>

---

## 📁 Projetos incluídos

Cada pasta representa um projeto independente:

- 🚦 [Algoritmo de Controle de Caixa](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Algoritmo%20de%20Controle%20de%20Caixa)
- 🚓 [Algoritmo de Radar PRF](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Algoritmo%20de%20Radar%20PRF)
- 🔐 [AutenticacaoUsuario](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/AutenticacaoUsuario)
- 🧮 [Calculo Aritmetico de Notas](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Calculo%20Aritmetico%20de%20Notas)
- 🚗 [Carro Autonomo Tesla Model X](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Carro%20Autonomo%20Tesla%20Model%20X)
- 📋 [Catalago de Tarefas](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Catalago%20de%20Tarefas)
- 🎬 [Catalogo Netflix](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Catalogo%20Netflix)
- 🚘 [Objeto_Carro_interage_Motorista](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Objeto_Carro_interage_Motorista)
- 👤 [Um Novo Usuario](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/Um%20Novo%20Usuario)
- 🏫 [ProjetoSGA_IntegradoMySql](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/ProjetoSGA_IntegradoMySql/SGA_SistemaGereciamentoAluno)
- 🎧 [ProjetoSPOTIFEI_IntegradoMySql](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/ProjetoSPOTIFEI_IntegradoMySql%2BDiagrama/Project_Final_Integracao_SPOTYFEI)
- 🐾 [ProjetoZoo_IntegradoMySql](https://github.com/AlanOliverDev/Projetos-CSharp/tree/main/Projetos%20C%23/ProjetoZoo_IntegradoMySql/ZooGerenciamento)

---

## 🛠️ Tecnologias Utilizadas

- C# (.NET Core / .NET 6)
- Visual Studio / VS Code
- Programação orientada a objetos
- Manipulação de arquivos e coleções
- ASP.NET Core (em projetos futuros)

---

## 📦 Pré-requisitos

Antes de executar qualquer projeto, certifique-se de ter os seguintes itens instalados em seu computador:

- ✅ [.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download): necessário para compilar e rodar os projetos em C#
- ✅ [Git](https://git-scm.com/downloads): usado para baixar os arquivos do repositório (opcional)
- ✅ [Visual Studio Code](https://code.visualstudio.com) ou outro editor de código de sua preferência

---

## 🚀 Como Executar

Você pode baixar e executar os projetos de duas formas:

### 🔁 Opção 1: Usando Git (recomendado)

1. Abra o terminal (Git Bash, PowerShell ou CMD)
2. Clone o repositório com o comando:

   ```bash
   git clone https://github.com/AlanOliverDev/Projetos-CSharp.git
   ```

Acesse a pasta do projeto desejado:

bash
cd "Projetos-CSharp/Projetos C#/NomeDoProjeto"
Execute o projeto com:

bash
dotnet run

📥 Opção 2: Baixar manualmente pelo GitHub
Acesse o repositório: github.com/AlanOliverDev/Projetos-CSharp

Clique no botão verde "Code" e depois em "Download ZIP"

Extraia o arquivo ZIP em seu computador

Abra a pasta do projeto desejado com o Visual Studio Code

No terminal do VS Code (Git Bash, PowerShell ou CMD), execute:

dotnet run

🐬 Projetos com integração MySQL
Alguns projetos utilizam banco de dados MySQL para armazenar e manipular dados. Para executá-los corretamente, siga os passos abaixo:

✅ Pré-requisitos adicionais
✔️ MySQL Server

✔️ MySQL Workbench (opcional, para gerenciar o banco graficamente)

✔️ .NET Connector para MySQL (MySql.Data)

⚙️ Como configurar e executar
Instale o MySQL Server e certifique-se de que ele está em execução.

Abra o MySQL Workbench ou outro cliente MySQL.

Execute o script SQL que está na pasta Script do projeto. Ele criará o banco de dados e todas as tabelas necessárias.

Exemplo: abra o arquivo script.sql, cole no editor do MySQL Workbench e clique em "Run".

Verifique a string de conexão no arquivo Connection.cs ou equivalente no projeto:

csharp
string connectionString = "server=localhost;user=root;password=senha;database=nome_do_banco";
Execute o projeto normalmente no terminal:

bash
dotnet run
📌 Observações
O script SQL já inclui a criação do banco de dados — não é necessário criá-lo manualmente.

Certifique-se de que o MySQL está rodando antes de executar o projeto.

Se houver erros de conexão, revise a string de conexão e as permissões do usuário MySQL.

✨ Autor
Alan Oliver 🔗 GitHub : https://github.com/AlanOliverDev
