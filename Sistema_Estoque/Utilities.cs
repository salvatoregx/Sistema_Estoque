public class Utilities()
{
    public static bool Menu(string resultado)
    {
        while (true)
        {
            Console.Clear();
            if (!String.IsNullOrEmpty(resultado))
            {
                Console.WriteLine(resultado);
                Console.WriteLine();
            }

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
                    Registros.Criar();
                    break;
                case "2":
                    Registros.Listar();
                    break;
                case "3":
                    Registros.Remover();
                    break;
                case "4":
                    Registros.Alteracao(true);
                    break;
                case "5":
                    Registros.Alteracao(false);
                    break;
                case "0":
                    return false;
                case "*":
                    new Registros("Camiseta Básica", 49.90, 100, "Brasil", "RoupaNac", "Vestuário");
                    new Registros("Notebook", 4299.00, 5, "EUA", "HomeOffice", "Eletrônicos");
                    new Registros("Mesa Escritório", 299.90, 10, "Argentina", "HomeOffice", "Móveis");
                    new Registros("Smartphone", 1899.00, 20, "China", "CeluChina", "Celulares");
                    new Registros("Fone Bluetooth", 199.50, 50, "China", "CeluChina", "Acessórios");
                    break;
                case "#":
                    Registros.LimparRegistros();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Escolha inválida. Tente novamente!");
                    break;
            }
        }

    }
}