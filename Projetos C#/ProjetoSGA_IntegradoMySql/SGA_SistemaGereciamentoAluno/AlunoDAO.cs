using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class AlunoDAO
{
    private readonly string connectionString;

    public AlunoDAO()
    {
        connectionString = Connection.ConnectionString;
    }

    public void Adicionar(Aluno aluno)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "INSERT INTO alunos (matricula, nomeCompleto, idade, cpf, endereco) VALUES (@matricula, @nomeCompleto, @idade, @cpf, @endereco)";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
            cmd.Parameters.AddWithValue("@nomeCompleto", aluno.NomeCompleto);
            cmd.Parameters.AddWithValue("@idade", aluno.Idade);
            cmd.Parameters.AddWithValue("@cpf", aluno.Cpf ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@endereco", aluno.Endereco ?? (object)DBNull.Value);
            try
            {
                cmd.ExecuteNonQuery();
                aluno.Id = (int)cmd.LastInsertedId;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                string message = ex.Message.Contains("matricula") ? "Matrícula já existe no sistema." : "CPF já existe no sistema.";
                throw new Exception(message);
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erro no banco de dados: {ex.Message}");
            }
        }
    }

    public List<Aluno> ObterTodos()
    {
        List<Aluno> alunos = new List<Aluno>();

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "SELECT id, matricula, nomeCompleto, idade, cpf, endereco FROM alunos ORDER BY matricula ASC"; // Alterado para ORDER BY matricula ASC
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Aluno aluno = new Aluno
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Matricula = reader["matricula"].ToString() ?? string.Empty,
                        NomeCompleto = reader["nomeCompleto"].ToString() ?? string.Empty,
                        Idade = reader.IsDBNull(reader.GetOrdinal("idade")) ? 0 : Convert.ToInt32(reader["idade"]),
                        Cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader["cpf"].ToString(),
                        Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader["endereco"].ToString()
                    };
                    alunos.Add(aluno);
                }
            }
        }
        return alunos;
    }

    public Aluno? ObterPorMatricula(string matricula)
    {
        if (string.IsNullOrEmpty(matricula))
        {
            return null;
        }

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "SELECT id, matricula, nomeCompleto, idade, cpf, endereco FROM alunos WHERE matricula = @matricula";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@matricula", matricula);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Aluno
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Matricula = reader["matricula"].ToString() ?? string.Empty,
                        NomeCompleto = reader["nomeCompleto"].ToString() ?? string.Empty,
                        Idade = reader.IsDBNull(reader.GetOrdinal("idade")) ? 0 : Convert.ToInt32(reader["idade"]),
                        Cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader["cpf"].ToString(),
                        Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader["endereco"].ToString()
                    };
                }
            }
        }
        return null;
    }

    public Aluno? BuscarPorMatriculaECpf(string? matricula = null, string? cpf = null)
    {
        if (string.IsNullOrEmpty(matricula) && string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("Pelo menos um dos campos (matrícula ou CPF) deve ser fornecido.");
        }
        if (!string.IsNullOrEmpty(matricula) && !string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("Forneça apenas matrícula ou CPF, não ambos.");
        }

        using (var conexao = new MySqlConnection(connectionString))
        {
            try
            {
                conexao.Open();
                string query = string.IsNullOrEmpty(matricula)
                    ? "SELECT id, matricula, nomeCompleto, idade, cpf, endereco FROM alunos WHERE cpf = @cpf"
                    : "SELECT id, matricula, nomeCompleto, idade, cpf, endereco FROM alunos WHERE matricula = @matricula";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                if (string.IsNullOrEmpty(matricula))
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                else
                    cmd.Parameters.AddWithValue("@matricula", matricula);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Aluno
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Matricula = reader["matricula"].ToString() ?? string.Empty,
                            NomeCompleto = reader["nomeCompleto"].ToString() ?? string.Empty,
                            Idade = reader.IsDBNull(reader.GetOrdinal("idade")) ? 0 : Convert.ToInt32(reader["idade"]),
                            Cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader["cpf"].ToString(),
                            Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader["endereco"].ToString()
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erro no banco de dados: {ex.Message}");
            }
        }
        return null;
    }

    public void Alterar(Aluno aluno)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "UPDATE alunos SET matricula = @matricula, nomeCompleto = @nomeCompleto, idade = @idade, cpf = @cpf, endereco = @endereco WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
            cmd.Parameters.AddWithValue("@nomeCompleto", aluno.NomeCompleto);
            cmd.Parameters.AddWithValue("@idade", aluno.Idade);
            cmd.Parameters.AddWithValue("@cpf", aluno.Cpf ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@endereco", aluno.Endereco ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", aluno.Id);
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Aluno não encontrado.");
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                throw new Exception("Erro: Matrícula já existe no sistema.");
            }
        }
    }

    public void Remover(Aluno aluno)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "DELETE FROM alunos WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@id", aluno.Id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new Exception("Aluno não encontrado.");
            }
        }
    }
}