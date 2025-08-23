using System;

class CarroAutonomo
{
    // Propriedades do carro
    public string Modelo { get; set; }
    public bool MotoristaPresente { get; set; }
    public double VelocidadeAtual { get; set; }
    public double Bateria { get; set; }
    public bool PilotoAutomaticoAtivo { get; set; }
    public bool CarroLigado { get; set; }
    private int marchaAtual;
    private bool wifiConectado;
    public bool DirecaoManualAtiva { get; private set; }

    const double VelocidadeMaxima = 250;
    const double VelocidadeMinima = 0;

    public CarroAutonomo(string modelo)
    {
        Modelo = modelo;
        MotoristaPresente = false;
        VelocidadeAtual = 0;
        Bateria = 100;
        PilotoAutomaticoAtivo = false;
        CarroLigado = false;
        marchaAtual = 0;
        wifiConectado = true; // Simula conexão Wi-Fi
        DirecaoManualAtiva = false;
    }

    // Métodos para consumo e descarregamento da bateria
    public void ConsumirBateria()
    {
        Bateria -= 1;
        if (Bateria < 0) Bateria = 0;
    }

    public void DescarregarBateria()
    {
        if (CarroLigado)
        {
            if (VelocidadeAtual > 0)
            {
                Bateria -= 0.5;
                if (Bateria < 0) Bateria = 0;
                Console.WriteLine($"Bateria em {Bateria}%");
            }
            else if (Bateria < 100)
            {
                Bateria += 0.1;
                if (Bateria > 100) Bateria = 100;
            }
        }
    }

   public void RecarregarBateria()
{
    if (Bateria == 100)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🔋 A bateria já está 100% carregada. Não é necessário recarregar.");
        Console.ResetColor();
        return;
    }else  if (VelocidadeAtual > 0) {
     
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("⚠️ Não é possível recarregar a bateria enquanto o veículo estiver em movimento.");
        Console.ResetColor();
        return;
    }

   
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Iniciando recarga da bateria a partir de {Bateria}%...");
    Console.ResetColor();

    int totalBarras = 20;
    double cargaInicial = Bateria;
    double cargaFinal = 100;
    double incremento = (cargaFinal - cargaInicial) / totalBarras;
    bool brilho = true; // Efeito de brilho na barra

    for (int i = 0; i <= totalBarras; i++)
    {
        System.Threading.Thread.Sleep(300);  // Pequeno delay

        double porcentagemAtual = cargaInicial + (incremento * i);
        if (porcentagemAtual > 100) porcentagemAtual = 100;

        int barrasCheias = (int)(porcentagemAtual / 5);

        // Alterna o símbolo para simular "brilho"
        char simbolo = brilho ? '#' : '=';
        brilho = !brilho; // Troca a cada atualização

        string barra = "[" + new string(simbolo, barrasCheias) + new string('-', totalBarras - barrasCheias) + "]";

        // Escolhe a cor conforme o nível da bateria
        if (porcentagemAtual <= 30)
            Console.ForegroundColor = ConsoleColor.Red;
        else if (porcentagemAtual <= 70)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Green;

        // Atualiza sempre na mesma linha
        Console.Write($"\r{barra} {porcentagemAtual:F0}%⚡");

        Console.ResetColor(); // Sempre reseta para não vazar cor
    }

    Bateria = 100;

