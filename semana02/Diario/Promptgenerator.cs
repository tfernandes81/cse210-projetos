using System;
using System.Collections.Generic;

// Responsável apenas por sortear perguntas.
public class PromptGenerator
{
    private readonly List<string> _prompts = new List<string>
    {
        "Quem foi a pessoa mais interessante com quem interagi hoje?",
        "Qual foi a melhor parte do meu dia?",
        "Como vi a mão do Senhor em minha vida hoje?",
        "Qual foi a emoção mais forte que senti hoje?",
        "Se eu pudesse fazer uma coisa hoje, o que seria?",
        "Qual foi o maior desafio que enfrentei hoje e como lidei com ele?",
        "Pelo que sou grato hoje?",
        "O que aprendi hoje que não sabia ontem?",
        "Qual pequeno gesto de bondade eu fiz ou recebi hoje?",
        "O que eu faria diferente se pudesse viver este dia de novo?"
    };

    private readonly Random _random = new Random();
    private string _lastPrompt = "";

    // Sorteia uma pergunta, evitando repetir a anterior (útil quando o usuário pede "outra").
    public string GetRandomPrompt()
    {
        string prompt;
        do
        {
            prompt = _prompts[_random.Next(_prompts.Count)];
        } while (prompt == _lastPrompt && _prompts.Count > 1);

        _lastPrompt = prompt;
        return prompt;
    }
}