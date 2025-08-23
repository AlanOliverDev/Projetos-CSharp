public class Convidado : IUsuario
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public Convidado()
    {
        Nome = "Convidado";
        Senha = "";
    }

    public void Autenticar()
    {
        Console.WriteLine("⚠️ Convidado logado com acesso limitado de visualização.");
    }
}
