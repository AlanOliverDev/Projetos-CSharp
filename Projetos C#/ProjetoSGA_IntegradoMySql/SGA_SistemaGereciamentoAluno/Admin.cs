using System;
using MySql.Data.MySqlClient;

public class Admin : IGerenciadorAluno
{
    private readonly AlunoDAO alunoDAO = new AlunoDAO();
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public Admin() { }

    public Admin(string login, string senha)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        Senha = senha ?? throw new ArgumentNullException(nameof(senha));
    }

    public void Cadastrar()
    {
        bool tentarNovamente;
        do
        {
            tentarNovamente = false;
            Console.WriteLine("\nCadastro de Aluno");
            Aluno aluno = new Aluno();

            // Validação da matrícula
            Console.Write("Matrícula: ");
            string? matriculaInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(matriculaInput))
            {
                Console.WriteLine("Matrícula não pode ser vazia.");
                Console.Write("Deseja tentar novamente? (sim/não): ");
                tentarNovamente = Console.ReadLine()?.Equals("sim", StringComparison.OrdinalIgnoreCase) ?? false;
                continue;
            }
            if (!int.TryParse(matriculaInput, out int matriculaNum) || matriculaNum < 0)
            {
                Console.WriteLine("Matrícula deve ser um número positivo.");
                tentarNovamente = true;
                continue;
            }
            aluno.Matricula = matriculaNum.ToString("D2");

            // Validação do nome completo
            Console.Write("Nome Completo: ");
            string? nomeInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nomeInput))
            {
                Console.WriteLine("Nome completo não pode ser vazio.");
                tentarNovamente = true;
                continue;
            }
            if (nomeInput.Length > 100)
            {
                Console.WriteLine("Nome completo excede o limite de 100 caracteres.");
                tentarNovamente = true;
                continue;
            }
            if (nomeInput.Any(char.IsDigit))
            {
                Console.WriteLine("Nome completo não pode conter números.");
                tentarNovamente = true;
                continue;
            }
            aluno.NomeCompleto = nomeInput.Trim();

            // Validação da idade
            Console.Write("Idade: ");
            string? idadeInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idadeInput) || !int.TryParse(idadeInput, out int idade) || idade < 0)
            {
                Console.WriteLine("Idade deve ser um número positivo.");
                tentarNovamente = true;
                continue;
            }
            aluno.Idade = idade;

            // Validação do CPF
            Console.Write("CPF (opcional): ");
            string? cpfInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cpfInput))
            {
                string cleanedCpf = cpfInput.Replace(".", "").Replace("-", "");
                if (!IsValidCpf(cleanedCpf))
                {
                    Console.WriteLine("CPF inválido. Deve seguir o formato de 11 dígitos válidos.");
                    tentarNovamente = true;
                    continue;
                }
                aluno.Cpf = FormatCpf(cleanedCpf);
            }

            // Validação do endereço
            Console.Write("Endereço (opcional): ");
            string? enderecoInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(enderecoInput))
            {
                if (enderecoInput.Length > 200)
                {
                    Console.WriteLine("Endereço excede o limite de 200 caracteres.");
                    tentarNovamente = true;
                    continue;
                }
                if (enderecoInput.All(char.IsDigit))
                {
                    Console.WriteLine("Endereço não pode conter apenas números.");
                    tentarNovamente = true;
                    continue;
                }
                aluno.Endereco = enderecoInput.Trim();
            }

            try
            {
                alunoDAO.Adicionar(aluno);
                Console.WriteLine("Aluno cadastrado com sucesso!\n");
                return;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                string message = ex.Message.Contains("matricula") ? "Matrícula já existe no sistema." : "CPF já existe no sistema.";
                Console.WriteLine($"Erro: {message}");
                Console.Write("Deseja tentar novamente? (sim/não): ");
                tentarNovamente = Console.ReadLine()?.Equals("sim", StringComparison.OrdinalIgnoreCase) ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar aluno: {ex.Message}");
                Console.Write("Deseja tentar novamente? (sim/não): ");
                tentarNovamente = Console.ReadLine()?.Equals("sim", StringComparison.OrdinalIgnoreCase) ?? false;
            }
        } while (tentarNovamente);
        Console.WriteLine("Cadastro cancelado.");
    }

    private bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return false;

        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
        int remainder = sum % 11;
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCpf += digit1;
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
        remainder = sum % 11;
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        return cpf.EndsWith(digit1.ToString() + digit2.ToString());
    }

    private string FormatCpf(string cpf)
    {
        if (cpf.Length != 11)
            return cpf;
        return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
    }

    public void Listar()
    {
        List<Aluno> alunos = alunoDAO.ObterTodos();

        // Ajustar a largura do buffer do console para evitar quebras
        Console.BufferWidth = Math.Max(Console.BufferWidth, 120);

        Console.WriteLine("=======================================================================================");
        Console.WriteLine($"|| {"MATRICULA",-10} || {"NOME COMPLETO",-30} || {"IDADE",-6} || {"CPF",-15} || {"ENDEREÇO",-30} ||");
        Console.WriteLine("=======================================================================================");
        foreach (Aluno aluno in alunos)
        {
            string nomeTruncado = aluno.NomeCompleto.Length > 30 ? aluno.NomeCompleto.Substring(0, 27) + "..." : aluno.NomeCompleto;
            string enderecoTruncado = (aluno.Endereco ?? "Não informado").Length > 30 ? (aluno.Endereco ?? "Não informado").Substring(0, 27) + "..." : (aluno.Endereco ?? "Não informado");
            Console.WriteLine($"|| {aluno.Matricula,-10} || {nomeTruncado,-30} || {aluno.Idade,-6} || {(aluno.Cpf ?? "Não informado"),-15} || {enderecoTruncado,-30} ||");
        }
        Console.WriteLine("=======================================================================================");
    }

    public void Alterar(string matricula)
    {
        if (string.IsNullOrEmpty(matricula))
        {
            Console.WriteLine("Matrícula não pode ser nula ou vazia.");
            return;
        }

        Aluno? aluno = alunoDAO.ObterPorMatricula(matricula);

        if (aluno != null)
        {
            bool rodando = true;
            while (rodando)
            {
                Console.WriteLine($"\nVocê escolheu alterar o(a) {aluno.NomeCompleto}\n[1] - Nome Completo\n[2] - Idade\n[3] - CPF\n[4] - Endereço\n[5] - Matrícula\n[6] - Sair");
                string? escolhaInput = Console.ReadLine();
                if (!int.TryParse(escolhaInput, out int escolha))
                {
                    Console.WriteLine("Opção inválida, tente novamente!");
                    continue;
                }

                switch (escolha)
                {
                    case 1:
                        Console.Write("Novo Nome Completo: ");
                        string? novoNome = Console.ReadLine();
                        if (string.IsNullOrEmpty(novoNome))
                        {
                            Console.WriteLine("Nome completo não pode ser nulo ou vazio.");
                            break;
                        }
                        if (novoNome.Any(char.IsDigit))
                        {
                            Console.WriteLine("Nome completo não pode conter números.");
                            break;
                        }
                        if (novoNome.Length > 100)
                        {
                            Console.WriteLine("Nome completo excede o limite de 100 caracteres.");
                            break;
                        }
                        aluno.NomeCompleto = novoNome.Trim();
                        break;
                    case 2:
                        Console.Write("Nova Idade: ");
                        string? novaIdadeInput = Console.ReadLine();
                        if (!int.TryParse(novaIdadeInput, out int novaIdade) || novaIdade < 0)
                        {
                            Console.WriteLine("Idade inválida. Deve ser um número positivo.");
                            break;
                        }
                        aluno.Idade = novaIdade;
                        break;
                    case 3:
                        Console.Write("Novo CPF (opcional): ");
                        string? novoCpfInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(novoCpfInput))
                        {
                            string cleanedCpf = novoCpfInput.Replace(".", "").Replace("-", "");
                            if (!IsValidCpf(cleanedCpf))
                            {
                                Console.WriteLine("CPF inválido. Deve seguir o formato de 11 dígitos válidos.");
                                break;
                            }
                            aluno.Cpf = FormatCpf(cleanedCpf);
                        }
                        else
                        {
                            aluno.Cpf = null;
                        }
                        break;
                    case 4:
                        Console.Write("Novo Endereço (opcional): ");
                        string? novoEndereco = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(novoEndereco))
                        {
                            if (novoEndereco.Length > 200)
                            {
                                Console.WriteLine("Endereço excede o limite de 200 caracteres.");
                                break;
                            }
                            if (novoEndereco.All(char.IsDigit))
                            {
                                Console.WriteLine("Endereço não pode conter apenas números.");
                                break;
                            }
                            aluno.Endereco = novoEndereco.Trim();
                        }
                        else
                        {
                            aluno.Endereco = null;
                        }
                        break;
                    case 5:
                        Console.Write("Nova Matrícula: ");
                        string? novaMatriculaInput = Console.ReadLine();
                        if (string.IsNullOrEmpty(novaMatriculaInput))
                        {
                            Console.WriteLine("Matrícula não pode ser nula ou vazia.");
                            break;
                        }
                        if (!int.TryParse(novaMatriculaInput, out int novaMatriculaNum) || novaMatriculaNum < 0)
                        {
                            Console.WriteLine("Matrícula deve ser um número positivo.");
                            break;
                        }
                        aluno.Matricula = novaMatriculaNum.ToString("D2");
                        break;
                    case 6:
                        rodando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente!");
                        break;
                }

                if (escolha != 6)
                {
                    try
                    {
                        alunoDAO.Alterar(aluno);
                        Console.WriteLine("Dados alterados com sucesso!");
                    }
                    catch (MySqlException ex) when (ex.Number == 1062)
                    {
                        string message = ex.Message.Contains("matricula") ? "Matrícula já existe no sistema." : "CPF já existe no sistema.";
                        Console.WriteLine($"Erro: {message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao alterar aluno: {ex.Message}");
                    }
                }

                if (rodando)
                {
                    rodando = ContinuarAlteracao();
                }
            }
        }
        else
        {
            Console.WriteLine("Aluno não encontrado.");
        }
    }

    private bool ContinuarAlteracao()
    {
        while (true)
        {
            Console.WriteLine("\nDeseja alterar mais alguma coisa? (sim/não)");
            string? resposta = Console.ReadLine()?.Trim().ToLower();
            
            if (resposta == "sim" || resposta == "s" || resposta == "ss")
                return true;
            if (resposta == "não" || resposta == "nao" || resposta == "n")
                return false;
            
            Console.WriteLine("Entrada inválida. Por favor, digite 'sim' ou 'não'.");
        }
    }

    public void Remover(string matricula)
    {
        if (string.IsNullOrEmpty(matricula))
        {
            Console.WriteLine("Matrícula não pode ser nula ou vazia.");
            return;
        }

        Aluno? aluno = alunoDAO.ObterPorMatricula(matricula);

        if (aluno != null)
        {
            Console.WriteLine($"\nRemovendo o aluno(a): {aluno.NomeCompleto}");
            alunoDAO.Remover(aluno);
            Console.WriteLine("Aluno removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Aluno não encontrado.");
        }
    }
}