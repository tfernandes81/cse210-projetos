using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Digite a nota numérica: ");
        int nota = int.Parse(Console.ReadLine());

        string letra;

        if (nota >= 90)
        {
            letra = "A";
        }
        else if (nota >= 80)
        {
            letra = "B";
        }
        else if (nota >= 70)
        {
            letra = "C";
        }
        else if (nota >= 60)
        {
            letra = "D";
        }
        else
        {
            letra = "F";
        }

        // Determinando o sinal pelo último dígito
        int ultimoDigito = nota % 10;
        string sinal;

        if (ultimoDigito >= 7)
        {
            sinal = "+";
        }
        else if (ultimoDigito < 3)
        {
            sinal = "-";
        }
        else
        {
            sinal = "";
        }

        // Parte 2: não existe A+
        if (letra == "A" && sinal == "+")
        {
            sinal = "";
        }

        // Parte 3: não existem F+ nem F-
        if (letra == "F")
        {
            sinal = "";
        }

        Console.WriteLine("Nota conceitual: " + letra + sinal);

        if (nota >= 70)
        {
            Console.WriteLine("Parabéns! Você foi aprovado no curso!");
        }
        else
        {
            Console.WriteLine("Não foi dessa vez, mas não desanime. Na próxima você consegue!");
        }
    }
}