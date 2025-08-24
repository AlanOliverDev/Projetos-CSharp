// Spotifei/Models/Conteudo/Podcast.cs
namespace Spotifei
{
    public class Podcast : Conteudo
    {
        public string Apresentador { get; set; } = string.Empty;
        public string NumeroEpisodio { get; set; } = string.Empty;

        public override void Reproduzir() => Console.WriteLine($"Podcast: {Titulo} - Episódio {NumeroEpisodio}");
    }
}