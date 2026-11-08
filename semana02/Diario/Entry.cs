using System;

// Representa UM registro do diário.
// Abstração: quem usa a classe só chama Display(); como ele é formatado fica escondido aqui.
public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;
    private int _mood; // 1 a 5 (0 = não informado)

    // Texto usado no lugar da pergunta quando o registro é de escrita livre.
    public const string FreeWritingLabel = "Escrita livre";

    public Entry(string date, string prompt, string response, int mood = 0)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _mood = (mood >= 0 && mood <= 5) ? mood : 0;
    }

    public string Date => _date;
    public string Prompt => _prompt;
    public string Response => _response;
    public int Mood => _mood;
    public bool IsFreeWriting => _prompt == FreeWritingLabel;

    public void Display()
    {
        Console.WriteLine($"Data: {_date}");
        if (_mood > 0)
        {
            Console.WriteLine($"Humor do dia: {_mood}/5");
        }
        if (IsFreeWriting)
        {
            Console.WriteLine($"Tipo: {FreeWritingLabel}");
            Console.WriteLine($"Texto:\n{_response}");
        }
        else
        {
            Console.WriteLine($"Pergunta: {_prompt}");
            Console.WriteLine($"Resposta: {_response}");
        }
        Console.WriteLine();
    }
}