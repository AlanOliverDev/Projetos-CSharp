using MySql.Data.MySqlClient;
using System;

public class Connection
{
    private static readonly string connectionString = "Server=localhost;Port=3306;Database=sga;Uid=root;Pwd=admin"; // adicione o login & senha que vc criou no seu Mysql para logar ao banco de dados 

    public static string ConnectionString => connectionString;

    public MySqlConnection AbrirConexao()
    {
        var conexao = new MySqlConnection(connectionString);
        try
        {
            conexao.Open();
            return conexao;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar com o banco: " + ex.Message);
            throw;
        }
    }
}