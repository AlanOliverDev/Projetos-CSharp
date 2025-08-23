using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Catálogo Netflix ===");
            Console.WriteLine("1. Ver Filmes");
            Console.WriteLine("2. Ver Séries");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    MenuFilmes();
                    break;
                case "2":
                    MenuSeries();
                    break;
                case "3":
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void MenuFilmes()
    {
        Console.Clear();
        Console.WriteLine("=== Filmes Disponíveis ===");
        Console.WriteLine("1. O Irlandês");
        Console.WriteLine("2. Bird Box");
        Console.WriteLine("3. Resgate");
        Console.Write("Escolha um filme: ");
        string escolha = Console.ReadLine();

        switch (escolha)
        {
            case "1":
                FilmeOIrlandes();
                break;
            case "2":
                FilmeBirdBox();
                break;
            case "3":
                FilmeResgate();
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void MenuSeries()
    {
        Console.Clear();
        Console.WriteLine("=== Séries Disponíveis ===");
        Console.WriteLine("1. Stranger Things");
        Console.WriteLine("2. The Crown");
        Console.WriteLine("3. Dark");
        Console.Write("Escolha uma série: ");
        string escolha = Console.ReadLine();

        switch (escolha)
        {
            case "1":
                SerieStrangerThings();
                break;
            case "2":
                SerieTheCrown();
                break;
            case "3":
                SerieDark();
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    // Métodos para Filmes
    static void FilmeOIrlandes()
    {
        Console.Clear();
        Console.WriteLine("Filme: O Irlandês");
        Console.WriteLine("Sinopse: Um assassino da máfia relembra sua participação no desaparecimento de Jimmy Hoffa.");
        Console.WriteLine("Elenco: Robert De Niro, Al Pacino, Joe Pesci");
        Console.WriteLine("Avaliação: 8.0/10");
    }

    static void FilmeBirdBox()
    {
        Console.Clear();
        Console.WriteLine("Filme: Bird Box");
        Console.WriteLine("Sinopse: Uma força misteriosa dizima a população. Para sobreviver, uma mulher e seus filhos devem percorrer um rio com os olhos vendados.");
        Console.WriteLine("Elenco: Sandra Bullock, Trevante Rhodes, John Malkovich");
        Console.WriteLine("Avaliação: 6.6/10");
    }

    static void FilmeResgate()
    {
        Console.Clear();
        Console.WriteLine("Filme: Resgate");
        Console.WriteLine("Sinopse: Um mercenário embarca em uma missão para salvar o filho sequestrado de um chefão do crime.");
        Console.WriteLine("Elenco: Chris Hemsworth, Rudhraksh Jaiswal, Randeep Hooda");
        Console.WriteLine("Avaliação: 7.2/10");
    }

    // Métodos para Séries
    static void SerieStrangerThings()
    {
        Console.Clear();
        Console.WriteLine("Série: Stranger Things");
        Console.WriteLine("Sinopse: Crianças enfrentam forças sobrenaturais enquanto tentam encontrar seu amigo desaparecido.");
        Console.WriteLine("Elenco: Millie Bobby Brown, Finn Wolfhard, David Harbour");
        Console.WriteLine("Avaliação: 8.7/10");
    }

    static void SerieTheCrown()
    {
        Console.Clear();
        Console.WriteLine("Série: The Crown");
        Console.WriteLine("Sinopse: A história do reinado da Rainha Elizabeth");
    }
    static void SerieDark()
    {
        Console.Clear();
        Console.WriteLine("Série: Dark");
        Console.WriteLine("Sinopse: O desaparecimento de uma criança revela os segredos de quatro famílias e um mistério envolvendo viagens no tempo.");
        Console.WriteLine("Elenco: Louis Hofmann, Karoline Eichhorn, Lisa Vicari");
        Console.WriteLine("Avaliação: 8.8/10");
    }
}