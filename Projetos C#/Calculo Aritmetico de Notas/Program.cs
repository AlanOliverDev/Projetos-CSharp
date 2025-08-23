// See https://aka.ms/new-console-template for more information

using System; 
// A biblioteca 'System' fornece funcionalidades básicas para entrada e saída, tipos de dados fundamentais (como string, int), manipulação de exceções, etc.

using System.Collections.Generic;
// A biblioteca 'System.Collections.Generic' fornece coleções genéricas como listas, dicionários, filas, etc., permitindo que você trabalhe com tipos de dados de forma mais segura e eficiente.

using System.Globalization;
// A biblioteca 'System.Globalization' fornece classes para manipulação de informações culturais, como formatação de números, datas e fusos horários, útil quando se lida com locais e idiomas diferentes.

using System.Text.RegularExpressions;
// A biblioteca 'System.Text.RegularExpressions' permite que você utilize expressões regulares para buscar, validar e manipular padrões de texto em strings, como validação de nomes ou padrões de entrada do usuário.

using System.Text;
// A biblioteca 'System.Text' fornece classes para manipulação de textos, como a classe 'StringBuilder', que é útil para concatenar strings de forma mais eficiente.


class Program
{
    static int errosConsecutivos = 0; // Contador para ativar a Alexa Malcriada

