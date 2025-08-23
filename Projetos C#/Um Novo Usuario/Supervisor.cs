public class Supervisor : IUsuario
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public Supervisor(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public void Autenticar()
    {
        Console.WriteLine($"✅ Supervisor {Nome} autenticado com acesso de monitoramento e gestão parcial.");
    }
}
