using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class AdminDAO
{
    private readonly string connectionString = "Server=localhost;Port=3306;Database=sga;Uid=root;Pwd=admin"; // adicione o login & senha que vc criou no seu Mysql para logar ao banco de dados 

    // Função para gerar hash SHA-256
    private string GerarHashSHA256(string senha)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public List<Admin> ListarTodos()
    {
        List<Admin> admins = new List<Admin>();
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "SELECT id, login, senha FROM admins ORDER BY id ASC"; // Adicionado ORDER BY id ASC
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string? login = reader["login"].ToString();
                    string? senha = reader["senha"].ToString();
                    if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
                    {
                        admins.Add(new Admin
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Login = login,
                            Senha = senha
                        });
                    }
                }
            }
        }
        return admins;
    }

    public Admin? BuscarPorLoginESenha(string login, string senha)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
        {
            return null;
        }

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "SELECT id, login, senha FROM admins WHERE login = @login AND senha = @senha";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha", GerarHashSHA256(senha));
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string? loginResult = reader["login"].ToString();
                    string? senhaResult = reader["senha"].ToString();
                    if (!string.IsNullOrEmpty(loginResult) && !string.IsNullOrEmpty(senhaResult))
                    {
                        return new Admin
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Login = loginResult,
                            Senha = senhaResult
                        };
                    }
                }
            }
        }
        return null;
    }

    public void Adicionar(Admin admin)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "INSERT INTO admins (login, senha) VALUES (@login, @senha)";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@login", admin.Login);
            cmd.Parameters.AddWithValue("@senha", GerarHashSHA256(admin.Senha));
            try
            {
                cmd.ExecuteNonQuery();
                admin.Id = (int)cmd.LastInsertedId;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                throw new Exception("Erro: Login já existe no sistema.");
            }
        }
    }

    public void AlterarSenha(string login, string novaSenha)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(novaSenha))
        {
            throw new ArgumentException("Login e nova senha não podem ser nulos ou vazios.");
        }

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "UPDATE admins SET senha = @senha WHERE login = @login";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@senha", GerarHashSHA256(novaSenha));
            cmd.Parameters.AddWithValue("@login", login);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new Exception("Admin não encontrado.");
            }
        }
    }

    public void Remover(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new ArgumentException("Login não pode ser nulo ou vazio.");
        }

        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string query = "DELETE FROM admins WHERE login = @login";
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@login", login);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new Exception("Admin não encontrado.");
            }
        }
    }
}