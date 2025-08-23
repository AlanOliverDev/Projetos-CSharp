// See https://aka.ms/new-console-template for more information
using System;

class RadarDeVelocidade
{
    static void Main()
    {
        Console.WriteLine("=== SISTEMA DE RADAR - POLÍCIA RODOVIÁRIA FEDERAL ===");

        double velocidadeMaxima = LerVelocidade("Digite a velocidade máxima permitida da via (km/h): ");
        double velocidadeMotorista = LerVelocidade("Digite a velocidade captada do motorista (km/h): ");

        double velocidadeMinimaPermitida = velocidadeMaxima * 0.5;
        double toleranciaMinima = velocidadeMinimaPermitida * 0.9; // 10% abaixo da mínima
        double toleranciaMaxima = velocidadeMaxima * 1.10;         // 10% acima da máxima

        Console.WriteLine("\n=== RESULTADO DA AVALIAÇÃO ===");
        Console.WriteLine($"Velocidade máxima da via: {velocidadeMaxima} km/h");
        Console.WriteLine($"Velocidade mínima permitida (50% da máxima): {velocidadeMinimaPermitida:F1} km/h");
        Console.WriteLine($"Velocidade captada do motorista: {velocidadeMotorista} km/h");

        if (velocidadeMotorista < toleranciaMinima)
        {
            double defasagemPercentual = ((velocidadeMinimaPermitida - velocidadeMotorista) / velocidadeMinimaPermitida) * 100;
            Console.WriteLine("Situação: Velocidade abaixo da mínima permitida.");
            Console.WriteLine($"Diferença percentual em relação à velocidade mínima: -{defasagemPercentual:F2}%");

            if (defasagemPercentual <= 25)
            {
                Console.WriteLine("Penalidade: Infração Média");
                Console.WriteLine("Pontos na CNH: 4");
                Console.WriteLine("Multa: R$ 130,16");
            }
            else if (defasagemPercentual <= 50)
            {
                Console.WriteLine("Penalidade: Infração Grave");
                Console.WriteLine("Pontos na CNH: 5");
                Console.WriteLine("Multa: R$ 195,23");
            }
            else
            {
                Console.WriteLine("Penalidade: Infração Gravíssima");
                Console.WriteLine("Pontos na CNH: 7");
                Console.WriteLine("Multa: R$ 880,41 (gravíssima com fator multiplicador 3)");
                Console.WriteLine("Medida administrativa: Possível suspensão do direito de dirigir.");
            }
        }
        else if (velocidadeMotorista > toleranciaMaxima)
        {
            double excessoPercentual = ((velocidadeMotorista - velocidadeMaxima) / velocidadeMaxima) * 100;
            Console.WriteLine("Situação: Velocidade acima da máxima permitida.");
            Console.WriteLine($"Diferença percentual em relação à velocidade máxima: +{excessoPercentual:F2}%");

            if (excessoPercentual <= 25)
            {
                Console.WriteLine("Penalidade: Infração Média");
                Console.WriteLine("Pontos na CNH: 4");
                Console.WriteLine("Multa: R$ 130,16");
            }
            else if (excessoPercentual <= 50)
            {
                Console.WriteLine("Penalidade: Infração Grave");
                Console.WriteLine("Pontos na CNH: 5");
                Console.WriteLine("Multa: R$ 195,23");
            }
            else
            {
                Console.WriteLine("Penalidade: Infração Gravíssima");
                Console.WriteLine("Pontos na CNH: 7");
                Console.WriteLine("Multa: R$ 880,41 (gravíssima com fator multiplicador 3)");
                Console.WriteLine("Medida administrativa: Possível suspensão do direito de dirigir.");
            }
        }
        else
        {
            // Mostra a faixa de tolerância somente se o motorista estiver dentro da faixa
            if ((velocidadeMotorista >= toleranciaMinima && velocidadeMotorista < velocidadeMinimaPermitida) ||
                (velocidadeMotorista > velocidadeMaxima && velocidadeMotorista <= toleranciaMaxima))
            {
                Console.WriteLine($"Faixa de tolerância: {toleranciaMinima:F1} km/h a {toleranciaMaxima:F1} km/h");
            }

            Console.WriteLine("Situação: Velocidade dentro da faixa permitida.");
            Console.WriteLine("Resultado: Nenhuma infração foi cometida.");
        }

        Console.WriteLine("===========================================");
        Console.WriteLine("Sistema finalizado.");
    }

    static double LerVelocidade(string mensagem)
    {
        double valor;
        while (true)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine();

            if (double.TryParse(entrada, out valor) && valor > 0)
                return valor;

            Console.WriteLine("Entrada inválida. Por favor, digite um valor numérico positivo.\n");
        }
    }
}
