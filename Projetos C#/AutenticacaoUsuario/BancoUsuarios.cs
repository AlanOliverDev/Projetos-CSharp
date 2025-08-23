using System.Collections.Generic;

public static class BancoUsuarios
{
    private static List<Usuario> usuarios = new List<Usuario>();

    public static bool UsuarioExiste(string nome)
    {
        foreach (var u in usuarios)
        {
            if (u.Nome == nome)
                return true;
        }
        return false;
    }

    public static bool CadastrarUsuario(Usuario usuario)
    {
        if (UsuarioExiste(usuario.Nome))
            return false;

        usuarios.Add(usuario);
        return true;
    }

    public static Usuario Logar(string nome, string senha)
    {
        foreach (var u in usuarios)
        {
            if (u.Nome == nome && u.Senha == senha)
                return u;
        }
        return null;
    }
}