using System;
using System.Collections.Generic;

/*
 * ============================================================
 *  PROGRAMA DE DIÁRIO - ABSTRAÇÃO
 * ============================================================
 *  Princípio da abstração aplicado: cada classe esconde seus detalhes
 *  internos (variáveis membro privadas) e expõe só métodos simples.
 *   - Entry:           guarda data, pergunta, resposta e humor em campos
 *                      privados; quem usa só chama Display().
 *   - PromptGenerator: guarda a lista de perguntas e o Random de forma
 *                      privada; quem usa só chama GetRandomPrompt().
 *   - Journal:         guarda a lista de registros de forma privada; quem
 *                      usa só chama AddEntry(), DisplayAll() e SaveToFile().
 *   - CsvHelper:       esconde as regras de escrita de CSV.
 *   - Program:         só cuida do menu e da conversa com o usuário; não
 *                      sabe como os dados são guardados ou formatados.
 *
 * ============================================================
 *  O QUE EU FIZ PARA IR ALÉM DOS REQUISITOS
 * ============================================================
 *  1. Salvar em CSV de verdade (abre no Excel): campos com
 *     ponto e vírgula, aspas ou quebras de linha são tratados
 *     corretamente (classe CsvHelper). Uso ';' como separador porque
 *     é o padrão do Excel em português, e gravo em UTF-8 com BOM
 *     para os acentos aparecerem certos.
 *  2. Novo campo no registro: "humor do dia" (nota de 1 a 5, opcional),
 *     salvo junto com os demais dados.
 *  3. Problema "não sei o que escrever": se a pergunta sorteada não
 *     agradar, o usuário digita "outra" e recebe uma nova pergunta
 *     (que nunca repete a anterior). Também há 10 perguntas na lista
 *     (o requisito pedia 5).
 *  4. Data de hoje preenchida automaticamente, e o usuário pode deixar
 *     a resposta em branco para cancelar sem salvar nada.
 *  5. Tratamento de erros ao salvar (sem permissão, nome inválido etc.)
 *     para o programa nunca fechar com exceção.
 *  6. Se a entrada do teclado for encerrada, o programa sai sem travar.
 *  7. Código organizado em um arquivo por classe, com 5 classes
 *     (o requisito pedia pelo menos 2 além de Program).
 *  8. MODO LIVRE: opção "Escrever livremente" no menu. Não faz pergunta;
 *     o usuário escreve o que estiver em seu coração, em várias linhas,
 *     e termina com Enter em uma linha vazia. O registro é salvo com o
 *     tipo "Escrita livre" no lugar da pergunta. O código de pedir o
 *     humor foi separado no método AskMood() para ser reaproveitado.
 *  9. Decisão de design: a opção de carregar o diário de um arquivo foi
 *     removida a pedido do usuário; o diário é um registro simples que
 *     é escrito, exibido e salvo.
 * ============================================================
 */
public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Journal journal = new Journal();
        PromptGenerator generator = new PromptGenerator();

        Console.WriteLine("Bem-vindo ao Programa de Diário!");

        int choice = 0;
        while (choice != 5)
        {
            Console.WriteLine("\nEscolha uma das opções a seguir:");
            Console.WriteLine("1. Escrever com uma pergunta");
            Console.WriteLine("2. Escrever livremente");
            Console.WriteLine("3. Exibir");
            Console.WriteLine("4. Salvar");
            Console.WriteLine("5. Sair");
            Console.Write("O que você gostaria de fazer? ");

            var input = Console.ReadLine();
            if (input == null)
            {
                // Entrada encerrada (Ctrl+Z / Ctrl+D): sai em vez de repetir o menu para sempre.
                Console.WriteLine("\nEntrada encerrada. Até logo!");
                break;
            }

            if (!int.TryParse(input, out choice))
            {
                choice = 0;
            }

            switch (choice)
            {
                case 1: WriteEntry(journal, generator); break;
                case 2: WriteFreeEntry(journal); break;
                case 3: journal.DisplayAll(); break;
                case 4: SaveJournal(journal); break;
                case 5: Console.WriteLine("Até logo! Continue registrando seus dias."); break;
                default: Console.WriteLine("Opção inválida. Digite um número de 1 a 5."); break;
            }
        }
    }

    private static void WriteEntry(Journal journal, PromptGenerator generator)
    {
        string prompt = generator.GetRandomPrompt();
        string response;

        while (true)
        {
            Console.WriteLine($"\n{prompt}");
            Console.Write("(digite 'outra' para trocar a pergunta, ou deixe em branco para cancelar)\n> ");
            response = Console.ReadLine() ?? "";

            if (response.Trim().ToLower() == "outra")
            {
                prompt = generator.GetRandomPrompt();
                continue;
            }
            break;
        }

        if (string.IsNullOrWhiteSpace(response))
        {
            Console.WriteLine("Registro cancelado.");
            return;
        }

        int mood = AskMood();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        journal.AddEntry(new Entry(date, prompt, response, mood));
        Console.WriteLine("Registro adicionado!");
    }

    // Modo livre: sem pergunta, o usuário escreve o que quiser, em quantas linhas quiser.
    private static void WriteFreeEntry(Journal journal)
    {
        Console.WriteLine("\nModo livre: escreva o que estiver em seu coração.");
        Console.WriteLine("Você pode usar várias linhas. Para terminar, aperte Enter em uma linha vazia.");
        Console.WriteLine("(Se não escrever nada, o registro é cancelado.)\n");

        var lines = new List<string>();
        while (true)
        {
            var line = Console.ReadLine();
            if (line == null || line.Trim() == "")
            {
                break;
            }
            lines.Add(line);
        }

        if (lines.Count == 0)
        {
            Console.WriteLine("Registro cancelado.");
            return;
        }

        string text = string.Join("\n", lines);
        int mood = AskMood();
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        journal.AddEntry(new Entry(date, Entry.FreeWritingLabel, text, mood));
        Console.WriteLine("Registro adicionado!");
    }

    // Pergunta o humor do dia (opcional). Devolve 0 se o usuário pular ou digitar algo inválido.
    private static int AskMood()
    {
        Console.Write("De 1 a 5, como foi seu dia? (Enter para pular) ");
        int.TryParse(Console.ReadLine(), out int mood);
        return mood;
    }

    private static void SaveJournal(Journal journal)
    {
        Console.Write("Qual é o nome do arquivo? (ex.: diario.csv) ");
        string file = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(file))
        {
            Console.WriteLine("Nome de arquivo inválido.");
            return;
        }

        try
        {
            journal.SaveToFile(file);
            Console.WriteLine($"Diário salvo em '{file}' ({journal.Count} registros).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Não foi possível salvar: {ex.Message}");
        }
    }
}