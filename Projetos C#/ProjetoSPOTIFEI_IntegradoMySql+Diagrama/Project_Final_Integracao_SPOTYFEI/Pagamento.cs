using System;

namespace Spotifei
{
    public class Pagamento
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = new();

        public string GerarRecibo() =>
            $"Pagamento de R${Valor} - {Status} em {DataPagamento.ToShortDateString()}";
    }
}
