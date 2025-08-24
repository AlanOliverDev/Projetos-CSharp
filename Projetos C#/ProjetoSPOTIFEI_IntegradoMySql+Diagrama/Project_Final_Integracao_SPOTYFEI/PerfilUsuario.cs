namespace Spotifei
{
    public class PerfilUsuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!;
        public List<Conteudo> HistoricoReproducao { get; set; } = new();
        public List<Playlist> Playlists { get; set; } = new();

        public void CriarPlaylist(Playlist playlist) => Playlists.Add(playlist);
        public void AdicionarConteudoHistorico(Conteudo conteudo) => HistoricoReproducao.Add(conteudo);
    }
}
