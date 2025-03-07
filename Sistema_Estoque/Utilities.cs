public class Utilities()
{
    public static bool Menu(string resultado)
    {
        string res = resultado;

        while (true)
        {
            if (!String.IsNullOrEmpty(res))
            {
                Console.WriteLine(res);
                Console.WriteLine();
                Console.WriteLine("Aperte qualquer botão para retornar ao menu principal.");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("Controle de estoque - Salvatore Xerri Variedades Ltda.\n");
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
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    res = Registros.Criar();
                    break;
                case "2":
                    res = Registros.Listar();
                    break;
                case "3":
                    res = Registros.Remover();
                    break;
                case "4":
                    res = Registros.Alteracao(true);
                    break;
                case "5":
                    res = Registros.Alteracao(false);
                    break;
                case "0":
                    return false;
                case "*":
                    res = null;
                    new Registros("Camiseta Básica", 49.90, 100, "Brasil", "RoupaNac", "Vestuário");
                    new Registros("Notebook", 4299.00, 5, "EUA", "HomeOffice", "Eletrônicos");
                    new Registros("Mesa Escritório", 299.90, 10, "Argentina", "HomeOffice", "Móveis");
                    new Registros("Smartphone", 1899.00, 20, "China", "CeluChina", "Celulares");
                    new Registros("Fone Bluetooth", 199.50, 50, "China", "CeluChina", "Acessórios");
                    Console.WriteLine("Listagem inicial concluída. Aperte qualquer botão para continuar.");
                    Console.ReadKey();
                    break;
                case "#":
                    res = Registros.LimparRegistros();
                    break;
                default:
                    res = "Escolha inválida. Tente novamente!";
                    break;
            }
        }

    }
}