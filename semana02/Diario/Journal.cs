using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Representa o diário inteiro: guarda a lista de registros e sabe exibir e salvar.
public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private const string Header = "Data;Pergunta;Resposta;Humor";

    public int Count => _entries.Count;

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("O diário está vazio.\n");
            return;
        }

        Console.WriteLine();
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    // Salva como CSV (UTF-8 com BOM, para o Excel reconhecer acentos).
    public void SaveToFile(string file)
    {
        using (var writer = new StreamWriter(file, false, new UTF8Encoding(true)))
        {
            writer.WriteLine(Header);
            foreach (Entry e in _entries)
            {
                writer.WriteLine(CsvHelper.BuildLine(
                    e.Date, e.Prompt, e.Response, e.Mood > 0 ? e.Mood.ToString() : ""));
            }
        }
    }
}