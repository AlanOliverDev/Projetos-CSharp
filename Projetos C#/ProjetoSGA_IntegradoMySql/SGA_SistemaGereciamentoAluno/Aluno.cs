public class Aluno
{
    public int Id { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty; // Alterado de Nome para NomeCompleto
    public int Idade { get; set; }
    public string? Cpf { get; set; }
    public string? Endereco { get; set; }

    public Aluno() { }

    public Aluno(int id, string matricula, string nomeCompleto, int idade, string? cpf, string? endereco)
    {
        Id = id;
        Matricula = matricula ?? throw new ArgumentNullException(nameof(matricula));
        NomeCompleto = nomeCompleto ?? throw new ArgumentNullException(nameof(nomeCompleto));
        Idade = idade;
        Cpf = cpf;
        Endereco = endereco;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nMatrícula: {Matricula}\nNome Completo: {NomeCompleto}\nIdade: {Idade}\nCPF: {Cpf ?? "Não informado"}\nEndereço: {Endereco ?? "Não informado"}";
    }

    public void ExibirDetalhes()
    {
        Console.WriteLine("----- Dados do Aluno -----");
        Console.WriteLine(ToString());
        Console.WriteLine("--------------------------");
    }
}