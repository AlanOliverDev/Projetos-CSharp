using System;
using System.Text;

namespace ZooGerenciamento;

class Program
{
    static void Main(string[] args)
    {
        // Forçar codificação UTF-8 no console de forma robusta
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("[INFO] Usando codificação UTF-8 para entrada e saída.");

        Zoologico zoo = new Zoologico();

        while (true)
        {
            Console.WriteLine("\nMenu do Zoológico\n");
            Console.WriteLine("1. Adicionar Animal (Create)");
            Console.WriteLine("2. Listar Animais (Read)");
            Console.WriteLine("3. Atualizar Animal (Update)");
            Console.WriteLine("4. Remover Animal (Delete)");
            Console.WriteLine("5. Sair\n");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? string.Empty;

            if (opcao == "5")
                break;

            try
            {
                switch (opcao)
                {
                    case "1":
                        Console.Write("Tipo de animal (1-Mamifero, 2-Reptil, 3-Ave): ");
                        string tipo = Console.ReadLine() ?? string.Empty;
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"[DEBUG] Nome digitado: '{nome}'");
                        if (string.IsNullOrWhiteSpace(nome))
                        {
                            Console.WriteLine("Nome não pode ser vazio.");
                            break;
                        }
                        Console.Write("Idade: ");
                        string idadeInput = Console.ReadLine() ?? "0";
                        if (!int.TryParse(idadeInput, out int idade) || idade <= 0)
                        {
                            Console.WriteLine("Idade deve ser um número positivo.");
                            break;
                        }
                        Console.Write("Espécie: ");
                        string especie = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"[DEBUG] Espécie digitada: '{especie}'");
                        if (string.IsNullOrWhiteSpace(especie))
                        {
                            Console.WriteLine("Espécie não pode ser vazia.");
                            break;
                        }

                        Animal? animal = null;
                        if (tipo == "1")
                        {
                            Console.Write("Tem Pelo (true/false): ");
                            string temPeloInput = Console.ReadLine() ?? "false";
                            if (!bool.TryParse(temPeloInput, out bool temPelo))
                            {
                                Console.WriteLine("Valor inválido para Tem Pelo.");
                                break;
                            }
                            Console.Write("Habitat: ");
                            string habitat = Console.ReadLine() ?? string.Empty;
                            animal = new Mamifero(nome, idade, especie, temPelo, habitat);
                        }
                        else if (tipo == "2")
                        {
                            Console.Write("Tem Escamas (true/false): ");
                            string temEscamasInput = Console.ReadLine() ?? "false";
                            if (!bool.TryParse(temEscamasInput, out bool temEscamas))
                            {
                                Console.WriteLine("Valor inválido para Tem Escamas.");
                                break;
                            }
                            Console.Write("Temperatura Corporal: ");
                            string temp = Console.ReadLine() ?? string.Empty;
                            animal = new Reptil(nome, idade, especie, temEscamas, temp);
                        }
                        else if (tipo == "3")
                        {
                            Console.Write("Pode Voar (true/false): ");
                            string podeVoarInput = Console.ReadLine() ?? "false";
                            if (!bool.TryParse(podeVoarInput, out bool podeVoar))
                            {
                                Console.WriteLine("Valor inválido para Pode Voar.");
                                break;
                            }
                            Console.Write("Tamanho das Asas (em metros): ");
                            string tamanhoAsasInput = Console.ReadLine() ?? "0.0";
                            if (!double.TryParse(tamanhoAsasInput, out double tamanhoAsas) || tamanhoAsas < 0)
                            {
                                Console.WriteLine("Tamanho das Asas deve ser um número não negativo.");
                                break;
                            }
                            animal = new Ave(nome, idade, especie, podeVoar, tamanhoAsas);
                        }
                        if (animal != null) zoo.AdicionarAnimal(animal);
                        break;

                    case "2":
                        zoo.ListarAnimais();
                        break;

                    case "3":
                        Console.Write("ID do animal a atualizar: ");
                        string idUpdateInput = Console.ReadLine() ?? "0";
                        if (!int.TryParse(idUpdateInput, out int idUpdate) || idUpdate <= 0)
                        {
                            Console.WriteLine("ID inválido.");
                            break;
                        }
                        Animal? animalExistente = zoo.ObterAnimalPorId(idUpdate);
                        if (animalExistente == null)
                        {
                            Console.WriteLine("Animal não encontrado.");
                            break;
                        }

                        Console.Write($"Novo Nome ({animalExistente.Nome}): ");
                        string novoNome = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"[DEBUG] Novo Nome digitado: '{novoNome}'");
                        if (!string.IsNullOrWhiteSpace(novoNome)) animalExistente.Nome = novoNome;

                        Console.Write($"Nova Idade ({animalExistente.Idade}): ");
                        string inputIdade = Console.ReadLine() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(inputIdade))
                        {
                            if (int.TryParse(inputIdade, out int novaIdade) && novaIdade > 0)
                                animalExistente.Idade = novaIdade;
                            else
                                Console.WriteLine("Idade inválida; mantendo valor atual.");
                        }

                        Console.Write($"Nova Espécie ({animalExistente.Especie}): ");
                        string novaEspecie = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine($"[DEBUG] Nova Espécie digitada: '{novaEspecie}'");
                        if (!string.IsNullOrWhiteSpace(novaEspecie)) animalExistente.Especie = novaEspecie;

                        if (animalExistente is Mamifero mamifero)
                        {
                            Console.Write($"Novo Tem Pelo ({mamifero.TemPelo}): ");
                            string inputPelo = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(inputPelo) && bool.TryParse(inputPelo, out bool novoPelo))
                                mamifero.TemPelo = novoPelo;

                            Console.Write($"Novo Habitat ({mamifero.Habitat}): ");
                            string novoHabitat = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(novoHabitat)) mamifero.Habitat = novoHabitat;
                        }
                        else if (animalExistente is Reptil reptil)
                        {
                            Console.Write($"Novo Tem Escamas ({reptil.TemEscamas}): ");
                            string inputEscamas = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(inputEscamas) && bool.TryParse(inputEscamas, out bool novoEscamas))
                                reptil.TemEscamas = novoEscamas;

                            Console.Write($"Nova Temperatura Corporal ({reptil.TemperaturaCorporal}): ");
                            string novaTemp = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(novaTemp)) reptil.TemperaturaCorporal = novaTemp;
                        }
                        else if (animalExistente is Ave ave)
                        {
                            Console.Write($"Novo Pode Voar ({ave.PodeVoar}): ");
                            string inputVoar = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(inputVoar) && bool.TryParse(inputVoar, out bool novoVoar))
                                ave.PodeVoar = novoVoar;

                            Console.Write($"Novo Tamanho das Asas ({ave.TamanhoAsas}): ");
                            string inputAsas = Console.ReadLine() ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(inputAsas) && double.TryParse(inputAsas, out double novoTamanho) && novoTamanho >= 0)
                                ave.TamanhoAsas = novoTamanho;
                            else
                                Console.WriteLine("Tamanho inválido; mantendo valor atual.");
                        }

                        zoo.AtualizarAnimal(animalExistente);
                        break;

                    case "4":
                        Console.Write("ID do animal a remover: ");
                        string idRemoveInput = Console.ReadLine() ?? "0";
                        if (!int.TryParse(idRemoveInput, out int idRemove) || idRemove <= 0)
                        {
                            Console.WriteLine("ID inválido.");
                            break;
                        }
                        zoo.RemoverAnimal(idRemove);
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Erro: Entrada inválida. Por favor, insira valores no formato correto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}