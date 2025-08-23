using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Menu de Acesso ---");
            Console.WriteLine("1 - Cadastrar Admin");
            Console.WriteLine("2 - Cadastrar Usuário Comum");
            Console.WriteLine("3 - Fazer Login");
            Console.WriteLine("4 - Entrar como Convidado");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite nome do Admin: ");
                    string nomeAdmin = Console.ReadLine();
                    Console.Write("Digite senha: ");
                    string senhaAdmin = Console.ReadLine();
                    if (BancoUsuarios.CadastrarUsuario(new Admin(nomeAdmin, senhaAdmin)))
                    {
                        Console.WriteLine("✅ Admin cadastrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Nome já está em uso. Escolha outro.");
                    }
                    break;

                case "2":
                    Console.Write("Digite nome do Usuário: ");
                    string nomeUser = Console.ReadLine();
                    Console.Write("Digite senha: ");
                    string senhaUser = Console.ReadLine();
                    if (BancoUsuarios.CadastrarUsuario(new UsuarioComum(nomeUser, senhaUser)))
                    {
                        Console.WriteLine("✅ Usuário comum cadastrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Nome já está em uso. Escolha outro.");
                    }
                    break;

                case "3":
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

                case "4":
                    var convidado = new Convidado();
                    convidado.Autenticar();
                    break;

                case "0":
                    Console.WriteLine("Encerrando...Programa Finalizado com sucesso✅");
                    return;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}