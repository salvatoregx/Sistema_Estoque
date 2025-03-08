using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Channels;

public class Registros
{
    private string nome;
    private double preco;
    private int on_hand;
    private string pais_or;
    private string fabricante;
    private string categoria;
    public string SKU;
    private static int contador_SKU = 0;
    private static List<Registros> todosRegistros = new List<Registros>();

    public Registros(string n, double p, int oh, string po, string f, string c)
    {
        this.nome = n;
        this.preco = p;
        this.on_hand = oh;
        this.pais_or = po;
        this.fabricante = f;
        this.categoria = c;
        int SKU_unico = ++contador_SKU;
        this.SKU = pais_or.Substring(0, 2) + fabricante.Substring(0, 3) + categoria.Substring(0, 3) + SKU_unico.ToString("D5");

        todosRegistros.Add(this);
    }

    public static string Criar()
    {
        string pais_orInput = "";
        string fabricanteInput = "";
        string categoriaInput = "";
        string nomeInput = "";
        double precoInput = 0;
        int on_handInput = 0;

        bool flag = true;
        Console.Clear();
        Console.WriteLine("Novo Registro\n");

        Console.WriteLine("Indique o país de origem do produto:");
        while (flag)
        {
            try
            {
                pais_orInput = Console.ReadLine();
                flag = String.IsNullOrEmpty(pais_orInput);
                if (flag)
                {
                    Console.WriteLine("Entrada inválida. Por gentileza indique o país novamente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Por gentileza indique o país novamente!");
            }
        }


        Console.WriteLine("Indique o fabricante do produto:");
        flag = true;
        while (flag)
        {
            try
            {
                fabricanteInput = Console.ReadLine();
                flag = String.IsNullOrEmpty(fabricanteInput);
                if (flag)
                {
                    Console.WriteLine("Entrada inválida. Por gentileza indique o fabricante novamente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Por gentileza indique o fabricante novamente!");
            }
        }

        Console.WriteLine("Indique a categoria do produto:");
        flag = true;
        while (flag)
        {
            try
            {
                categoriaInput = Console.ReadLine();
                flag = String.IsNullOrEmpty(categoriaInput);
                if (flag)
                {
                    Console.WriteLine("Entrada inválida. Por gentileza indique a categoria novamente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Por gentileza indique a categoria novamente!");
            }
        }

        Console.WriteLine("Indique o nome do produto:");
        flag = true;
        while (flag)
        {
            try
            {
                nomeInput = Console.ReadLine();
                flag = String.IsNullOrEmpty(nomeInput);
                if (flag)
                {
                    Console.WriteLine("Entrada inválida. Por gentileza indique o nome novamente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Por gentileza indique o nome novamente!");
            }
        }

        flag = true;
        while (flag)
        {
            try
            {
                bool precoValido = false;
                while (!precoValido)
                {
                    Console.WriteLine("Indique o preço do produto (formato 0.00):");
                    precoValido = double.TryParse(Console.ReadLine(), out precoInput);
                    if (!precoValido) Console.WriteLine("Valor inválido! Tente novamente.");
                    else flag = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Insira o preço novamente!");
            }
        }

        new Registros(
            nomeInput,
            precoInput,
            on_handInput,
            pais_orInput,
            fabricanteInput,
            categoriaInput
        );

        return "Produto cadastrado com sucesso!";
    }

    public static string Listar()
    {
        if (todosRegistros.Count == 0)
        {
            return "Nenhum registro cadastrado.";
        }

        var colunas = new List<(string Nome, int Cont)>
        {
            ("SKU", 10),
            ("Nome", 20),
            ("País", 15),
            ("Fabricante", 15),
            ("Categoria", 15),
            ("Preço", 10),
            ("Estoque", 10)
        };

        foreach (var reg in todosRegistros)
        {
            colunas[0] = (colunas[0].Nome, Math.Max(colunas[0].Cont, reg.SKU.Length));
            colunas[1] = (colunas[1].Nome, Math.Max(colunas[1].Cont, reg.nome.Length));
            colunas[2] = (colunas[2].Nome, Math.Max(colunas[2].Cont, reg.pais_or.Length));
            colunas[3] = (colunas[3].Nome, Math.Max(colunas[3].Cont, reg.fabricante.Length));
            colunas[4] = (colunas[4].Nome, Math.Max(colunas[4].Cont, reg.categoria.Length));
            colunas[5] = (colunas[5].Nome, Math.Max(colunas[5].Cont, $"R${reg.preco:N2}".Length));
            colunas[6] = (colunas[6].Nome, Math.Max(colunas[6].Cont, reg.on_hand.ToString().Length));
        }

        var format = string.Format("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|",
            $" {{0,-{colunas[0].Cont}}} ",
            $" {{1,-{colunas[1].Cont}}} ",
            $" {{2,-{colunas[2].Cont}}} ",
            $" {{3,-{colunas[3].Cont}}} ",
            $" {{4,-{colunas[4].Cont}}} ",
            $" {{5,-{colunas[5].Cont}}} ",
            $" {{6,-{colunas[6].Cont}}} ");

        var header = string.Format(format,
            colunas[0].Nome,
            colunas[1].Nome,
            colunas[2].Nome,
            colunas[3].Nome,
            colunas[4].Nome,
            colunas[5].Nome,
            colunas[6].Nome);

        // Print table
        Console.WriteLine(new string('-', header.Length));
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));

        foreach (var reg in todosRegistros)
        {
            Console.WriteLine(format,
                reg.SKU,
                reg.nome,
                reg.pais_or,
                reg.fabricante,
                reg.categoria,
                $"R${reg.preco:N2}",
                reg.on_hand
                );
        }
        Console.WriteLine(new string('-', header.Length));
        Console.WriteLine();
        Console.WriteLine("Listagem concluída. Aperte qualquer botão para retornar ao menu principal.");
        Console.ReadKey();

        return null;
    }

    public static string Remover()
    {
        if (todosRegistros.Count == 0)
        {
            return "Nenhum registro cadastrado.";
        }

        Console.WriteLine("Digite o SKU do produto que você deseja remover ou digite x para desistir:");
        while (true)
        {
            string SKU_deletar = Console.ReadLine();
            if (SKU_deletar.ToLower() == "x")
            {
                return "Remoção cancelada.";
            }

            var registroParaRemover = todosRegistros.FirstOrDefault(r => r.SKU == SKU_deletar);

            if (registroParaRemover != null)
            {
                todosRegistros.Remove(registroParaRemover);
                return "Registro removido com sucesso!";
            }

            Console.WriteLine("SKU inválido. Tente novamente!");
        }
    }

    public static string Alteracao(bool metodo)
    {
        if (todosRegistros.Count == 0)
        {
            return "Nenhum registro cadastrado.";
        }

        Console.WriteLine($"Digite o SKU do produto para o qual você deseja {(metodo ? "incrementar" : "reduzir")} o estoque ou digite x para desistir:");
        while (true)
        {
            string SKU_ajustado = Console.ReadLine();
            if (SKU_ajustado.ToLower() == "x")
            {
                return "Remoção cancelada.";
            }

            var registroParaAjustar = todosRegistros.FirstOrDefault(r => r.SKU == SKU_ajustado);

            if (registroParaAjustar != null)
            {
                if (metodo)
                {
                    Console.WriteLine("Indique em quantas unidades você quer aumentar o estoque:");
                    int quantidade;
                    while (true)
                    {
                        if (!int.TryParse(Console.ReadLine(), out quantidade))
                        {
                            Console.WriteLine("Entrada inválida! Insira um número inteiro:");
                            continue;
                        }
                        if (quantidade < 0)
                        {
                            Console.WriteLine("A quantidade deve ser positiva! Tente novamente:");
                            continue;
                        }
                        break;
                    }
                    registroParaAjustar.on_hand += quantidade;
                }

                else
                {
                    Console.WriteLine("Indique em quantas unidades você quer reduzir o estoque:");
                    int quantidade;
                    while (true)
                    {
                        if (!int.TryParse(Console.ReadLine(), out quantidade))
                        {
                            Console.WriteLine("Entrada inválida! Insira um número inteiro:");
                            continue;
                        }
                        if (quantidade < 0)
                        {
                            Console.WriteLine("A quantidade deve ser positiva! Tente novamente:");
                            continue;
                        }
                        if (quantidade > registroParaAjustar.on_hand)
                        {
                            Console.WriteLine($"Quantidade excede o estoque atual ({registroParaAjustar.on_hand})! Tente novamente:");
                            continue;
                        }
                        break;
                    }
                    registroParaAjustar.on_hand -= quantidade;
                }

                return "Registro alterado com sucesso!";
            }

            Console.WriteLine("SKU inválido. Tente novamente!");
        }
    }

    public static string LimparRegistros()
    {
        Console.WriteLine("Tem certeza que deseja apagar TODOS os registros? (S/N)");
        var confirmacao = Console.ReadLine().ToUpper();

        if (confirmacao == "S")
        {
            todosRegistros.Clear();
            return "Limpeza concluída.";
        }
        return "Operação cancelada.";
    }

}
