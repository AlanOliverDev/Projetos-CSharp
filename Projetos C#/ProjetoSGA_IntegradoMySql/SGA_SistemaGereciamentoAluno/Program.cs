using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            new Connection().AbrirConexao().Close();
            Console.WriteLine("Banco de Dados conectado com Sucesso!!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Bah não deu certo, erro na conexão!!");
            Console.WriteLine($"Erro: {e.Message}");
            return;
        }

        int escolha;
        do
        {
            Console.WriteLine("\n--- Menu Principal ---");
            Console.WriteLine("1 - Login como Admin");
            Console.WriteLine("2 - Login como Aluno");
            Console.WriteLine("3 - Criar Admin");
            Console.WriteLine("0 - Encerrar Programa");
            Console.Write("Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("Entrada inválida! Digite um número.");
                continue;
            }

            switch (escolha)
            {
                case 1:
                    EntrarAdmin();
                    break;
                case 2:
                    EntrarAluno();
                    break;
                case 3:
                    CriarAdmin();
                    break;
                case 0:
                    Console.WriteLine("Encerrando Programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (escolha != 0);
    }

    static void CriarAdmin()
    {
        Console.Write("Login: ");
        string? login = Console.ReadLine();
        Console.Write("Senha: ");
        string? senha = LerSenhaMascarada(); // Entrada mascarada

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
        {
            Console.WriteLine("Login e senha não podem ser nulos ou vazios.");
            return;
        }

        Admin novo = new Admin(login, senha);
        try
        {
            new AdminDAO().Adicionar(novo);
            Console.WriteLine("Admin cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar admin: {ex.Message}");
        }
    }

    static void EntrarAdmin()
    {
        Console.Write("Login: ");
        string? login = Console.ReadLine();
        Console.Write("Senha: ");
        string? senha = LerSenhaMascarada(); // Entrada mascarada

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
        {
            Console.WriteLine("Login e senha não podem ser nulos ou vazios.");
            return;
        }

        Admin? admin = new AdminDAO().BuscarPorLoginESenha(login, senha);

        if (admin != null)
        {
            Console.WriteLine($"Bem-vindo, {admin.Login}");
            MenuAdmin(admin);
        }
        else
        {
            Console.WriteLine("Usuário ou senha incorretos.");
        }
    }

    private static string LerSenhaMascarada()
    {
        string senha = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                senha += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
            {
                senha = senha.Substring(0, senha.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return senha;
    }

    static void EntrarAluno()
    {
        bool tentarNovamente;
        do
        {
            tentarNovamente = false;
            Console.WriteLine("\n--- Login do Aluno ---");
            Console.WriteLine("Escolha como deseja consultar:");
            Console.WriteLine("[1] Por matrícula");
            Console.WriteLine("[2] Por CPF");
            Console.WriteLine("[0] Voltar");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out int opcao) || (opcao != 0 && opcao != 1 && opcao != 2))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                tentarNovamente = true;
                continue;
            }

            if (opcao == 0)
            {
                Console.WriteLine("Voltando ao menu principal...");
                return;
            }

            string? matricula = null;
            string? cpf = null;

            if (opcao == 1)
            {
                Console.Write("Digite a matrícula: ");
                matricula = Console.ReadLine();
                if (string.IsNullOrEmpty(matricula))
                {
                    Console.WriteLine("Matrícula não pode ser vazia.");
                    tentarNovamente = true;
                    continue;
                }
            }
            else if (opcao == 2)
            {
                Console.Write("Digite o CPF: ");
                cpf = Console.ReadLine();
                if (string.IsNullOrEmpty(cpf))
                {
                    Console.WriteLine("CPF não pode ser vazio.");
                    tentarNovamente = true;
                    continue;
                }
            }

            try
            {
                Aluno? aluno = new AlunoDAO().BuscarPorMatriculaECpf(matricula, cpf);

                if (aluno != null)
                {
                    Console.WriteLine($"\nBem-vindo(a), {aluno.NomeCompleto}!");
                    MenuAluno(aluno);
                }
                else
                {
                    Console.WriteLine("Aluno não encontrado com os dados fornecidos.");
                    Console.Write("Deseja tentar novamente? (sim/não): ");
                    string? resposta = Console.ReadLine();
                    tentarNovamente = resposta != null && resposta.Equals("sim", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar aluno: {ex.Message}");
                Console.Write("Deseja tentar novamente? (sim/não): ");
                string? resposta = Console.ReadLine();
                tentarNovamente = resposta != null && resposta.Equals("sim", StringComparison.OrdinalIgnoreCase);
            }
        } while (tentarNovamente);
    }

    static void MenuAdmin(Admin admin)
    {
        int opcao;
        do
        {
            Console.WriteLine("\n--- Menu do Administrador ---");
            Console.WriteLine("[1] Gerenciar Alunos");
            Console.WriteLine("[2] Gerenciar Admins");
            Console.WriteLine("[0] Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    ControlarAlunos(admin);
                    break;
                case 2:
                    ControlarAdmins();
                    break;
                case 0:
                    Console.WriteLine("Saindo do painel de administrador...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }

    static void MenuAluno(Aluno aluno)
    {
        bool sair = false;
        do
        {
            Console.WriteLine("\n--- Menu do Aluno ---");
            Console.WriteLine("[1] Ver dados cadastrais");
            Console.WriteLine("[0] Sair");
            Console.Write("Escolha uma opção: ");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int opcao) || (opcao != 0 && opcao != 1))
            {
                Console.WriteLine("Opção inválida. Por favor, escolha 1 para consultar os dados ou 0 para sair.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    aluno.ExibirDetalhes();
                    break;
                case 0:
                    Console.WriteLine("Saindo do painel do aluno...");
                    sair = true;
                    break;
            }
        } while (!sair);
    }

    static void ControlarAlunos(Admin admin)
    {
        int opcao;
        do
        {
            Console.WriteLine("\n--- Gerenciamento de Alunos ---");
            Console.WriteLine("[1] Cadastrar aluno");
            Console.WriteLine("[2] Alterar aluno");
            Console.WriteLine("[3] Remover aluno");
            Console.WriteLine("[4] Listar alunos");
            Console.WriteLine("[0] Voltar");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    admin.Cadastrar();
                    break;
                case 2:
                    Console.Write("Matrícula do aluno: ");
                    string? matricula = Console.ReadLine();
                    if (string.IsNullOrEmpty(matricula))
                    {
                        Console.WriteLine("Matrícula não pode ser nula ou vazia.");
                        break;
                    }
                    admin.Alterar(matricula);
                    break;
                case 3:
                    Console.Write("Matrícula do aluno: ");
                    matricula = Console.ReadLine();
                    if (string.IsNullOrEmpty(matricula))
                    {
                        Console.WriteLine("Matrícula não pode ser nula ou vazia.");
                        break;
                    }
                    admin.Remover(matricula);
                    break;
                case 4:
                    admin.Listar();
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu anterior...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }

    static void ControlarAdmins()
    {
        AdminDAO dao = new AdminDAO();
        int opcao;
        do
        {
            Console.WriteLine("\n--- Gerenciamento de Admins ---");
            Console.WriteLine("[1] Cadastrar novo admin");
            Console.WriteLine("[2] Alterar senha do admin");
            Console.WriteLine("[3] Remover admin");
            Console.WriteLine("[4] Listar admins");
            Console.WriteLine("[0] Voltar");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    CriarAdmin();
                    break;
                case 2:
                    Console.Write("Digite o nome do Login: ");
                    string? login = Console.ReadLine();
                    Console.Write("Digite a nova senha: ");
                    string? novaSenha = LerSenhaMascarada(); // Entrada mascarada
                    if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(novaSenha))
                    {
                        Console.WriteLine("Login e nova senha não podem ser nulos ou vazios.");
                        break;
                    }
                    try
                    {
                        dao.AlterarSenha(login, novaSenha);
                        Console.WriteLine("Senha alterada com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao alterar senha: {ex.Message}");
                    }
                    break;
                case 3:
                    Console.Write("Remover Admin: ");
                    string? removerLogin = Console.ReadLine();
                    if (string.IsNullOrEmpty(removerLogin))
                    {
                        Console.WriteLine("Login não pode ser nulo ou vazio.");
                        break;
                    }
                    try
                    {
                        dao.Remover(removerLogin);
                        Console.WriteLine("Admin removido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover admin: {ex.Message}");
                    }
                    break;
                case 4:
                    var lista = dao.ListarTodos();
                    Console.WriteLine("Admins cadastrados:");
                    foreach (var adm in lista)
                    {
                        Console.WriteLine($"- {adm.Login} (ID: {adm.Id})");
                    }
                    break;
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 0);
    }
}