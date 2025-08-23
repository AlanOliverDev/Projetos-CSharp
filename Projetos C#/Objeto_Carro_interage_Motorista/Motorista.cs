using System;

public class Motorista
{
    private string nome;
    private int idade;
    private string habilitacao;
    private Carro carroControlado;

    public Motorista(string nome, int idade, string habilitacao, Carro carro)
    {
        this.nome = nome;
        this.idade = idade;
        this.habilitacao = habilitacao;
        this.carroControlado = carro;
    }

    public void AssumirControleManual()
    {
        Console.WriteLine($"{nome} está assumindo o controle manual do carro.");
        carroControlado.DesligarIA();
    }

    public void AtivarModoAutonomo()
    {
        Console.WriteLine($"{nome} está ativando o modo autônomo.");
        carroControlado.LigarIA();
    }

    public void Dirigir()
    {
        if (!carroControlado.IaEstaAtivada())
        {
            Console.WriteLine($"{nome} está dirigindo o carro.");
            carroControlado.AumentarVelocidade();
        }
        else
        {
            Console.WriteLine("Não é possível dirigir manualmente com a IA ativada.");
        }
    }

    public void MostrarStatusDoCarro()
    {
        carroControlado.MostrarStatus();
    }
}
