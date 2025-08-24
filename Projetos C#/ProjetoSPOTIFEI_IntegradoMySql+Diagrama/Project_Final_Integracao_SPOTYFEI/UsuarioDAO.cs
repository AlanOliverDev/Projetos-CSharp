using MySql.Data.MySqlClient;
  using Spotifei;
  using System;
  using System.Collections.Generic;

  public class UsuarioDAO
  {
      private readonly Connection connection = new Connection();

      public void AdicionarUsuario(Usuario usuario)
      {
          using (var conexao = connection.AbrirConexao())
          {
              string query = @"
                  INSERT INTO usuario_tb (nome, email, data_nascimento, status, data_cadastro, pais, estado, genero, senha, plano_assinatura_tb_id_plano)
                  VALUES (@nome, @email, @dataNascimento, @status, @dataCadastro, @pais, @estado, @genero, @senha, @planoId)";
              using (var cmd = new MySqlCommand(query, conexao))
              {
                  cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                  cmd.Parameters.AddWithValue("@email", usuario.Email);
                  cmd.Parameters.AddWithValue("@dataNascimento", usuario.DataNascimento);
                  cmd.Parameters.AddWithValue("@status", usuario.Status);
                  cmd.Parameters.AddWithValue("@dataCadastro", usuario.DataCadastro);
                  cmd.Parameters.AddWithValue("@pais", usuario.Pais);
                  cmd.Parameters.AddWithValue("@estado", usuario.Estado ?? (object)DBNull.Value); // Ajuste explícito
                  cmd.Parameters.AddWithValue("@genero", usuario.Genero ?? (object)DBNull.Value); // Ajuste explícito
                  cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                  cmd.Parameters.AddWithValue("@planoId", usuario.Plano.Id > 0 ? (object)usuario.Plano.Id : DBNull.Value);

                  try
                  {
                      cmd.ExecuteNonQuery();
                      usuario.Id = (int)cmd.LastInsertedId;
                  }
                  catch (MySqlException ex)
                  {
                      throw new Exception($"Erro ao inserir usuário: {ex.Message}", ex);
                  }
              }
          }
      }

      public Usuario? ObterUsuarioPorEmail(string email)
      {
          using (var conexao = connection.AbrirConexao())
          {
              string query = "SELECT * FROM usuario_tb WHERE email = @email";
              using (var cmd = new MySqlCommand(query, conexao))
              {
                  cmd.Parameters.AddWithValue("@email", email);
                  using (var reader = cmd.ExecuteReader())
                  {
                      if (reader.Read())
                      {
                          return new Usuario
                          {
                              Id = reader.GetInt32("id_usuarios"),
                              Nome = reader.GetString("nome"),
                              Email = reader.GetString("email"),
                              DataNascimento = reader.GetDateTime("data_nascimento"),
                              Status = reader.GetString("status"),
                              DataCadastro = reader.GetDateTime("data_cadastro"),
                              Pais = reader.GetString("pais"),
                              Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? null : reader.GetString("estado"),
                              Genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? null : reader.GetString("genero"),
                              Senha = reader.GetString("senha"),
                              Plano = new PlanoAssinatura { Id = reader.IsDBNull(reader.GetOrdinal("plano_assinatura_tb_id_plano")) ? 0 : reader.GetInt32("plano_assinatura_tb_id_plano") }
                          };
                      }
                      return null;
                  }
              }
          }
      }
  }