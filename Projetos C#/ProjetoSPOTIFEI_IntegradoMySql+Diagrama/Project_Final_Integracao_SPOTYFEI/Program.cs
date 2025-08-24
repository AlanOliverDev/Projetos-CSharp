using Spotifei;
using System;
using System.Collections.Generic;

namespace Spotifei
{
    class Program
    {
        private static UsuarioService usuarioService = new UsuarioService();
        private static StreamingService streamingService = new StreamingService();
        private static Usuario? usuarioLogado = null;

        public static Usuario? UsuarioLogado => usuarioLogado;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Bem-vindo ao Spotifei ===");

            bool sair = false;
            while (!sair)
            {
                try
                {
                    if (usuarioLogado == null)
                        MenuInicial();
                    else
                        MenuUsuarioLogado();

                    Console.Write("Escolha uma opção: ");
                    if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 0)
                    {
                        Console.WriteLine("Opção inválida. Digite um número válido.");
                        continue;
                    }

                    if (usuarioLogado == null)
                    {
                        switch (opcao)
                        {
                            case 1: CriarUsuario(); break;
                            case 2: Login(); break;
                            case 0: sair = true; break;
                            default: Console.WriteLine("Opção inválida."); break;
                        }
                    }
                    else
                    {
                        switch (opcao)
                        {
                            case 1: CriarPerfil(); break;
                            case 2: ListarPerfis(); break;
                            case 3: CriarPlaylist(); break;
                            case 4: ListarPlaylistsPerfil(); break;
                            case 5: AdicionarConteudoPlaylist(); break;
                            case 6: ReproduzirConteudo(); break;
                            case 7: Logout(); break;
                            case 0: sair = true; break;
                            default: Console.WriteLine("Opção inválida."); break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                }
            }

            Console.WriteLine("Obrigado por usar o Spotifei!");
        }

        static void MenuInicial()
        {
            Console.WriteLine("\n--- Menu Inicial ---");
            Console.WriteLine("1 - Criar usuário");
            Console.WriteLine("2 - Login");
            Console.WriteLine("0 - Sair");
        }

        static void MenuUsuarioLogado()
        {
            Console.WriteLine($"\n--- Menu do Usuário: {usuarioLogado?.Nome ?? "Desconhecido"} ---");
            Console.WriteLine("1 - Criar perfil");
            Console.WriteLine("2 - Listar meus perfis");
            Console.WriteLine("3 - Criar playlist");
            Console.WriteLine("4 - Listar playlists de um perfil");
            Console.WriteLine("5 - Adicionar conteúdo em playlist");
            Console.WriteLine("6 - Reproduzir conteúdo");
            Console.WriteLine("7 - Logout");
            Console.WriteLine("0 - Sair");
        }

        static void CriarUsuario()
        {
            Console.WriteLine("\n=== Criar Usuário ===");
            try
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nome))
                    throw new ArgumentException("Nome é obrigatório.");

                Console.Write("Email: ");
                string email = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                    throw new ArgumentException("Email inválido.");

                Console.Write("Data de nascimento (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
                    throw new ArgumentException("Data de nascimento inválida.");

                Console.Write("País: ");
                string pais = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(pais))
                    throw new ArgumentException("País é obrigatório.");

                Console.Write("Estado (opcional): ");
                string? estado = Console.ReadLine()?.Trim();

                Console.Write("Gênero (opcional): ");
                string? genero = Console.ReadLine()?.Trim();

