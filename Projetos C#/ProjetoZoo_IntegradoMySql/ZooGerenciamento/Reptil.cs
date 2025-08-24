namespace ZooGerenciamento;

public class Reptil : Animal
{
    public bool TemEscamas { get; set; } // Propriedade corrigida
    public string TemperaturaCorporal { get; set; }

    public Reptil(string nome, int idade, string especie, bool temEscamas, string temperaturaCorporal)
        : base(nome, idade, especie)
    {
        TemEscamas = temEscamas;
        TemperaturaCorporal = temperaturaCorporal;
    }

    public override string ExibirInformacoes()
    {
        return base.ExibirInformacoes() + $", Tem Escamas: {TemEscamas}, Temperatura Corporal: {TemperaturaCorporal}";
    }
}