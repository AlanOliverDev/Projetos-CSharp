// Spotifei/Services/PagamentoService.cs
namespace Spotifei
{


    public class PagamentoService
    {
        public void ProcessarPagamento(Pagamento pagamento)
        {
            pagamento.Status = "concluido";
            pagamento.DataPagamento = DateTime.Now;
            Console.WriteLine($"Pagamento processado: R${pagamento.Valor}");
        }
    }
}