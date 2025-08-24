

namespace Spotifei
{ 

    public class HistoricoReproducao
    {
        public int Id { get; set; }
        public DateTime DataReproducao { get; set; }
        public TimeSpan DuracaoReproduzida { get; set; }
        public string Status { get; set; } = string.Empty; // "completo", "parcial"
        public PerfilUsuario Perfil { get; set; }  = new();
        public Conteudo Conteudo { get; set; } = new Musica();

        public void AdicionarRegistro(Conteudo conteudo, TimeSpan tempo, string status)
        {
            Conteudo = conteudo;
            DuracaoReproduzida = tempo;
            Status = status;
            DataReproducao = DateTime.Now;
        }
    }
}
