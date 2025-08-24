namespace ZooGerenciamento;

public abstract class Animal
{
    public int ID { get; private set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Especie { get; set; }

    public Animal(string nome, int idade, string especie)
    {
        Nome = nome;
        Idade = idade;
        Especie = especie;
    }

    public void SetID(int id)
    {
        ID = id;
    }

    public virtual string ExibirInformacoes()
    {
        return $"ID: {ID}, Nome: {Nome}, Idade: {Idade}, Espécie: {Especie}";
    }
}