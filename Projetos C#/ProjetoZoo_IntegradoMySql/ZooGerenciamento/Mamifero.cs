namespace ZooGerenciamento;

public class Mamifero : Animal
{
    public bool TemPelo { get; set; } // Propriedade corrigida
    public string Habitat { get; set; }

    public Mamifero(string nome, int idade, string especie, bool temPelo, string habitat)
        : base(nome, idade, especie)
    {
        TemPelo = temPelo;
        Habitat = habitat;
    }

    public override string ExibirInformacoes()
    {
        return base.ExibirInformacoes() + $", Tem Pelo: {TemPelo}, Habitat: {Habitat}";
    }
}