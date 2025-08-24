using System;
using System.Collections.Generic;

namespace Spotifei
{
    public class UsuarioService
    {
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        private readonly PerfilUsuarioDAO perfilDAO = new PerfilUsuarioDAO();
        private int usuarioIdCounter = 1;
        private int perfilIdCounter = 1;

        public void CriarUsuario(Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    throw new ArgumentException("Nome, email e senha são obrigatórios.");
                }
                if (!DateTime.TryParse(usuario.DataNascimento.ToString(), out _))
                {
                    throw new ArgumentException("Data de nascimento inválida.");
                }
                usuario.Id = usuarioIdCounter++;
                usuario.Status = "ativo";
                usuario.DataCadastro = DateTime.Now;
                usuarioDAO.AdicionarUsuario(usuario);
                Console.WriteLine($"Usuário {usuario.Nome} cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
            }
        }

        public Usuario? ObterUsuarioPorEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email não pode ser nulo ou vazio.");
                }
                return usuarioDAO.ObterUsuarioPorEmail(email) ?? throw new Exception("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                return null;
            }
        }

        public void AdicionarPerfil(Usuario usuario, PerfilUsuario perfil)
        {
            try
            {
                if (usuario == null || string.IsNullOrWhiteSpace(perfil.Nome))
                {
                    throw new ArgumentException("Usuário e nome do perfil são obrigatórios.");
                }
                perfil.Id = perfilIdCounter++;
                perfil.Usuario = usuario;
                perfilDAO.AdicionarPerfil(perfil); // Persistir no banco
                usuario.Perfis ??= new List<PerfilUsuario>(); // Inicializar se nulo
                usuario.Perfis.Add(perfil); // Adicionar à lista do usuário
                Console.WriteLine($"Perfil '{perfil.Nome}' adicionado para {usuario.Nome}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar perfil: {ex.Message}");
            }
        }

        public List<PerfilUsuario> ObterTodosPerfis()
        {
            // Implementar com PerfilUsuarioDAO
            return new List<PerfilUsuario>(); // Placeholder
        }

        public PerfilUsuario? ObterPerfilPorId(int id)
        {
            // Implementar com PerfilUsuarioDAO
            return null; // Placeholder
        }
    }
}