    // Mensagem de finalização em azul
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n\n🔋 Bateria totalmente recarregada com sucesso!\n");
    Console.ResetColor();
}

    // Métodos para controle do carro
    public void EntrarNoVeiculo()
    {
        if (MotoristaPresente)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("👤 O motorista já está no veículo.");
            Console.ResetColor();
            return;
        }

        if (VelocidadeAtual > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠️ Não é possível entrar no veículo enquanto ele está em movimento! Aguarde ele parar completamente.");
            Console.ResetColor();
            return;
        }

        MotoristaPresente = true;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🚗 O motorista entrou no veículo.");
        Console.ResetColor();

        if (!MotoristaPresente)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("🚪 O motorista já está fora do veículo.");
            Console.ResetColor();
            return;
        }

        if (VelocidadeAtual > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠️ Não é seguro sair do veículo enquanto ele está em movimento! Por favor, pare o carro antes de sair.");
            Console.ResetColor();
            return;
        }
    }

    

    public void SairDoVeiculo()
    
    {
         MotoristaPresente = true;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🚗 O motorista saiu do veículo.");
        Console.ResetColor();
    }

    public void Ligar()
    {
        if (Bateria <= 5)
        {
            Console.WriteLine("Bateria muito baixa! Não é possível ligar o veículo.");
            return;
        }

        CarroLigado = true;

        if (MotoristaPresente)
            Console.WriteLine($"{Modelo} foi ligado manualmente.");
        else
            Console.WriteLine($"{Modelo} foi ligado remotamente via aplicativo Tesla.");
    }

    public void Desligar()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro já está desligado.");
            return;
        }

        CarroLigado = false;
        VelocidadeAtual = 0;
        marchaAtual = 0;
        Console.WriteLine($"{Modelo} foi desligado.");
    }

    public void Dirigir()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro precisa estar ligado para dirigir.");
            return;
        }

        if (PilotoAutomaticoAtivo)
        {
            Console.WriteLine("O piloto automático está ativado. Para dirigir manualmente, desative o piloto automático.");
            return;
        }

        if (MotoristaPresente)
        {
            DirecaoManualAtiva = true;
            Console.WriteLine("Direção manual ativada. Você pode controlar a velocidade e frenagem.");
        }
        else
        {
            Console.WriteLine("É necessário que o motorista esteja presente para controlar o veículo manualmente.");
        }
    }

    public void AumentarVelocidade()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro precisa estar ligado para aumentar a velocidade.");
            return;
        }

        if (!MotoristaPresente && !PilotoAutomaticoAtivo)
        {
            Console.WriteLine("Não há motorista presente e o piloto automático não está ativado. Não é possível aumentar a velocidade.");
            return;
        }

        if (DirecaoManualAtiva)
        {
            if (VelocidadeAtual < 30)
            {
                marchaAtual = 1;
                VelocidadeAtual = 30;
            }
            else if (VelocidadeAtual < 60)
            {
                marchaAtual = 2;
                VelocidadeAtual = 60;
            }
            else if (VelocidadeAtual < 100)
            {
                marchaAtual = 3;
                VelocidadeAtual = 100;
            }
            else if (VelocidadeAtual < 160)
            {
                marchaAtual = 4;
                VelocidadeAtual = 160;
            }
            else if (VelocidadeAtual < VelocidadeMaxima)
            {
                marchaAtual = 5;
                VelocidadeAtual = VelocidadeMaxima;
            }

            Console.WriteLine($"Direção manual ativada: Velocidade aumentada para {VelocidadeAtual} km/h (Marcha {marchaAtual}).");
        }
        else
        {
            Console.WriteLine("O piloto automático está ativado. A velocidade está sendo controlada automaticamente.");
        }

        ConsumirBateria();
        DescarregarBateria();
    }

    public void DiminuirVelocidade()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro precisa estar ligado para diminuir a velocidade.");
            return;
        }

        if (!MotoristaPresente && !PilotoAutomaticoAtivo)
        {
            Console.WriteLine("Não há motorista presente e o piloto automático não está ativado. Não é possível diminuir a velocidade.");
            return;
        }

        if (DirecaoManualAtiva)
        {
            if (VelocidadeAtual > 160)
            {
                marchaAtual = 4;
                VelocidadeAtual = 160;
            }
            else if (VelocidadeAtual > 100)
            {
                marchaAtual = 3;
                VelocidadeAtual = 100;
            }
            else if (VelocidadeAtual > 60)
            {
                marchaAtual = 2;
                VelocidadeAtual = 60;
            }
            else if (VelocidadeAtual > 30)
            {
                marchaAtual = 1;
                VelocidadeAtual = 30;
            }
            else
            {
                marchaAtual = 0;
                VelocidadeAtual = 0;
            }

            Console.WriteLine($"Direção manual ativada: Velocidade reduzida para {VelocidadeAtual} km/h (Marcha {marchaAtual}).");
        }
        else
        {
            Console.WriteLine("O piloto automático está ativado. A velocidade está sendo controlada automaticamente.");
        }

        ConsumirBateria();
        DescarregarBateria();
    }

    public void FrearVeiculo()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro precisa estar ligado para frear.");
            return;
        }

        if (!MotoristaPresente && !PilotoAutomaticoAtivo)
        {
            Console.WriteLine("Não há motorista presente e o piloto automático não está ativado. Não é possível frear.");
            return;
        }

        if (DirecaoManualAtiva)
        {
            Console.WriteLine("Iniciando frenagem manual...");
            while (VelocidadeAtual > 0)
            {
                VelocidadeAtual -= 10;
                if (VelocidadeAtual < 0) VelocidadeAtual = 0;
                System.Threading.Thread.Sleep(300);
                Console.WriteLine($"Velocidade atual: {VelocidadeAtual} km/h");
            }
            marchaAtual = 0;
            Console.WriteLine("O carro parou completamente.");
        }
        else
        {
            Console.WriteLine("O piloto automático está ativado. A frenagem está sendo controlada automaticamente.");
        }

        ConsumirBateria();
        DescarregarBateria();
    }

    public void EstacionarAutomaticamente()
    {
        if (!CarroLigado)
        {
            Console.WriteLine("O carro precisa estar ligado para estacionar.");
            return;
        }

        if (VelocidadeAtual != 0)
        {
            Console.WriteLine("Pare o carro antes de estacionar.");
            return;
        }

        if (!MotoristaPresente && !wifiConectado)
        {
            Console.WriteLine("Não foi possível estacionar. Conexão Wi-Fi ausente.");
            return;
        }

        Console.WriteLine(MotoristaPresente
            ? "Iniciando estacionamento automático..."
            : "Iniciando estacionamento via App Tesla...");

        for (int i = 0; i <= 100; i += 10)
        {
            Console.Write("█");
            System.Threading.Thread.Sleep(200);
        }

        Console.WriteLine("\nCarro estacionado com sucesso!");
    }

    public void AtualizarSistema()
    {
        if (Bateria <= 10)
        {
            Console.WriteLine("Bateria muito baixa para atualizar o sistema.");
            return;
        }

        Console.WriteLine("Atualizando sistema...");

        // Simula tempo de atualização com mensagens
        System.Threading.Thread.Sleep(500);  
        Console.WriteLine("🔧 Iniciando a atualização do sistema...");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("🌐 Baixando novos dados de tráfego.");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("🚗 Carregando mapas e otimizando rotas.");
        System.Threading.Thread.Sleep(1500);
        Console.WriteLine("🛠️ Atualizando sensores de segurança.");
        System.Threading.Thread.Sleep(2000);
        Console.WriteLine("✅ Sistema atualizado com sucesso!");
    }

    public void AtivarPilotoAutomatico()
    {
        if (PilotoAutomaticoAtivo)
        {
            Console.WriteLine("O piloto automático já está ativado.");
            return;
        }

        PilotoAutomaticoAtivo = true;
        Console.WriteLine("Piloto automático ativado. O carro agora está no controle automático.");
    }

    public void DesativarPilotoAutomatico()
    {
        if (!PilotoAutomaticoAtivo)
        {
            Console.WriteLine("O piloto automático não está ativado.");
            return;
        }

        PilotoAutomaticoAtivo = false;
        Console.WriteLine("Piloto automático desativado. O controle voltou para o motorista.");
    }

    public void VerificarSensores()
    {
        Console.WriteLine("Verificando sensores...");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("✔️ Todos os sensores estão funcionando corretamente.");
    }

    // Método que mostra o status completo do veículo
    public void MostrarStatus()
    {
        Console.WriteLine("\n=== Status do Veículo ===");
        Console.WriteLine($"Modelo: {Modelo}");
        Console.WriteLine($"Carro Ligado: {(CarroLigado ? "Sim" : "Não")}");
        Console.WriteLine($"Motorista Presente: {(MotoristaPresente ? "Sim" : "Não")}");
        Console.WriteLine($"Velocidade Atual: {VelocidadeAtual} km/h");
        Console.WriteLine($"Marcha Atual: {(marchaAtual > 0 ? $"Marcha {marchaAtual}" : "Neutro")}");
        Console.WriteLine($"Bateria: {Bateria}%");
        Console.WriteLine($"Piloto Automático Ativo: {(PilotoAutomaticoAtivo ? "Sim" : "Não")}");
        Console.WriteLine($"Direção Manual Ativa: {(DirecaoManualAtiva ? "Sim" : "Não")}");
        Console.WriteLine($"Wi-Fi Conectado: {(wifiConectado ? "Sim" : "Não")}");
        Console.WriteLine($"Estado do Veículo: {(VelocidadeAtual == 0 ? "Parado" : "Em Movimento")}");
    }
}

