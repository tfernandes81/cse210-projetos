using System;

class Program
{
    static void Main()
    {
        ExibirBoasVindas();

        string nome = PerguntarNomeUsuario();
        int numero = PerguntarNumeroFavorito();
        int quadrado = ElevarAoQuadrado(numero);

        ExibirResultado(nome, quadrado);
    }

    static void ExibirBoasVindas()
    {
        Console.WriteLine("Bem-Vindo ao Programa!");
    }

    static string PerguntarNomeUsuario()
    {
        Console.Write("Qual é o seu nome? ");
        return Console.ReadLine() ?? string.Empty;
    }

    static int PerguntarNumeroFavorito()
    {
        int numero;
        Console.Write("Qual é o seu número favorito? ");

        while (!int.TryParse(Console.ReadLine(), out numero))
        {
            Console.Write("Entrada inválida. Digite um número inteiro: ");
        }

        return numero;
    }

    static int ElevarAoQuadrado(int numero)
    {
        return numero * numero;
    }

    static void ExibirResultado(string nome, int numeroAoQuadrado)
    {
        Console.WriteLine($"{nome}, o seu número favorito ao quadrado é {numeroAoQuadrado}.");
    }
}