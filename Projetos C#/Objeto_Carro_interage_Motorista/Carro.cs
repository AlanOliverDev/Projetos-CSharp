using System;

public class Carro
{
    private int velocidade;
    private bool iaAtivada;
    private int bateria;

    public Carro()
    {
        velocidade = 0;
        iaAtivada = false;
        bateria = 100;
    }

    public void LigarIA()
    {
        iaAtivada = true;
        Console.WriteLine("IA ativada. Carro no modo autônomo.");
        ReduzirBateria();
    }

    public void DesligarIA()
    {
        iaAtivada = false;
        Console.WriteLine("IA desativada. Controle manual ativado.");
        ReduzirBateria();
    }

    public void AumentarVelocidade()
    {
        if (velocidade < 120)
        {
            velocidade += 10;
            Console.WriteLine($"Velocidade aumentada para {velocidade} km/h.");
        }
        else
        {
            Console.WriteLine("Velocidade máxima atingida.");
        }
        ReduzirBateria();
    }

    public void ReduzirVelocidade()
    {
        if (velocidade > 0)
        {
            velocidade -= 10;
            Console.WriteLine($"Velocidade reduzida para {velocidade} km/h.");
        }
        else
        {
            Console.WriteLine("O carro já está parado.");
        }
        ReduzirBateria();
    }

    public void MostrarStatus()
    {
        Console.WriteLine($"Velocidade atual: {velocidade +=10} km/h | IA: {(iaAtivada ? "Ativa" : "Desativada")} | Bateria do Veículo: {bateria}%");
    }

    private void ReduzirBateria()
    {
        bateria = Math.Max(0, bateria - 1);
    }

    public bool IaEstaAtivada()
    {
        return iaAtivada;
    }
}
