class Program
{
    static void Main(string[] args)
    {
        Carro carroTesla = new Carro();
        Motorista motorista = new Motorista("Alan", 26, "AB", carroTesla);

        motorista.AssumirControleManual();
        motorista.Dirigir();
        motorista.MostrarStatusDoCarro();
        motorista.AtivarModoAutonomo();
        motorista.MostrarStatusDoCarro();
    }
}
