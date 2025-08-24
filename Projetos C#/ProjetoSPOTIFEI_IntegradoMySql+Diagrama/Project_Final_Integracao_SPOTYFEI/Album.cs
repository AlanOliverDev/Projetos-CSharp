// Spotifei/Models/Album.cs
namespace Spotifei
{
    public class Album
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataLancamento { get; set; }
        public List<Musica> Musicas { get; set; } = new();

        public void AdicionarMusica(Musica musica) => Musicas.Add(musica);

        public TimeSpan ObterDuracaoTotal() =>
            new TimeSpan(Musicas.Sum(m => m.Duracao.Ticks));
    }
}