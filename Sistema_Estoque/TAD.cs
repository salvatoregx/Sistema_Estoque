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

        flag = true;
        while (flag)
        {
            try
            {
                bool estoqueValido = false;
                while (!estoqueValido)
                {
                    Console.WriteLine("Indique a quantidade inicial em estoque:");
                    estoqueValido = int.TryParse(Console.ReadLine(), out on_handInput);
                    if (!estoqueValido) Console.WriteLine("Valor inválido! Tente novamente.");
                    else flag = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Entrada inválida. Insira a quantidade novamente!");
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

        var colunas = new Dictionary<string, int>
        {
            ["Nome"] = "Nome".Length,
            ["Preço"] = "Preço".Length,
            ["Estoque"] = "Estoque".Length,
            ["País"] = "País".Length,
            ["Fabricante"] = "Fabricante".Length,
            ["Categoria"] = "Categoria".Length,
            ["SKU"] = "SKU".Length
        };

        foreach (var reg in todosRegistros)
        {
            colunas["Nome"] = Math.Max(colunas["Nome"], reg.nome.Length);
            colunas["Preço"] = Math.Max(colunas["Preço"], reg.preco.ToString("N2").Length);
            colunas["Estoque"] = Math.Max(colunas["Estoque"], reg.on_hand.ToString().Length);
            colunas["País"] = Math.Max(colunas["País"], reg.pais_or.Length);
            colunas["Fabricante"] = Math.Max(colunas["Fabricante"], reg.fabricante.Length);
            colunas["Categoria"] = Math.Max(colunas["Categoria"], reg.categoria.Length);
            colunas["SKU"] = Math.Max(colunas["SKU"], reg.SKU.Length);
        }

        var format = string.Join(" | ", colunas.Select(c => $"{{0,-{c.Value}}}")) + " |";
        var header = string.Format(format,
            "SKU", "Nome", "País", "Fabricante", "Categoria", "Preço", "Estoque");

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
                "R$" + reg.preco.ToString("N2"),
                reg.on_hand.ToString()
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
                        if (quantidade <= 0)
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
                        if (quantidade <= 0)
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
            return "Todos os registros foram removidos com sucesso!";
        }
        return "Operação cancelada.";
    }

}
