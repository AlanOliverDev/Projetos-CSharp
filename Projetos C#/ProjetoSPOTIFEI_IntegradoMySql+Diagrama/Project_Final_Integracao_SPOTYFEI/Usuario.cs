namespace Spotifei
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string? Estado { get; set; } // Alterado para string?
        public string? Genero { get; set; } // Alterado para string?
        public string Senha { get; set; } = string.Empty;
        public PlanoAssinatura Plano { get; set; } = new PlanoAssinatura();
        public List<PerfilUsuario> Perfis { get; set; } = new List<PerfilUsuario>();
    }
}