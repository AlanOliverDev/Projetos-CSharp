using MySql.Data.MySqlClient;
  using Spotifei;
  using System;
  using System.Collections.Generic;

  public class PlaylistDAO
  {
      private readonly Connection connection = new Connection();

      public void AdicionarPlaylist(Playlist playlist)
      {
          using (var conexao = connection.AbrirConexao())
          {
              string query = @"
                  INSERT INTO playlist_tb (nome_playlist, data_criacao, perfil_usuario_tb_id_perfil)
                  VALUES (@nome, @dataCriacao, @perfilId)";
              using (var cmd = new MySqlCommand(query, conexao))
              {
                  cmd.Parameters.AddWithValue("@nome", playlist.Nome);
                  cmd.Parameters.AddWithValue("@dataCriacao", playlist.DataCriacao);
                  cmd.Parameters.AddWithValue("@perfilId", playlist.Perfil.Id);

                  try
                  {
                      cmd.ExecuteNonQuery();
                      playlist.Id = (int)cmd.LastInsertedId;
                  }
                  catch (MySqlException ex)
                  {
                      throw new Exception($"Erro ao inserir playlist: {ex.Message}", ex);
                  }
              }
          }
      }

      public List<Playlist> ObterPlaylistsPorPerfil(int perfilId)
      {
          var playlists = new List<Playlist>();
          using (var conexao = connection.AbrirConexao())
          {
              string query = "SELECT * FROM playlist_tb WHERE perfil_usuario_tb_id_perfil = @perfilId";
              using (var cmd = new MySqlCommand(query, conexao))
              {
                  cmd.Parameters.AddWithValue("@perfilId", perfilId);
                  using (var reader = cmd.ExecuteReader())
                  {
                      while (reader.Read())
                      {
                          playlists.Add(new Playlist
                          {
                              Id = reader.GetInt32("id_playlist"),
                              Nome = reader.GetString("nome_playlist"),
                              DataCriacao = reader.GetDateTime("data_criacao")
                              // Perfil será carregado separadamente se necessário
                          });
                      }
                  }
              }
          }
          return playlists;
      }
  }