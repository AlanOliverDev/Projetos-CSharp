// Spotifei/Models/PlanoAssinatura.cs
namespace Spotifei
{
    public class PlanoAssinatura
    {
        public int Id { get; set; }
        public string TipoPlano { get; set; } = string.Empty;
        public decimal PrecoMensal { get; set; }
        public int MaxPerfis { get; set; }
        public string QualidadeAudio { get; set; } = string.Empty;
        public string QuantidadeAnuncios { get; set; } = string.Empty;

        public decimal CalcularPrecoAnual() => PrecoMensal * 12;

        public string ObterBeneficios()
        {
            return $"Plano: {TipoPlano.ToUpper()} - Qualidade: {QualidadeAudio}, Anúncios: {QuantidadeAnuncios}, Perfis: {MaxPerfis}";
        }
    }
}