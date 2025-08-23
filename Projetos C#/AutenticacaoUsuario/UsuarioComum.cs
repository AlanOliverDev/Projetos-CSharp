public class UsuarioComum : Usuario
{
    public UsuarioComum(string nome, string senha) : base(nome, senha) { }

    public override void Autenticar()
    {
        Console.WriteLine($"✅ Usuário comum {Nome} autenticado com acesso limitado ao dashboard.");
    }
}