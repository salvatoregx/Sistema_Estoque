public class Utilities()
{
    public static void Menu(string resultado)
    {
        Console.Clear();
        if (!String.IsNullOrEmpty(resultado))
        {
            Console.WriteLine(resultado);
            Console.WriteLine();
        }
        Console.WriteLine("Menu principal:");
        Console.WriteLine("[1] Novo");
        Console.WriteLine("[2] Listar produtos");
        Console.WriteLine("[3] Remover produtos");
        Console.WriteLine("[4] Entrada em estoque");
        Console.WriteLine("[5] Saída Estoque");
        Console.WriteLine("[0] Sair");
        Console.WriteLine("[*] Popular lista base");
        Console.WriteLine("[#] Apagar todos registros\n");
        Console.WriteLine("Informe a opção desejada:");
        Console.ReadLine();

    }
}