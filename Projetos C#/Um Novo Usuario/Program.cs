using System;


Console.WriteLine("--- Sistema Iniciado ---");

while (true)
{
    Console.WriteLine("\n--- Menu de Acesso ---");
    Console.WriteLine("1 - Cadastrar Admin");
    Console.WriteLine("2 - Cadastrar Usuário Comum");
    Console.WriteLine("3 - Cadastrar Supervisor");
    Console.WriteLine("4 - Fazer Login");
    Console.WriteLine("5 - Entrar como Convidado");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarUsuario(new Admin(LerNome(), LerSenha()));
            break;

        case "2":
            CadastrarUsuario(new UsuarioComum(LerNome(), LerSenha()));
            break;

        case "3":
            CadastrarUsuario(new Supervisor(LerNome(), LerSenha()));
            break;

        case "4":
            Console.Write("Nome: ");
            string nomeLogin = Console.ReadLine();
            Console.Write("Senha: ");
            string senhaLogin = Console.ReadLine();
            var usuario = BancoUsuarios.Logar(nomeLogin, senhaLogin);
            if (usuario != null)
                usuario.Autenticar();
            else
                Console.WriteLine("❌ Nome ou senha inválidos!");
            break;

        case "5":
            var convidado = new Convidado();
            convidado.Autenticar();
            break;

        case "0":
            Console.WriteLine("Encerrando...Programa Finalizado com sucesso! ✅");
            return;

        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}

// Métodos auxiliares

void CadastrarUsuario(IUsuario usuario)
{
    if (BancoUsuarios.CadastrarUsuario(usuario))
        Console.WriteLine($"✅ {usuario.GetType().Name} cadastrado com sucesso!");
    else
        Console.WriteLine("❌ Nome já está em uso. Escolha outro.");
}

string LerNome()
{
    Console.Write("Digite o nome: ");
    return Console.ReadLine();
}

string LerSenha()
{
    Console.Write("Digite a senha: ");
    return Console.ReadLine();
}
