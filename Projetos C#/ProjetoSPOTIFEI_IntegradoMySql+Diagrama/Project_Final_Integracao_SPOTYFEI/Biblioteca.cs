
namespace Spotifei
{
    public class Biblioteca
    {
        public PerfilUsuario Perfil { get; set; }  = new();
        public string Tipo { get; set; } = string.Empty; // "favoritos", "downloads", etc
        public DateTime DataAdicao { get; set; }
        public List<Conteudo> Itens { get; set; } = new();

        public void AdicionarItem(Conteudo conteudo)
        {
            Itens.Add(conteudo);
        }

        public void OrganizarPorTipo()
        {
            // lógica de ordenação
        }
    }
}
