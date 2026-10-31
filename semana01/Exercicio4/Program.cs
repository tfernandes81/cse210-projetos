using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<double> numeros = new List<double>();

        Console.WriteLine("Digite números (aceita positivos e negativos).");
        Console.WriteLine("Digite 0 para confirmar e ver o resultado.");

        while (true)
        {
            Console.Write("Número: ");
            string entrada = Console.ReadLine();

            if (!double.TryParse(entrada, out double numero))
            {
                Console.WriteLine("Valor inválido! Digite apenas números.");
                continue;
            }

            if (numero == 0)
            {
                break; // o zero só confirma, não entra na conta
            }

            numeros.Add(numero);
        }

        // Caso o usuário digite 0 logo de cara
        if (numeros.Count == 0)
        {
            Console.WriteLine("Nenhum número foi informado.");
            return;
        }

        double soma = numeros.Sum();
        double media = numeros.Average();
        double maior = numeros.Max();
        double menor = numeros.Min();

        Console.WriteLine();
        Console.WriteLine("===== RESULTADOS =====");
        Console.WriteLine($"Soma de todos os valores: {soma}");
        Console.WriteLine($"Média dos valores: {media:F2}");
        Console.WriteLine($"Maior número: {maior}");
        Console.WriteLine($"Menor número: {menor}");

        // Menor número positivo (pode não existir se só houver negativos)
        var positivos = numeros.Where(n => n > 0).ToList();

        if (positivos.Count > 0)
        {
            Console.WriteLine($"Menor número positivo: {positivos.Min()}");
        }
        else
        {
            Console.WriteLine("Menor número positivo: não há números positivos na lista.");
        }

        // Lista em ordem crescente
        var ordenados = numeros.OrderBy(n => n).ToList();
        Console.WriteLine($"Lista em ordem crescente: {string.Join(", ", ordenados)}");
    }
}