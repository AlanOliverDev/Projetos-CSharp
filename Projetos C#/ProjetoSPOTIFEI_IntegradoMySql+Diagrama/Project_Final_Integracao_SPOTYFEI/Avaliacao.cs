
namespace Spotifei
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; }

        public PerfilUsuario Autor { get; set; }  = new();
        public Conteudo Conteudo { get; set; } = new Musica();

        public void AtualizarAvaliacao(int novaNota, string novoComentario)
        {
            Nota = novaNota;
            Comentario = novoComentario;
        }
    }
}
