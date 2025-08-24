using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ZooGerenciamento;

public class ZoologicoDAO
{
    private const string ConnectionString = "Server=localhost;Database=zoologico;Uid=root;Pwd=admin;Charset=utf8mb4;"; // adicione o login & senha que vc criou no seu Mysql para logar ao banco de dados 

    public void AdicionarAnimal(Animal animal)
    {
        using (var conexao = new MySqlConnection(ConnectionString))
        {
            try
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    Console.WriteLine($"[DEBUG] Enviando ao banco: Nome='{animal.Nome}', Espécie='{animal.Especie}'");
                    string sql = "INSERT INTO animais (nome, idade, especie, tipo) VALUES (@nome, @idade, @especie, @tipo); SELECT LAST_INSERT_ID();";
                    using (var comando = new MySqlCommand(sql, conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@nome", animal.Nome ?? string.Empty);
                        comando.Parameters.AddWithValue("@idade", animal.Idade);
                        comando.Parameters.AddWithValue("@especie", animal.Especie ?? string.Empty);
                        comando.Parameters.AddWithValue("@tipo", animal.GetType().Name);
                        int id = Convert.ToInt32(comando.ExecuteScalar());
                        animal.SetID(id);
                    }

                    if (animal is Mamifero mamifero)
                    {
                        string sqlMamifero = "INSERT INTO mamiferos (animal_id, tem_pelo, habitat) VALUES (@id, @temPelo, @habitat)";
                        using (var comando = new MySqlCommand(sqlMamifero, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            comando.Parameters.AddWithValue("@temPelo", mamifero.TemPelo);
                            comando.Parameters.AddWithValue("@habitat", mamifero.Habitat ?? string.Empty);
                            comando.ExecuteNonQuery();
                        }
                    }
                    else if (animal is Reptil reptil)
                    {
                        string sqlReptil = "INSERT INTO repteis (animal_id, tem_escamas, temperatura_corporal) VALUES (@id, @temEscamas, @temperatura)";
                        using (var comando = new MySqlCommand(sqlReptil, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            comando.Parameters.AddWithValue("@temEscamas", reptil.TemEscamas);
                            comando.Parameters.AddWithValue("@temperatura", reptil.TemperaturaCorporal ?? string.Empty);
                            comando.ExecuteNonQuery();
                        }
                    }
                    else if (animal is Ave ave)
                    {
                        string sqlAve = "INSERT INTO aves (animal_id, pode_voar, tamanho_asas) VALUES (@id, @podeVoar, @tamanhoAsas)";
                        using (var comando = new MySqlCommand(sqlAve, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            comando.Parameters.AddWithValue("@podeVoar", ave.PodeVoar);
                            comando.Parameters.AddWithValue("@tamanhoAsas", ave.TamanhoAsas);
                            comando.ExecuteNonQuery();
                        }
                    }

                    transacao.Commit();
                    Console.WriteLine("Animal adicionado com sucesso!");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco ao adicionar animal: {ex.Message} (Código: {ex.Number})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao adicionar animal: {ex.Message}");
            }
        }
    }

    public List<Animal> ObterTodosAnimais()
    {
        var animais = new List<Animal>();
        using (var conexao = new MySqlConnection(ConnectionString))
        {
            try
            {
                conexao.Open();
                string sql = @"
                    SELECT a.id, a.nome, a.idade, a.especie, a.tipo,
                           m.tem_pelo, m.habitat,
                           r.tem_escamas, r.temperatura_corporal,
                           av.pode_voar, av.tamanho_asas
                    FROM animais a
                    LEFT JOIN mamiferos m ON a.id = m.animal_id
                    LEFT JOIN repteis r ON a.id = r.animal_id
                    LEFT JOIN aves av ON a.id = av.animal_id";
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ordId = reader.GetOrdinal("id");
                            int ordNome = reader.GetOrdinal("nome");
                            int ordIdade = reader.GetOrdinal("idade");
                            int ordEspecie = reader.GetOrdinal("especie");
                            int ordTipo = reader.GetOrdinal("tipo");

                            int id = reader.GetInt32(ordId);
                            string nome = reader.GetString(ordNome);
                            int idade = reader.GetInt32(ordIdade);
                            string especie = reader.GetString(ordEspecie);
                            string tipo = reader.GetString(ordTipo);

                            Console.WriteLine($"[DEBUG] Lendo do banco: Nome='{nome}', Espécie='{especie}'");

                            Animal? animal = null;
                            if (tipo == "Mamifero")
                            {
                                int ordTemPelo = reader.GetOrdinal("tem_pelo");
                                int ordHabitat = reader.GetOrdinal("habitat");
                                bool temPelo = reader.IsDBNull(ordTemPelo) ? false : reader.GetBoolean(ordTemPelo);
                                string habitat = reader.IsDBNull(ordHabitat) ? string.Empty : reader.GetString(ordHabitat);
                                animal = new Mamifero(nome, idade, especie, temPelo, habitat);
                            }
                            else if (tipo == "Reptil")
                            {
                                int ordTemEscamas = reader.GetOrdinal("tem_escamas");
                                int ordTemperatura = reader.GetOrdinal("temperatura_corporal");
                                bool temEscamas = reader.IsDBNull(ordTemEscamas) ? false : reader.GetBoolean(ordTemEscamas);
                                string temperatura = reader.IsDBNull(ordTemperatura) ? string.Empty : reader.GetString(ordTemperatura);
                                animal = new Reptil(nome, idade, especie, temEscamas, temperatura);
                            }
                            else if (tipo == "Ave")
                            {
                                int ordPodeVoar = reader.GetOrdinal("pode_voar");
                                int ordTamanhoAsas = reader.GetOrdinal("tamanho_asas");
                                bool podeVoar = reader.IsDBNull(ordPodeVoar) ? false : reader.GetBoolean(ordPodeVoar);
                                double tamanhoAsas = reader.IsDBNull(ordTamanhoAsas) ? 0.0 : reader.GetDouble(ordTamanhoAsas);
                                animal = new Ave(nome, idade, especie, podeVoar, tamanhoAsas);
                            }

                            if (animal != null)
                            {
                                animal.SetID(id);
                                animais.Add(animal);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco ao listar animais: {ex.Message} (Código: {ex.Number})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao listar animais: {ex.Message}");
            }
        }
        return animais;
    }

    public void AtualizarAnimal(Animal animal)
    {
        using (var conexao = new MySqlConnection(ConnectionString))
        {
            try
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    Console.WriteLine($"[DEBUG] Atualizando no banco: Nome='{animal.Nome}', Espécie='{animal.Especie}'");
                    string sqlAnimal = "UPDATE animais SET nome = @nome, idade = @idade, especie = @especie WHERE id = @id";
                    using (var comando = new MySqlCommand(sqlAnimal, conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@nome", animal.Nome ?? string.Empty);
                        comando.Parameters.AddWithValue("@idade", animal.Idade);
                        comando.Parameters.AddWithValue("@especie", animal.Especie ?? string.Empty);
                        comando.Parameters.AddWithValue("@id", animal.ID);
                        int rowsAffected = comando.ExecuteNonQuery();
                        if (rowsAffected == 0) throw new Exception("Animal não encontrado na tabela animais.");
                    }

                    int rowsAffectedSpecific = 0;
                    if (animal is Mamifero mamifero)
                    {
                        string sqlMamifero = "UPDATE mamiferos SET tem_pelo = @temPelo, habitat = @habitat WHERE animal_id = @id";
                        using (var comando = new MySqlCommand(sqlMamifero, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@temPelo", mamifero.TemPelo);
                            comando.Parameters.AddWithValue("@habitat", mamifero.Habitat ?? string.Empty);
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            rowsAffectedSpecific = comando.ExecuteNonQuery();
                        }
                        if (rowsAffectedSpecific == 0)
                        {
                            string sqlInsertMamifero = "INSERT INTO mamiferos (animal_id, tem_pelo, habitat) VALUES (@id, @temPelo, @habitat)";
                            using (var comando = new MySqlCommand(sqlInsertMamifero, conexao, transacao))
                            {
                                comando.Parameters.AddWithValue("@id", animal.ID);
                                comando.Parameters.AddWithValue("@temPelo", mamifero.TemPelo);
                                comando.Parameters.AddWithValue("@habitat", mamifero.Habitat ?? string.Empty);
                                comando.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (animal is Reptil reptil)
                    {
                        string sqlReptil = "UPDATE repteis SET tem_escamas = @temEscamas, temperatura_corporal = @temperatura WHERE animal_id = @id";
                        using (var comando = new MySqlCommand(sqlReptil, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@temEscamas", reptil.TemEscamas);
                            comando.Parameters.AddWithValue("@temperatura", reptil.TemperaturaCorporal ?? string.Empty);
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            rowsAffectedSpecific = comando.ExecuteNonQuery();
                        }
                        if (rowsAffectedSpecific == 0)
                        {
                            string sqlInsertReptil = "INSERT INTO repteis (animal_id, tem_escamas, temperatura_corporal) VALUES (@id, @temEscamas, @temperatura)";
                            using (var comando = new MySqlCommand(sqlInsertReptil, conexao, transacao))
                            {
                                comando.Parameters.AddWithValue("@id", animal.ID);
                                comando.Parameters.AddWithValue("@temEscamas", reptil.TemEscamas);
                                comando.Parameters.AddWithValue("@temperatura", reptil.TemperaturaCorporal ?? string.Empty);
                                comando.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (animal is Ave ave)
                    {
                        string sqlAve = "UPDATE aves SET pode_voar = @podeVoar, tamanho_asas = @tamanhoAsas WHERE animal_id = @id";
                        using (var comando = new MySqlCommand(sqlAve, conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@podeVoar", ave.PodeVoar);
                            comando.Parameters.AddWithValue("@tamanhoAsas", ave.TamanhoAsas);
                            comando.Parameters.AddWithValue("@id", animal.ID);
                            rowsAffectedSpecific = comando.ExecuteNonQuery();
                        }
                        if (rowsAffectedSpecific == 0)
                        {
                            string sqlInsertAve = "INSERT INTO aves (animal_id, pode_voar, tamanho_asas) VALUES (@id, @podeVoar, @tamanhoAsas)";
                            using (var comando = new MySqlCommand(sqlInsertAve, conexao, transacao))
                            {
                                comando.Parameters.AddWithValue("@id", animal.ID);
                                comando.Parameters.AddWithValue("@podeVoar", ave.PodeVoar);
                                comando.Parameters.AddWithValue("@tamanhoAsas", ave.TamanhoAsas);
                                comando.ExecuteNonQuery();
                            }
                        }
                    }

                    transacao.Commit();
                    Console.WriteLine("Animal atualizado com sucesso!");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco ao atualizar animal: {ex.Message} (Código: {ex.Number})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao atualizar animal: {ex.Message}");
            }
        }
    }

    public void RemoverAnimal(int id)
    {
        using (var conexao = new MySqlConnection(ConnectionString))
        {
            try
            {
                conexao.Open();
                string sql = "DELETE FROM animais WHERE id = @id";
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("Animal não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("Animal removido com sucesso!");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco ao remover animal: {ex.Message} (Código: {ex.Number})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao remover animal: {ex.Message}");
            }
        }
    }

    public Animal? ObterAnimalPorId(int id)
    {
        using (var conexao = new MySqlConnection(ConnectionString))
        {
            try
            {
                conexao.Open();
                string sql = @"
                    SELECT a.id, a.nome, a.idade, a.especie, a.tipo,
                           m.tem_pelo, m.habitat,
                           r.tem_escamas, r.temperatura_corporal,
                           av.pode_voar, av.tamanho_asas
                    FROM animais a
                    LEFT JOIN mamiferos m ON a.id = m.animal_id
                    LEFT JOIN repteis r ON a.id = r.animal_id
                    LEFT JOIN aves av ON a.id = av.animal_id
                    WHERE a.id = @id";
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int ordId = reader.GetOrdinal("id");
                            int ordNome = reader.GetOrdinal("nome");
                            int ordIdade = reader.GetOrdinal("idade");
                            int ordEspecie = reader.GetOrdinal("especie");
                            int ordTipo = reader.GetOrdinal("tipo");

                            string nome = reader.GetString(ordNome);
                            int idade = reader.GetInt32(ordIdade);
                            string especie = reader.GetString(ordEspecie);
                            string tipo = reader.GetString(ordTipo);

                            Console.WriteLine($"[DEBUG] Lendo do banco (ObterAnimalPorId): Nome='{nome}', Espécie='{especie}'");

                            Animal? animal = null;
                            if (tipo == "Mamifero")
                            {
                                int ordTemPelo = reader.GetOrdinal("tem_pelo");
                                int ordHabitat = reader.GetOrdinal("habitat");
                                bool temPelo = reader.IsDBNull(ordTemPelo) ? false : reader.GetBoolean(ordTemPelo);
                                string habitat = reader.IsDBNull(ordHabitat) ? string.Empty : reader.GetString(ordHabitat);
                                animal = new Mamifero(nome, idade, especie, temPelo, habitat);
                            }
                            else if (tipo == "Reptil")
                            {
                                int ordTemEscamas = reader.GetOrdinal("tem_escamas");
                                int ordTemperatura = reader.GetOrdinal("temperatura_corporal");
                                bool temEscamas = reader.IsDBNull(ordTemEscamas) ? false : reader.GetBoolean(ordTemEscamas);
                                string temperatura = reader.IsDBNull(ordTemperatura) ? string.Empty : reader.GetString(ordTemperatura);
                                animal = new Reptil(nome, idade, especie, temEscamas, temperatura);
                            }
                            else if (tipo == "Ave")
                            {
                                int ordPodeVoar = reader.GetOrdinal("pode_voar");
                                int ordTamanhoAsas = reader.GetOrdinal("tamanho_asas");
                                bool podeVoar = reader.IsDBNull(ordPodeVoar) ? false : reader.GetBoolean(ordPodeVoar);
                                double tamanhoAsas = reader.IsDBNull(ordTamanhoAsas) ? 0.0 : reader.GetDouble(ordTamanhoAsas);
                                animal = new Ave(nome, idade, especie, podeVoar, tamanhoAsas);
                            }

                            if (animal != null)
                            {
                                animal.SetID(reader.GetInt32(ordId));
                                return animal;
                            }
                        }
                        Console.WriteLine($"Animal com ID {id} não encontrado.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco ao obter animal: {ex.Message} (Código: {ex.Number})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao obter animal: {ex.Message}");
            }
        }
        return null;
    }
}