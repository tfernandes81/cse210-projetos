/*
 * ======================= ALÉM DOS REQUISITOS =======================
 * Para superar as expectativas, adicionei:
 *
 * 1. BIBLIOTECA DE ESCRITURAS: o programa trabalha com várias escrituras e
 *    sorteia uma aleatoriamente (sem repetir a anterior em sequência).
 * 2. CARREGAMENTO DE ARQUIVO: as escrituras vêm de "scriptures.txt"
 *    (formato Referência|Texto). Se o arquivo não existir, usa escrituras padrão.
 * 3. SÓ OCULTA PALAVRAS AINDA VISÍVEIS (desafio adicional): cada rodada
 *    sorteia apenas entre palavras não ocultas, então sempre há progresso.
 * 4. NÍVEIS DE DIFICULDADE: fácil, médio e difícil definem quantas palavras
 *    somem a cada rodada (porcentagem do total).
 * 5. DICA DA PRIMEIRA LETRA: digitar "dica" liga/desliga a exibição da
 *    primeira letra de cada palavra oculta (ex.: "P_____"), ajudando a lembrar.
 * 6. PONTUAÇÃO PRESERVADA: vírgulas e pontos continuam visíveis, mantendo
 *    o "ritmo" da frase, o que ajuda na memorização.
 * 7. BARRA DE PROGRESSO: mostra a porcentagem de palavras ocultas.
 * 8. TREINO CONTÍNUO: ao terminar, o usuário pode treinar outra escritura
 *    ("nova" também troca de escritura a qualquer momento).
 * 9. Referência com Parse() e vários construtores (versículo único e intervalo).
 * ===================================================================
 */

using System;
using System.IO;
using System.Text;

namespace ScriptureMemorizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ScriptureLibrary library = LoadLibrary();
            double hidePercentPerRound = AskDifficulty();
            bool showHint = false;

            bool keepPlaying = true;
            while (keepPlaying)
            {
                Scripture scripture = library.GetRandomScripture();
                int wordsPerRound = Math.Max(1, (int)Math.Ceiling(scripture.TotalWords * hidePercentPerRound));

                keepPlaying = PracticeScripture(scripture, wordsPerRound, ref showHint);
            }

            Console.WriteLine("\nAté a próxima! Continue meditando na Palavra.");
        }

        /// <summary>
        /// Executa o treino de uma escritura. Retorna false se o usuário quiser encerrar o programa.
        /// </summary>
        private static bool PracticeScripture(Scripture scripture, int wordsPerRound, ref bool showHint)
        {
            while (true)
            {
                DisplayScripture(scripture, showHint);

                if (scripture.IsCompletelyHidden())
                {
                    Console.WriteLine("\nParabéns! Você chegou ao fim desta escritura.");
                    Console.Write("Pressione Enter para treinar outra ou digite \"sair\": ");
                    string answer = (Console.ReadLine() ?? "").Trim().ToLower();
                    return answer != "sair";
                }

                Console.WriteLine("\nPressione Enter para continuar, ou digite:");
                Console.WriteLine("  \"dica\" - liga/desliga a primeira letra   \"nova\" - outra escritura   \"sair\" - encerrar");
                Console.Write("> ");
                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                switch (input)
                {
                    case "sair":
                        return false;
                    case "nova":
                        return true;
                    case "dica":
                        showHint = !showHint;
                        break;
                    default:
                        scripture.HideRandomWords(wordsPerRound);
                        break;
                }
            }
        }

        private static void DisplayScripture(Scripture scripture, bool showHint)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText(showHint));
            Console.WriteLine();
            Console.WriteLine($"Progresso: {BuildProgressBar(scripture.PercentHidden)} {scripture.PercentHidden}% oculto" +
                              (showHint ? "   [dica ligada]" : ""));
        }

        private static string BuildProgressBar(int percent)
        {
            const int width = 20;
            int filled = percent * width / 100;
            return "[" + new string('#', filled) + new string('-', width - filled) + "]";
        }

        private static ScriptureLibrary LoadLibrary()
        {
            var library = new ScriptureLibrary();
            string path = Path.Combine(AppContext.BaseDirectory, "scriptures.txt");

            if (File.Exists(path))
            {
                library.LoadFromFile(path);
            }

            if (library.Count == 0)
            {
                library.AddDefaults();
            }

            return library;
        }

        private static double AskDifficulty()
        {
            Console.Clear();
            Console.WriteLine("=== Memorização de Escrituras ===\n");
            Console.WriteLine("Escolha a dificuldade (quantas palavras somem por rodada):");
            Console.WriteLine("  1 - Fácil   (~10%)");
            Console.WriteLine("  2 - Médio   (~20%)");
            Console.WriteLine("  3 - Difícil (~35%)");
            Console.Write("Opção [2]: ");

            return Console.ReadLine()?.Trim() switch
            {
                "1" => 0.10,
                "3" => 0.35,
                _ => 0.20
            };
        }
    }
}