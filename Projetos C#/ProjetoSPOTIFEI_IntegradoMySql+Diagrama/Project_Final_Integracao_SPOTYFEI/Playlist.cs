namespace Spotifei
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public PerfilUsuario Perfil { get; set; } = null!;
        public List<Conteudo> Itens { get; set; } = new();

        public void AdicionarConteudo(Conteudo conteudo)
        {
            Itens.Add(conteudo);
        }

        public void RemoverConteudo(Conteudo conteudo)
        {
            Itens.Remove(conteudo);
        }
    }
}
