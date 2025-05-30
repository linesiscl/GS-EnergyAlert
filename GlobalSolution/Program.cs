using GlobalSolution.Model;
using GlobalSolution.Service;
using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CadastroOuLogin usuarioService = new CadastroOuLogin();
        RegistroFalhas registroFalhas = new RegistroFalhas(usuarioService);
        ServicoDeAlerta servicoDeAlerta = new ServicoDeAlerta();

        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ENERGY ALERT ===");
                Console.WriteLine("1. Cadastrar Cidadão");
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
                else if (opcao == "2")
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
                            Console.WriteLine("3. Ver Alertas");
                            Console.WriteLine("0. Sair");
                            Console.Write("Escolha: ");
                            string subOpcao = Console.ReadLine();

                            if (subOpcao == "0") break;

                            if (subOpcao == "1")
                            {
                                Console.WriteLine("\n== Registrar Falha ==");
                                Console.Write("Local: ");
                                string local = Console.ReadLine();

                                Console.WriteLine("Tipo de Falha:");
                                Console.WriteLine("1. Queda Temporária");
                                Console.WriteLine("2. Apagão");
                                Console.WriteLine("3. Oscilação de tensão");
                                Console.WriteLine("4. Queda programada");
                                Console.Write("Escolha: ");
                                string tipoOpcao = Console.ReadLine();
                                string tipo = tipoOpcao switch
                                {
                                    "1" => "Queda Temporária",
                                    "2" => "Apagão",
                                    "3" => "Oscilação de tensão",
                                    "4" => "Queda programada",
                                    _ => "Outro"
                                };

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

                                registroFalhas.AdicionarFalha(falha);
                                Console.WriteLine("Falha registrada com sucesso.");
                            }
                            else if (subOpcao == "2")
                            {
                                Console.WriteLine("\n== Suas Falhas de Energia ==");
                                var falhas = registroFalhas.ListarPorCidadao(cidadao.Id);
                                if (falhas.Count == 0)
                                    Console.WriteLine("Nenhuma falha registrada.");
                                else
                                    foreach (var f in falhas)
                                        Console.WriteLine($"- {f.DataHora} | {f.Local} | Tipo: {f.Tipo} | Técnico: {f.TecnicoId}");
                            }
                            else if (subOpcao == "3")
                            {
                                Console.WriteLine("\n== Alertas Ativos ==");
                                var alertas = servicoDeAlerta.ListarAlertas();
                                if (alertas.Count == 0)
                                    Console.WriteLine("Nenhum alerta ativo.");
                                else
                                    foreach (var a in alertas)
                                        Console.WriteLine($"- {a.DataHora} | {a.Local} | Tipo: {a.Tipo} | {a.Descricao}");
                            }
                            else Console.WriteLine("Opção inválida.");
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

                        while (true)
                        {
                            Console.WriteLine("\n== Menu do Técnico ==");
                            Console.WriteLine("1. Registrar Alerta de Perigo");
                            Console.WriteLine("2. Registrar Alerta de Resolução");
                            Console.WriteLine("3. Ver Falhas Atribuídas");
                            Console.WriteLine("0. Sair");
                            Console.Write("Escolha: ");
                            string subOpcao = Console.ReadLine();

                            if (subOpcao == "0") break;

                            if (subOpcao == "1" || subOpcao == "2")
                            {
                                Console.Write("Local: ");
                                string local = Console.ReadLine();
                                Console.Write("Descrição: ");
                                string descricao = Console.ReadLine();
                                string tipo = subOpcao == "1" ? "Perigo" : "Resolvido";

                                var alerta = new Alerta
                                {
                                    Local = local,
                                    Tipo = tipo,
                                    Descricao = descricao,
                                    DataHora = DateTime.Now,
                                    TecnicoId = tecnico.Id
                                };

                                servicoDeAlerta.AdicionarAlerta(alerta);
                                Console.WriteLine("Alerta registrado com sucesso.");
                            }
                            else if (subOpcao == "3")
                            {
                                Console.WriteLine("\n== Falhas Atribuídas ==");
                                var falhas = registroFalhas.ListarPorTecnico(tecnico.Id);
                                if (falhas.Count == 0)
                                    Console.WriteLine("Nenhuma falha atribuída.");
                                else
                                    foreach (var f in falhas)
                                        Console.WriteLine($"- {f.DataHora} | {f.Local} | Tipo: {f.Tipo} | Descrição: {f.Descricao}");
                            }
                            else Console.WriteLine("Opção inválida.");
                        }
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

                        bool continuar = true;
                        while (continuar)
                        {
                            Console.WriteLine("\n== Cadastro de Técnico ==");
                            Console.Write("Nome: ");
                            string nome = Console.ReadLine();
                            Console.Write("Sobrenome: ");
                            string sobrenome = Console.ReadLine();
                            Console.Write("Data de nascimento (dd/MM/yyyy): ");
                            DateTime nascimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Console.Write("Senha: ");
                            string senhaTecnico = Console.ReadLine();

                            Console.WriteLine("Especialidade do técnico:");
                            Console.WriteLine("1. Queda Temporária");
                            Console.WriteLine("2. Apagão");
                            Console.WriteLine("3. Oscilação de tensão");
                            Console.WriteLine("4. Queda programada");
                            Console.Write("Escolha: ");
                            string espOpcao = Console.ReadLine();
                            string especialidade = espOpcao switch
                            {
                                "1" => "Queda Temporária",
                                "2" => "Apagão",
                                "3" => "Oscilação de tensão",
                                "4" => "Queda programada",
                                _ => "Outro"
                            };

                            var tecnico = new Tecnico
                            {
                                Nome = nome,
                                Sobrenome = sobrenome,
                                DataNascimento = nascimento,
                                Senha = senhaTecnico,
                                Especialidade = especialidade
                            };

                            usuarioService.CadastrarTecnico(tecnico);
                            Console.WriteLine("Técnico cadastrado com sucesso.");
                            Logs.Registrar($"Técnico cadastrado por admin: {tecnico.Id}");

                            Console.Write("\nDeseja cadastrar outro técnico? (s/n): ");
                            string resposta = Console.ReadLine()?.ToLower();
                            if (resposta != "s" && resposta != "sim")
                            {
                                continuar = false;
                                Console.WriteLine("Encerrando sessão do administrador...");
                                Logs.Registrar("Administrador encerrou o cadastro de técnicos.");
                            }
                        }
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
