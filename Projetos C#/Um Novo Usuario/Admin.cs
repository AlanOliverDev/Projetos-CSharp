public class Admin : IUsuario
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public Admin(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public void Autenticar()
    {
        Console.WriteLine($"✅ Admin {Nome} autenticado com acesso total ao dashboard!");
    }
}
