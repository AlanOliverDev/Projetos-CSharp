public abstract class Usuario
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public Usuario(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public abstract void Autenticar();
}