                Console.Write("Senha: ");
                string senha = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(senha))
                    throw new ArgumentException("Senha é obrigatória.");

                var usuario = new Usuario
                {
                    Nome = nome,
                    Email = email,
                    DataNascimento = dataNascimento,
                    Pais = pais,
                    Estado = estado,
                    Genero = genero,
                    Senha = senha,
                    Status = "ativo",
                    DataCadastro = DateTime.Now,
                    Plano = new PlanoAssinatura
                    {
                        TipoPlano = "basico",
                        PrecoMensal = 9.99m,
                        MaxPerfis = 1,
                        QualidadeAudio = "media",
                        QuantidadeAnuncios = "normal"
                    }
                };

                usuarioService.CriarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
            }
        }

        static void Login()
        {
            Console.WriteLine("\n=== Login ===");
            try
            {
                Console.Write("Email: ");
                string email = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email é obrigatório.");

                Console.Write("Senha: ");
                string senha = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(senha))
                    throw new ArgumentException("Senha é obrigatória.");

                var usuario = usuarioService.ObterUsuarioPorEmail(email);
                if (usuario == null || usuario.Senha != senha)
                    throw new Exception("Email ou senha incorretos.");

                usuarioLogado = usuario;
                Console.WriteLine($"Login realizado com sucesso! Bem-vindo, {usuarioLogado.Nome}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer login: {ex.Message}");
            }
        }

        static void Logout()
        {
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Nenhum usuário logado.");
                Console.WriteLine($"Usuário {usuarioLogado.Nome} deslogado.");
                usuarioLogado = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer logout: {ex.Message}");
            }
        }

        static void CriarPerfil()
        {
            Console.WriteLine("\n=== Criar Perfil ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                Console.Write("Nome do perfil: ");
                string nomePerfil = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nomePerfil))
                    throw new ArgumentException("Nome do perfil é obrigatório.");

                var perfil = new PerfilUsuario { Nome = nomePerfil, Usuario = usuarioLogado };
                usuarioService.AdicionarPerfil(usuarioLogado, perfil);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar perfil: {ex.Message}");
            }
        }

        static void ListarPerfis()
        {
            Console.WriteLine("\n=== Listar Perfis ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                var perfis = usuarioLogado.Perfis;
                if (perfis == null || perfis.Count == 0)
                    Console.WriteLine("Nenhum perfil cadastrado.");
                else
                    foreach (var perfil in perfis)
                        Console.WriteLine($"ID: {perfil.Id} | Nome: {perfil.Nome}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar perfis: {ex.Message}");
            }
        }

        static void CriarPlaylist()
        {
            Console.WriteLine("\n=== Criar Playlist ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                Console.Write("ID do perfil para criar playlist: ");
                if (!int.TryParse(Console.ReadLine(), out int perfilId) || perfilId <= 0)
                    throw new ArgumentException("ID inválido.");

                var perfil = usuarioLogado.Perfis.Find(p => p.Id == perfilId);
                if (perfil == null)
                    throw new Exception("Perfil não encontrado.");

                Console.Write("Nome da playlist: ");
                string nomePlaylist = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nomePlaylist))
                    throw new ArgumentException("Nome da playlist é obrigatório.");

                streamingService.CriarPlaylist(perfil, nomePlaylist);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar playlist: {ex.Message}");
            }
        }

        static void ListarPlaylistsPerfil()
        {
            Console.WriteLine("\n=== Listar Playlists ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                Console.Write("ID do perfil: ");
                if (!int.TryParse(Console.ReadLine(), out int perfilId) || perfilId <= 0)
                    throw new ArgumentException("ID inválido.");

                var perfil = usuarioLogado.Perfis.Find(p => p.Id == perfilId);
                if (perfil == null)
                    throw new Exception("Perfil não encontrado.");

                var playlists = streamingService.ObterPlaylistsPerfil(perfil);
                if (playlists.Count == 0)
                    Console.WriteLine("Nenhuma playlist cadastrada.");
                else
                    foreach (var p in playlists)
                        Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar playlists: {ex.Message}");
            }
        }

        static void AdicionarConteudoPlaylist()
        {
            Console.WriteLine("\n=== Adicionar Conteúdo à Playlist ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                Console.Write("ID da playlist: ");
                if (!int.TryParse(Console.ReadLine(), out int playlistId) || playlistId <= 0)
                    throw new ArgumentException("ID inválido.");

                var playlist = streamingService.ObterPlaylistPorId(playlistId);
                if (playlist == null)
                    throw new Exception("Playlist não encontrada.");

                Console.Write("ID do conteúdo para adicionar: ");
                if (!int.TryParse(Console.ReadLine(), out int conteudoId) || conteudoId <= 0)
                    throw new ArgumentException("ID inválido.");

                var conteudo = streamingService.ObterConteudoPorId(conteudoId);
                if (conteudo == null)
                    throw new Exception("Conteúdo não encontrado.");

                streamingService.AdicionarConteudoPlaylist(playlist, conteudo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar conteúdo: {ex.Message}");
            }
        }

        static void ReproduzirConteudo()
        {
            Console.WriteLine("\n=== Reproduzir Conteúdo ===");
            try
            {
                if (usuarioLogado == null)
                    throw new Exception("Faça login primeiro.");

                Console.Write("ID do conteúdo para reproduzir: ");
                if (!int.TryParse(Console.ReadLine(), out int conteudoId) || conteudoId <= 0)
                    throw new ArgumentException("ID inválido.");

                var conteudo = streamingService.ObterConteudoPorId(conteudoId);
                if (conteudo == null)
                    throw new Exception("Conteúdo não encontrado.");

                streamingService.Reproduzir(conteudo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao reproduzir conteúdo: {ex.Message}");
            }
        }
    }
}