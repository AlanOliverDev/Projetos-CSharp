using Spotifei;
using System;
using System.Collections.Generic;

namespace Spotifei
{
    public class StreamingService
    {
        private readonly PlaylistDAO playlistDAO = new PlaylistDAO();
        private readonly List<Conteudo> conteudos = new();
        private int playlistIdCounter = 1;

        public StreamingService()
        {
            conteudos.Add(new Musica { Id = 1, Titulo = "Musica A", Album = "Album X", Artista = "Artista Y" });
            conteudos.Add(new Podcast { Id = 2, Titulo = "Podcast B" });
        }

        public void CriarPlaylist(PerfilUsuario perfil, string nomePlaylist)
        {
            try
            {
                if (perfil == null || string.IsNullOrWhiteSpace(nomePlaylist))
                {
                    throw new ArgumentException("Perfil e nome da playlist são obrigatórios.");
                }
                var playlist = new Playlist
                {
                    Id = playlistIdCounter++,
                    Nome = nomePlaylist,
                    DataCriacao = DateTime.Now,
                    Perfil = perfil,
                    Itens = new List<Conteudo>()
                };
                playlistDAO.AdicionarPlaylist(playlist);
                perfil.Playlists ??= new List<Playlist>();
                perfil.Playlists.Add(playlist);
                Console.WriteLine($"Playlist '{nomePlaylist}' criada para o perfil {perfil.Nome}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar playlist: {ex.Message}");
            }
        }

        public List<Playlist> ObterPlaylistsPerfil(PerfilUsuario perfil)
        {
            try
            {
                if (perfil == null)
                {
                    throw new ArgumentException("Perfil não pode ser nulo.");
                }
                return playlistDAO.ObterPlaylistsPorPerfil(perfil.Id) ?? new List<Playlist>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter playlists: {ex.Message}");
                return new List<Playlist>();
            }
        }

        public Playlist? ObterPlaylistPorId(int id)
        {
            try
            {
                var perfil = Program.UsuarioLogado?.Perfis?.Find(p => p.Playlists.Any(pl => pl.Id == id));
                if (perfil == null) return null;
                return perfil.Playlists.FirstOrDefault(p => p.Id == id) ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter playlist: {ex.Message}");
                return null;
            }
        }

        public Conteudo? ObterConteudoPorId(int id)
        {
            try
            {
                return conteudos.Find(c => c.Id == id) ?? throw new Exception("Conteúdo não encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter conteúdo: {ex.Message}");
                return null;
            }
        }

        public void AdicionarConteudoPlaylist(Playlist playlist, Conteudo conteudo)
        {
            try
            {
                if (playlist == null || conteudo == null)
                {
                    throw new ArgumentException("Playlist e conteúdo são obrigatórios.");
                }
                playlist.Itens.Add(conteudo);
                Console.WriteLine($"Conteúdo '{conteudo.Titulo}' adicionado à playlist '{playlist.Nome}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar conteúdo à playlist: {ex.Message}");
            }
        }

        public void Reproduzir(Conteudo conteudo)
        {
            try
            {
                if (conteudo == null)
                {
                    throw new ArgumentException("Conteúdo não pode ser nulo.");
                }
                conteudo.Reproduzir();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao reproduzir conteúdo: {ex.Message}");
            }
        }
    }
}