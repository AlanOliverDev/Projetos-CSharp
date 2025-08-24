
// Spotifei/Models/Conteudo/Conteudo.cs
namespace Spotifei
{
    public abstract class Conteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Classificacao { get; set; } = string.Empty;
        public TimeSpan Duracao { get; set; }
        public Artista Artista { get; set; } = new();
        public string TipoConteudo { get; set; } = string.Empty;

        public abstract void Reproduzir();
        public virtual string ObterDetalhes() => $"{Titulo} ({Categoria}) - {Duracao}";
    }
}