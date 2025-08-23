public interface IUsuario
{
    string Nome { get; set; }
    string Senha { get; set; }

    void Autenticar();
}
