public class Admin : Usuario
{
    public Admin(string nome, string senha) : base(nome, senha) { }

    public override void Autenticar()
    {
        Console.WriteLine($"✅ Admin {Nome} autenticado com acesso total ao dashboard!");
    }
}