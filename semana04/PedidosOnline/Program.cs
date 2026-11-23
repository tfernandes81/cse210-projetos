using System;

class Program
{
    static void Main(string[] args)
    {
        // ----- Pedido 1: cliente nos EUA -----
        Endereco endereco1 = new Endereco("123 Main St", "Salt Lake City", "UT", "USA");
        Cliente cliente1 = new Cliente("John Smith", endereco1);

        Pedido pedido1 = new Pedido(cliente1);
        pedido1.AdicionarProduto(new Produto("Teclado Mecânico", "TEC-001", 49.99, 2));
        pedido1.AdicionarProduto(new Produto("Mouse Sem Fio", "MOU-014", 19.50, 3));
        pedido1.AdicionarProduto(new Produto("Mousepad", "PAD-100", 8.00, 1));

        // ----- Pedido 2: cliente fora dos EUA -----
        Endereco endereco2 = new Endereco("Av. Paulista, 1000", "São Paulo", "SP", "Brasil");
        Cliente cliente2 = new Cliente("Maria Silva", endereco2);

        Pedido pedido2 = new Pedido(cliente2);
        pedido2.AdicionarProduto(new Produto("Monitor 24\"", "MON-240", 129.90, 1));
        pedido2.AdicionarProduto(new Produto("Cabo HDMI", "CAB-007", 7.25, 4));

        ExibirPedido(1, pedido1);
        ExibirPedido(2, pedido2);
    }

    static void ExibirPedido(int numero, Pedido pedido)
    {
        Console.WriteLine($"========== PEDIDO {numero} ==========");
        Console.WriteLine("--- Etiqueta de Embalagem ---");
        Console.WriteLine(pedido.ObterEtiquetaEmbalagem());
        Console.WriteLine();
        Console.WriteLine("--- Etiqueta de Envio ---");
        Console.WriteLine(pedido.ObterEtiquetaEnvio());
        Console.WriteLine();
        Console.WriteLine($"Preço Total: ${pedido.CalcularCustoTotal():F2}");
        Console.WriteLine();
    }
}