// See https://aka.ms/new-console-template for more information
using System;
using System.Globalization;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Define a cultura brasileira (interpreta vírgula como separador decimal)
        CultureInfo culturaBR = new CultureInfo("pt-BR");

        // Listas para armazenar os valores de entradas e saídas de caixa
        List<decimal> entradas = new List<decimal>();
        List<decimal> saidas = new List<decimal>();

        // Variável para armazenar a opção do usuário
        string opcao;

        Console.WriteLine("Bem-vindo ao controle de caixa do restaurante!");

        // Laço principal do menu
        do
        {
            Console.WriteLine("\nInforme o tipo de movimentação:");
            Console.WriteLine("1 - Entrada");
            Console.WriteLine("2 - Saída");
            Console.WriteLine("0 - Calcular saldo e sair");
            Console.Write("Opção: ");
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o valor da entrada: R$ ");
                    string entradaTexto = Console.ReadLine()
                        .Replace(".", ","); // Corrige ponto para vírgula

                    if (decimal.TryParse(entradaTexto, NumberStyles.Number, culturaBR, out decimal valorEntrada))
                    {
                        if (valorEntrada < 0)
                        {
                            Console.WriteLine("\n❌ Valor negativo não permitido. Tente novamente.");
                        }
                        else
                        {
                            entradas.Add(valorEntrada);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ Valor inválido. Tente novamente.");
                    }
                    break;

                case "2":
                    Console.Write("Digite o valor da saída: R$ ");
                    string saidaTexto = Console.ReadLine()
                        .Replace(".", ","); // Corrige ponto para vírgula

                    if (decimal.TryParse(saidaTexto, NumberStyles.Number, culturaBR, out decimal valorSaida))
                    {
                        if (valorSaida < 0)
                        {
                            Console.WriteLine("\n❌ Valor negativo não permitido. Tente novamente.");
                        }
                        else
                        {
                            saidas.Add(valorSaida);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ Valor inválido. Tente novamente.");
                    }
                    break;

                case "0":
                    Console.WriteLine("\nCalculando saldo final...");
                    break;

                default:
                    Console.WriteLine("\n⚠️  Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != "0");

        // Calcula os totais
        decimal totalEntradas = SomarValores(entradas);
        decimal totalSaidas = SomarValores(saidas);
        decimal saldoFinal = totalEntradas - totalSaidas;

        // Mostra os resultados com moeda brasileira
        Console.WriteLine("\nTotal de Entradas: " + totalEntradas.ToString("C", culturaBR));
        Console.WriteLine("Total de Saídas:   " + totalSaidas.ToString("C", culturaBR));
        Console.WriteLine("Saldo Final:       " + saldoFinal.ToString("C", culturaBR));

        // Avaliação do saldo
        if (saldoFinal > 0)
            Console.WriteLine("✅ O dia foi POSITIVO.");
        else if (saldoFinal < 0)
            Console.WriteLine("🔻 O dia foi NEGATIVO.");
        else
            Console.WriteLine("\n⚖️  O dia terminou NEUTRO.");

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    // Função que soma os valores de uma lista
    static decimal SomarValores(List<decimal> valores)
    {
        decimal total = 0;
        foreach (var valor in valores)
        {
            total += valor;
        }
        return total;
    }
}


