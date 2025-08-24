namespace ZooGerenciamento;

public class Ave : Animal
{
    public bool PodeVoar { get; set; } // Propriedade corrigida
    public double TamanhoAsas { get; set; }

    public Ave(string nome, int idade, string especie, bool podeVoar, double tamanhoAsas)
        : base(nome, idade, especie)
    {
        PodeVoar = podeVoar;
        TamanhoAsas = tamanhoAsas;
    }

    public override string ExibirInformacoes()
    {
        return base.ExibirInformacoes() + $", Pode Voar: {PodeVoar}, Tamanho das Asas: {TamanhoAsas}m";
    }
}