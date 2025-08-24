public interface IGerenciadorAluno
{
    void Cadastrar();
    void Listar();
    void Alterar(string matricula);
    void Remover(string matricula);
}