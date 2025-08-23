public class Convidado : Usuario
{
    public Convidado() : base("Convidado", "") { }

    public override void Autenticar()
    {
        Console.WriteLine("⚠️ Convidado logado com acesso limitado de visualização.");
    }
}