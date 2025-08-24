using Spotifei;
using System.Collections.Generic;

public interface IGerenciadorConteudo
{
    void AdicionarConteudo(Conteudo conteudo);
    void RemoverConteudo(Conteudo conteudo);
    List<Conteudo> Buscar(string termo);
    List<Conteudo> FiltrarPorCategoria(string categoria);
}
