using MySql.Data.MySqlClient;
  using System;

  public class Connection
  {
      private readonly string connectionString;

      public Connection()
      {
          connectionString = "Server=localhost;Database=spotifei;Uid=root;Pwd=admin;";
          if (string.IsNullOrEmpty(connectionString))
          {
              throw new ArgumentException("A string de conexão não pode ser nula ou vazia.");
          }
      }

      public MySqlConnection AbrirConexao()
      {
          MySqlConnection conexao = new MySqlConnection(connectionString);
          try
          {
              if (conexao.State == System.Data.ConnectionState.Open)
              {
                  return conexao;
              }
              conexao.Open();
              Console.WriteLine("Conexão com o banco Spotifei estabelecida com sucesso!");
              return conexao;
          }
          catch (MySqlException ex)
          {
              switch (ex.Number)
              {
                  case 0:
                      throw new Exception("Não foi possível conectar ao servidor MySQL. Verifique se o servidor está ativo.", ex);
                  case 1045:
                      throw new Exception("Acesso negado. Verifique o usuário e a senha.", ex);
                  case 1049:
                      throw new Exception("O banco de dados 'spotifei' não foi encontrado. Crie o banco antes de prosseguir.", ex);
                  default:
                      throw new Exception($"Erro ao abrir a conexão com o MySQL: {ex.Message}", ex);
              }
          }
          catch (Exception ex)
          {
              throw new Exception($"Erro inesperado ao abrir a conexão: {ex.Message}", ex);
          }
      }

      public void FecharConexao(MySqlConnection conexao)
      {
          if (conexao == null)
          {
              throw new ArgumentNullException(nameof(conexao), "A conexão não pode ser nula.");
          }

          try
          {
              if (conexao.State != System.Data.ConnectionState.Closed)
              {
                  conexao.Close();
                  Console.WriteLine("Conexão com o banco Spotifei fechada com sucesso.");
              }
          }
          catch (MySqlException ex)
          {
              throw new Exception($"Erro ao fechar a conexão com o MySQL: {ex.Message}", ex);
          }
          catch (Exception ex)
          {
              throw new Exception($"Erro inesperado ao fechar a conexão: {ex.Message}", ex);
          }
      }
  }