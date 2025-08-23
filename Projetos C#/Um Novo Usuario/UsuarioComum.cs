public class UsuarioComum : IUsuario
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public UsuarioComum(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public void Autenticar()
    {
        Console.WriteLine($"✅ Usuário comum {Nome} autenticado com acesso limitado ao dashboard.");
    }
}
