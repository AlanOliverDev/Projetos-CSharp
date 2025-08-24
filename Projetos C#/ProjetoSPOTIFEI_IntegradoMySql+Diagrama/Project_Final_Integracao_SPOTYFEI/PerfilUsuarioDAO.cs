using MySql.Data.MySqlClient;
  using Spotifei;
  using System;

  public class PerfilUsuarioDAO
  {
      private readonly Connection connection = new Connection();

      public void AdicionarPerfil(PerfilUsuario perfil)
      {
          using (var conexao = connection.AbrirConexao())
          {
              string query = @"
                  INSERT INTO perfil_usuario_tb (nome, usuario_tb_id_usuarios)
                  VALUES (@nome, @usuarioId)";
              using (var cmd = new MySqlCommand(query, conexao))
              {
                  cmd.Parameters.AddWithValue("@nome", perfil.Nome);
                  cmd.Parameters.AddWithValue("@usuarioId", perfil.Usuario.Id);

                  try
                  {
                      cmd.ExecuteNonQuery();
                      perfil.Id = (int)cmd.LastInsertedId;
                  }
                  catch (MySqlException ex)
                  {
                      throw new Exception($"Erro ao inserir perfil: {ex.Message}", ex);
                  }
              }
          }
      }
  }