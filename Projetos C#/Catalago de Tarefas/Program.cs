using System; // Importa funcionalidades básicas do .NET
using System.Collections.Generic; // Permite o uso de listas genéricas
using System.Globalization; // Permite configurar formatos de números e moedas
using System.Text; // Usado para configurar codificação de texto no console
using System.Text.RegularExpressions; // Usado para validar strings com expressões regulares

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // Define a codificação UTF-8 para suportar caracteres especiais
        bool continuar = true; // Controla o loop do menu principal

        while (continuar) // Executa o menu enquanto o usuário não optar por sair
        {
            Console.Clear(); // Limpa a tela do console
            Console.WriteLine("📚 CATÁLOGO DE TAREFAS DE CASA");
            Console.WriteLine("1 - Cálculo de Médias");
            Console.WriteLine("2 - Radar de Velocidade");
            Console.WriteLine("3 - Controle de Caixa");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string escolha = Console.ReadLine(); // Lê a escolha do usuário

            switch (escolha)
            {
                case "1":
                    CalculoDeMedias(); // Executa a tarefa 1
                    break;
                case "2":
                    RadarDeVelocidade(); // Executa a tarefa 2
                    break;
                case "3":
                    ControleDeCaixa(); // Executa a tarefa 3
                    break;
                case "0":
                    continuar = false; // Encerra o programa
                    break;
                default:
                    Console.WriteLine("\n❌ Opção inválida! Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey(); // Aguarda tecla e volta ao menu
                    break;
            }
        }

        Console.WriteLine("\n👋 Programa encerrado. Até a próxima!"); // Mensagem final
    }

    // ---------------------------- TAREFA 1: Cálculo de Médias ----------------------------
    static void CalculoDeMedias()
    {
        int errosConsecutivos = 0; // Contador de tentativas inválidas

        int numAlunos = ObterNumero("Digite o número de alunos:", ref errosConsecutivos); // Número de alunos
        int numNotas = ObterNumero("Digite o número de notas por aluno:", ref errosConsecutivos); // Notas por aluno

        List<Aluno> alunos = new List<Aluno>(); // Lista para armazenar os alunos

        for (int i = 0; i < numAlunos; i++)
        {
            Console.Write($"\nDigite o nome do aluno {i + 1}: ");
            string nome = Console.ReadLine().Trim(); // Lê e remove espaços extras

            // Valida nome não repetido, vazio ou com caracteres inválidos
            while (alunos.Exists(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)) ||
                   string.IsNullOrWhiteSpace(nome) || !Regex.IsMatch(nome, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine("❌ Nome inválido ou já cadastrado. Tente novamente:");
                nome = Console.ReadLine().Trim();
            }

            double[] notas = new double[numNotas]; // Array de notas

            for (int j = 0; j < numNotas; j++)
            {
                notas[j] = ObterNota($"Digite a nota {j + 1} do aluno {nome} (0 a 10):"); // Coleta nota
            }

            alunos.Add(new Aluno(nome, notas)); // Adiciona aluno à lista
        }

        Console.WriteLine("\n📊 RESULTADO FINAL:");
        Console.WriteLine("Aluno\t\tMédia\t\tStatus");

        foreach (var aluno in alunos)
        {
            double media = aluno.CalcularMedia(); // Calcula média
            string status = media >= 7 ? "✅ Aprovado" : "❌ Reprovado"; // Define status
            Console.WriteLine($"{aluno.Nome,-10}\t{media:F2}\t\t{status}");
        }

        VoltarOuSair(); // Retorna ao menu ou sai
    }

    static int ObterNumero(string mensagem, ref int erros)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            if (int.TryParse(Console.ReadLine(), out int valor) && valor > 0) // Valida se número é válido
                return valor;

            Console.WriteLine("❌ Número inválido. Tente novamente.");
            erros++; // Incrementa tentativas inválidas
        }
    }

    static double ObterNota(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            if (double.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double nota)
                && nota >= 0 && nota <= 10) // Verifica se nota está entre 0 e 10
                return nota;

            Console.WriteLine("❌ Nota inválida. Digite um número entre 0 e 10.");
        }
    }

    class Aluno
    {
        public string Nome { get; set; } // Nome do aluno
        public double[] Notas { get; set; } // Notas do aluno

        public Aluno(string nome, double[] notas)
        {
            Nome = nome;
            Notas = notas;
        }

        public double CalcularMedia()
        {
            double soma = 0;
            foreach (var n in Notas)
                soma += n; // Soma as notas
            return soma / Notas.Length; // Calcula média
        }
    }

    // ---------------------------- TAREFA 2: Radar de Velocidade ----------------------------
    static void RadarDeVelocidade()
    {
        Console.WriteLine("\n=== SISTEMA DE RADAR ===");

        double vMax = LerVelocidade("Digite a velocidade máxima da via (km/h): ");
        double vCaptada = LerVelocidade("Digite a velocidade do motorista (km/h): ");

        double vMin = vMax * 0.5; // Velocidade mínima é 50% da máxima
        double toleranciaMin = vMin * 0.9; // Tolerância mínima de 10%
        double toleranciaMax = vMax * 1.10; // Tolerância máxima de 10%

        Console.WriteLine($"\nVelocidade Máxima: {vMax} km/h");
        Console.WriteLine($"Velocidade Mínima Permitida: {vMin} km/h");
        Console.WriteLine($"Velocidade Captada: {vCaptada} km/h");

        if (vCaptada < toleranciaMin)
        {
            double defasagem = ((vMin - vCaptada) / vMin) * 100; // Calcula percentual abaixo
            Console.WriteLine($"🔻 Abaixo da mínima: {defasagem:F2}%");

            if (defasagem <= 25)
                Penalidade("Média", 4, 130.16m);
            else if (defasagem <= 50)
                Penalidade("Grave", 5, 195.23m);
            else
                Penalidade("Gravíssima", 7, 880.41m, true);
        }
        else if (vCaptada > toleranciaMax)
        {
            double excesso = ((vCaptada - vMax) / vMax) * 100; // Calcula percentual acima
            Console.WriteLine($"🔺 Acima da máxima: {excesso:F2}%");

            if (excesso <= 25)
                Penalidade("Média", 4, 130.16m);
            else if (excesso <= 50)
                Penalidade("Grave", 5, 195.23m);
            else
                Penalidade("Gravíssima", 7, 880.41m, true);
        }
        else
        {
            Console.WriteLine("✅ Velocidade dentro da faixa permitida.");
        }

        VoltarOuSair(); // Opção de retornar ao menu ou encerrar
    }

    static double LerVelocidade(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (double.TryParse(Console.ReadLine(), out double v) && v > 0) // Valida valor positivo
                return v;

            Console.WriteLine("❌ Valor inválido. Digite novamente.");
        }
    }

    static void Penalidade(string tipo, int pontos, decimal multa, bool suspensao = false)
    {
        Console.WriteLine($"🚨 Penalidade: Infração {tipo}");
        Console.WriteLine($"Pontos: {pontos}");
        Console.WriteLine($"Multa: R$ {multa:F2}");
        if (suspensao)
            Console.WriteLine("⚠️ Medida administrativa: Possível suspensão da CNH.");
    }

    // ---------------------------- TAREFA 3: Controle de Caixa ----------------------------
    static void ControleDeCaixa()
    {
        CultureInfo cultura = new CultureInfo("pt-BR"); // Formato brasileiro
        List<decimal> entradas = new List<decimal>(); // Lista de entradas
        List<decimal> saidas = new List<decimal>(); // Lista de saídas
        string opcao;

        do
        {
            Console.WriteLine("\nMENU DE MOVIMENTAÇÃO:");
            Console.WriteLine("1 - Entrada");
            Console.WriteLine("2 - Saída");
            Console.WriteLine("0 - Calcular saldo e sair");
            Console.Write("Opção: ");
            opcao = Console.ReadLine(); // Lê escolha do usuário

            switch (opcao)
            {
                case "1":
                    entradas.Add(ObterValor("Digite o valor da entrada: R$ "));
                    break;
                case "2":
                    saidas.Add(ObterValor("Digite o valor da saída: R$ "));
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("❌ Opção inválida.");
                    break;
            }

        } while (opcao != "0");

        decimal totalEntrada = Somar(entradas); // Soma total de entradas
        decimal totalSaida = Somar(saidas); // Soma total de saídas
        decimal saldo = totalEntrada - totalSaida; // Calcula saldo

        Console.WriteLine($"\nTotal Entradas: {totalEntrada.ToString("C", cultura)}");
        Console.WriteLine($"Total Saídas: {totalSaida.ToString("C", cultura)}");
        Console.WriteLine($"Saldo Final: {saldo.ToString("C", cultura)}");

        if (saldo > 0)
            Console.WriteLine("✅ Dia POSITIVO.");
        else if (saldo < 0)
            Console.WriteLine("🔻 Dia NEGATIVO.");
        else
            Console.WriteLine("⚖️ Dia NEUTRO.");

        VoltarOuSair(); // Volta ou encerra
    }

    static decimal ObterValor(string mensagem)
    {
        CultureInfo cultura = new CultureInfo("pt-BR");

        while (true)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine().Replace(".", ",");

            if (decimal.TryParse(entrada, NumberStyles.Number, cultura, out decimal valor) && valor >= 0)
                return valor;

            Console.WriteLine("❌ Valor inválido.");
        }
    }

    static decimal Somar(List<decimal> valores)
    {
        decimal total = 0;
        foreach (var v in valores) total += v; // Soma cada valor
        return total;
    }

    // ---------------------------- MENU DE RETORNO OU SAÍDA ----------------------------
    static void VoltarOuSair()
    {
        while (true)
        {
            Console.WriteLine("\nDigite [0] para ENCERRAR ou [1] para VOLTAR ao menu:");
            string escolha = Console.ReadLine();

            if (escolha == "1")
                break; // Volta ao menu principal
            else if (escolha == "0")
            {
                Console.WriteLine("\n👋 Tarefa encerrada. Obrigado por utilizar o sistema!");
                Environment.Exit(0); // Encerra o programa
            }
            else
            {
                Console.WriteLine("❌ Entrada inválida. Por favor, digite 0 ou 1."); // Mensagem de erro
            }
        }
    }
}