    static void Main(string[] args)
    {
        // Garante que o console aceite emojis corretamente
        Console.OutputEncoding = Encoding.UTF8;

        int numAlunos, numNotas;

        // Loop principal para perguntar o número de alunos e de notas por aluno
        while (true)
        {
            numAlunos = ObterNumero("📋 Digite o número de alunos que deseja cadastrar:", 
                                    "⚠️ Erro: Por favor, digite um número válido e maior que zero para o número de alunos.");
            numNotas = ObterNumero("📚 Digite o número de notas que deseja cadastrar para cada aluno:", 
                                   "⚠️ Erro: Por favor, digite um número válido e maior que zero para o número de notas.");

            // Mostra o resumo das escolhas feitas pelo usuário
            Console.WriteLine($"\n🔎 Você escolheu {numAlunos} aluno(s) e {numNotas} nota(s) por aluno.");
            Console.WriteLine("🔄 Deseja modificar esses valores? (S/N)");

            string resposta = Console.ReadLine().Trim().ToUpper();
            while (resposta != "S" && resposta != "N")
            {
                Console.WriteLine(); // Adiciona uma linha em branco antes do erro
                ExibirErro("⚠️ Erro: Resposta inválida! Digite 'S' para confirmar ou 'N' para modificar.");
                Console.WriteLine("🔄 Deseja modificar esses valores? (S/N)");
                resposta = Console.ReadLine().Trim().ToUpper();
            }

            if (resposta == "S")
            {
                errosConsecutivos = 0; // Reseta o contador de erros
                continue; // Volta para as perguntas de número de alunos e notas
            }

            break;
        }

        List<Aluno> alunos = new List<Aluno>(); // Lista que armazenará os alunos

        // Loop para cadastrar os alunos e suas respectivas notas
        for (int i = 0; i < numAlunos; i++)
        {
            Console.WriteLine(); // Adiciona uma linha em branco para facilitar a visualização

            Console.WriteLine($"✏️ Digite o nome do aluno {i + 1}:");
            string nome = Console.ReadLine().Trim();

            // Verifica se o nome já foi cadastrado, ignorando letras maiúsculas/minúsculas
            while (alunos.Exists(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(); // Adiciona uma linha em branco antes do erro
                ExibirErro("⚠️ Erro: Já existe um aluno com esse nome, tente outro.");
                nome = Console.ReadLine().Trim();
            }

            // Valida o nome do aluno, que deve conter apenas caracteres alfabéticos
            while (string.IsNullOrWhiteSpace(nome) || !Regex.IsMatch(nome, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine(); // Adiciona uma linha em branco antes do erro
                ExibirErro("⚠️ Erro: O nome deve conter apenas caracteres alfabéticos (A-Z). Digite novamente:");
                nome = Console.ReadLine().Trim();
            }

            double[] notas = new double[numNotas];

            // Loop para cadastrar as notas do aluno
            for (int j = 0; j < numNotas; j++)
            {
                Console.WriteLine(); // Adiciona uma linha em branco para facilitar a visualização
                notas[j] = ObterNota($"📝 Digite a nota {j + 1} do aluno {nome} (0 a 10):");
            }

            alunos.Add(new Aluno(nome, notas)); // Adiciona o aluno à lista
        }

        // Exibe os resultados
        Console.WriteLine(); // Adiciona uma linha em branco antes de mostrar os resultados
        Console.WriteLine("📊 Resultados:");
        Console.WriteLine("🏷️ Nomes      🎯 Médias       ✅ Status");
        Console.WriteLine("-------------------------------------------");

        foreach (var aluno in alunos)
        {
            double media = aluno.CalcularMedia();
            string statusEmoji = media >= 7 ? "✅ Aprovado" : "❌ Reprovado";

            Console.WriteLine($"{aluno.Nome,-12} {media:F2}          {statusEmoji}");
        }
    }

    // Função para obter um número válido do usuário
    static int ObterNumero(string mensagem, string erroPadrao)
    {
        int numero;
        bool primeiroErro = true;

        while (true)
        {
            Console.WriteLine(); // Adiciona uma linha em branco para facilitar a visualização
            Console.WriteLine(mensagem);

            if (int.TryParse(Console.ReadLine(), out numero) && numero > 0)
            {
                errosConsecutivos = 0; // Reseta o contador de erros após resposta correta
                return numero;
            }

            // Exibe a mensagem padrão de erro
            if (primeiroErro)
            {
                Console.WriteLine(erroPadrao);
                primeiroErro = false;
            }
            else
            {
                // Exibe a mensagem malcriada após erro
                ExibirErro(erroPadrao);
            }
        }
    }

    // Função para obter uma nota válida entre 0 e 10
    static double ObterNota(string mensagem)
    {
        double nota;
        while (true)
        {
            Console.WriteLine(); // Adiciona uma linha em branco para facilitar a visualização
            Console.WriteLine(mensagem);
            string input = Console.ReadLine().Trim();

            if (input == "-0")
            {
                ExibirErro("⚠️ Erro: Nota inválida! O valor '-0' não existe. Digite novamente:");
                continue;
            }

            if (double.TryParse(input.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out nota) && nota >= 0 && nota <= 10)
            {
                errosConsecutivos = 0; // Reseta o contador de erros após resposta correta
                return nota;
            }

            ExibirErro("⚠️ Erro: A nota deve ser um número entre 0 e 10.");
        }
    }

    // Função para exibir mensagens de erro
    static void ExibirErro(string mensagem)
    {
        // Mensagem padrão de erro
        Console.WriteLine(mensagem);

        // Respostas malcriadas da Alexa
        string[] respostasMalcriadas = {
            "🤨 Meu anjo, já erramos uma vez... quer tentar de novo direito?",
            "😒 Olha... se fosse um concurso de erro, você tava no pódio agora!",
            "🙄 Ai ai... será que vou precisar desenhar pra você entender?",
            "😤 EU NÃO ACREDITO QUE VOCÊ ERROU DE NOVO! Bora focar, criatura!"
        };

        if (errosConsecutivos < respostasMalcriadas.Length)
        {
            // Exibe a resposta malcriada com humor da Alexa
            Console.WriteLine(respostasMalcriadas[errosConsecutivos]);
        }
        else
        {
            // Mensagem final após vários erros
            Console.WriteLine("😤 Fala sério, eu não tenho o dia todo! Anda logo!");
        }

        errosConsecutivos++; // Incrementa o contador de erros
    }
}

// Classe para representar os alunos
class Aluno
{
    public string Nome { get; set; }
    public double[] Notas { get; set; }

    // Construtor da classe Aluno
    public Aluno(string nome, double[] notas)
    {
        Nome = nome;
        Notas = notas;
    }

    // Método para calcular a média das notas do aluno
    public double CalcularMedia()
    {
        double soma = 0;
        foreach (var nota in Notas)
        {
            soma += nota; // Soma as notas
        }
        return soma / Notas.Length; // Retorna a média
    }
}
