using System;
using System.Collections.Generic;

namespace ZooGerenciamento;

public class Zoologico
{
    private ZoologicoDAO dao = new ZoologicoDAO();

    public void AdicionarAnimal(Animal animal)
    {
        dao.AdicionarAnimal(animal);
    }

    public void ListarAnimais()
    {
        List<Animal> animais = dao.ObterTodosAnimais();
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum animal cadastrado.");
            return;
        }
        foreach (var animal in animais)
        {
            Console.WriteLine(animal.ExibirInformacoes());
        }
    }

    public void AtualizarAnimal(Animal animal)
    {
        dao.AtualizarAnimal(animal);
    }

    public void RemoverAnimal(int id)
    {
        dao.RemoverAnimal(id);
    }

    public Animal ObterAnimalPorId(int id)
    {
        return dao.ObterAnimalPorId(id);
    }
}