class Program
{
    static void Main()
    {
        CarroAutonomo meuCarro = new CarroAutonomo("Tesla Model X");

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("=== Menu do Carro Autônomo ===");
            Console.WriteLine("1. Ligar o Carro");
            Console.WriteLine("2. Desligar o Carro");
            Console.WriteLine("3. Entrar no Carro");
            Console.WriteLine("4. Sair do Carro");
            Console.WriteLine("5. Dirigir");
            Console.WriteLine("6. Aumentar Velocidade");
            Console.WriteLine("7. Diminuir Velocidade");
            Console.WriteLine("8. Frear Veículo");
            Console.WriteLine("9. Estacionar Automaticamente");
            Console.WriteLine("10. Atualizar Sistema");
            Console.WriteLine("11. Ativar Piloto Automático");
            Console.WriteLine("12. Desativar Piloto Automático");
            Console.WriteLine("13. Verificar Sensores");
            Console.WriteLine("14. Recarregar Bateria");
            Console.WriteLine("15. Sair");
            Console.WriteLine("16. Mostrar Status Detalhado do Veículo");  // Nova opção
            Console.Write("\nDigite uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    meuCarro.Ligar();
                    break;
                case "2":
                    meuCarro.Desligar();
                    break;
                case "3":
                    meuCarro.EntrarNoVeiculo();
                    break;
                case "4":
                    meuCarro.SairDoVeiculo();
                    break;
                case "5":
                    meuCarro.Dirigir();
                    break;
                case "6":
                    meuCarro.AumentarVelocidade();
                    break;
                case "7":
                    meuCarro.DiminuirVelocidade();
                    break;
                case "8":
                    meuCarro.FrearVeiculo();
                    break;
                case "9":
                    meuCarro.EstacionarAutomaticamente();
                    break;
                case "10":
                    meuCarro.AtualizarSistema();
                    break;
                case "11":
                    meuCarro.AtivarPilotoAutomatico();
                    break;
                case "12":
                    meuCarro.DesativarPilotoAutomatico();
                    break;
                case "13":
                    meuCarro.VerificarSensores();
                    break;
                case "14":
                    meuCarro.RecarregarBateria();
                    break;
                case "15":
                    Console.WriteLine("Finalizando o programa...");
                    continuar = false;
                    break;
                case "16":  // Novo caso para mostrar o status
                    meuCarro.MostrarStatus();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }
    }
}
