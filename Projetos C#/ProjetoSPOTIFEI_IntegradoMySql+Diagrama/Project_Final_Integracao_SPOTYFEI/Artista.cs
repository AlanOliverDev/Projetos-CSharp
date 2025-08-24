// Spotifei/Models/Artista.cs
namespace Spotifei
{
    public class Artista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nacionalidade { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public List<Conteudo> Obras { get; set; } = new();

        public void AdicionarObra(Conteudo conteudo) => Obras.Add(conteudo);

        public List<string> ObterDiscografia() => Obras.Select(o => o.Titulo).ToList();
    }
}