using GlobalSolution.Model;
using GlobalSolution.Service;
using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CadastroOuLogin usuarioService = new CadastroOuLogin();
        FalhaService falhaService = new FalhaService(usuarioService);

        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ENERGY ALERT ===");
                Console.WriteLine("1. Cadastrar Cidadão");

                if (usuarioService.TemCidadaos())
                    Console.WriteLine("2. Login Cidadão");

                Console.WriteLine("3. Login Técnico");
                Console.WriteLine("4. Login Administrador");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha: ");
                string opcao = Console.ReadLine();

                if (opcao == "0") break;

                if (opcao == "1")
                {
                    Console.WriteLine("\n== Cadastro de Cidadão ==");
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Sobrenome: ");
                    string sobrenome = Console.ReadLine();
                    Console.Write("Data de nascimento (dd/MM/yyyy): ");
                    DateTime nascimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    var c = new Cidadao { Nome = nome, Sobrenome = sobrenome, DataNascimento = nascimento, Senha = senha };
                    usuarioService.CadastrarCidadao(c);
                    Console.WriteLine("Cidadão cadastrado com sucesso.");
                    Logs.Registrar($"Novo cidadão cadastrado: {c.Id}");
                }
                else if (opcao == "2" && usuarioService.TemCidadaos())
                {
                    Console.WriteLine("\n== Login Cidadão ==");
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Sobrenome: ");
                    string sobrenome = Console.ReadLine();
                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    var cidadao = usuarioService.LoginCidadao(nome, sobrenome, senha);
                    if (cidadao != null)
                    {
                        Console.WriteLine("Login bem-sucedido.");
                        Logs.Registrar($"Login realizado com sucesso: cidadao {cidadao.Id}");

                        while (true)
                        {
                            Console.WriteLine("\n== Menu do Cidadão ==");
                            Console.WriteLine("1. Registrar Falha de Energia");
                            Console.WriteLine("2. Ver Minhas Falhas");
                            Console.WriteLine("0. Sair");
                            Console.Write("Escolha: ");
                            string subOpcao = Console.ReadLine();

                            if (subOpcao == "0") break;

                            if (subOpcao == "1")
                            {
                                Console.WriteLine("\n== Registrar Falha ==");
                                Console.Write("Local: ");
                                string local = Console.ReadLine();
                                Console.Write("Tipo: ");
                                string tipo = Console.ReadLine();
                                Console.Write("Descrição: ");
                                string descricao = Console.ReadLine();

                                var falha = new FalhaDeEnergia
                                {
                                    Local = local,
                                    Tipo = tipo,
                                    Descricao = descricao,
                                    DataHora = DateTime.Now,
                                    IdCidadao = cidadao.Id
                                };

                                falhaService.AdicionarFalha(falha);
                                Console.WriteLine("Falha registrada com sucesso.");
                            }
                            else if (subOpcao == "2")
                            {
                                Console.WriteLine("\n== Suas Falhas de Energia ==");
                                var falhas = falhaService.ListarPorCidadao(cidadao.Id);
                                if (falhas.Count == 0)
                                {
                                    Console.WriteLine("Nenhuma falha registrada.");
                                }
                                else
                                {
                                    foreach (var f in falhas)
                                    {
                                        Console.WriteLine($"- {f.DataHora} | {f.Local} | Tipo: {f.Tipo} | Técnico: {f.TecnicoResponsavelId}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opção inválida.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Credenciais inválidas.");
                        Logs.Registrar($"Tentativa de login falhou para cidadao {nome}_{sobrenome}");
                    }
                }
                else if (opcao == "3")
                {
                    Console.WriteLine("\n== Login Técnico ==");
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Sobrenome: ");
                    string sobrenome = Console.ReadLine();
                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    var tecnico = usuarioService.LoginTecnico(nome, sobrenome, senha);
                    if (tecnico != null)
                    {
                        Console.WriteLine("Login bem-sucedido.");
                        Logs.Registrar($"Login realizado com sucesso: tecnico {tecnico.Id}");

                        Console.WriteLine("\nFuncionalidades para técnico ainda não implementadas.");
                    }
                    else
                    {
                        Console.WriteLine("Credenciais inválidas.");
                        Logs.Registrar($"Tentativa de login falhou para tecnico {nome}_{sobrenome}");
                    }
                }
                else if (opcao == "4")
                {
                    Console.WriteLine("\n== Login Administrador ==");
                    Console.Write("Usuário: ");
                    string usuario = Console.ReadLine();
                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    if (usuarioService.LoginAdministrador(usuario, senha))
                    {
                        Console.WriteLine("Login de administrador bem-sucedido.");
                        Logs.Registrar("Administrador logado.");

                        Console.WriteLine("\n== Cadastro de Técnico ==");
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        Console.Write("Sobrenome: ");
                        string sobrenome = Console.ReadLine();
                        Console.Write("Data de nascimento (dd/MM/yyyy): ");
                        DateTime nascimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Console.Write("Senha: ");
                        string senhaTecnico = Console.ReadLine();

                        var tecnico = new Tecnico { Nome = nome, Sobrenome = sobrenome, DataNascimento = nascimento, Senha = senhaTecnico };
                        usuarioService.CadastrarTecnico(tecnico);
                        Console.WriteLine("Técnico cadastrado com sucesso.");
                        Logs.Registrar($"Técnico cadastrado por admin: {tecnico.Id}");
                    }
                    else
                    {
                        Console.WriteLine("Credenciais de administrador inválidas.");
                        Logs.Registrar("Tentativa de login inválida como administrador.");
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato de data inválido.");
                Logs.Registrar("ERRO: Data em formato inválido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
                Logs.Registrar($"ERRO: {ex.Message}");
            }
        }
    }
}

