using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string jogarNovamente = "sim";

        while (jogarNovamente == "sim")
        {
            int numeroSecreto = random.Next(1, 101);
            int tentativas = 0;
            bool acertou = false;

            while (!acertou)
            {
                Console.Write("Qual é o seu palpite? ");
                int palpite = int.Parse(Console.ReadLine());
                tentativas++;

                if (palpite < numeroSecreto)
                {
                    Console.WriteLine("Mais alto");
                }
                else if (palpite > numeroSecreto)
                {
                    Console.WriteLine("Mais baixo");
                }
                else
                {
                    Console.WriteLine("Você acertou!");
                    Console.WriteLine($"Você precisou de {tentativas} tentativas");
                    acertou = true;
                }
            }

            Console.WriteLine();
            Console.Write("Gostaria de jogar novamente (sim/não)? ");
            jogarNovamente = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();
        }

        Console.WriteLine("Obrigado por jogar. Até logo.");
    }
}