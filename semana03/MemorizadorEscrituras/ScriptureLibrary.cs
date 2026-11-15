using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Biblioteca de escrituras carregada de um arquivo de texto.
    /// Formato de cada linha:  Referência|Texto   (linhas começando com # são comentários)
    /// </summary>
    public class ScriptureLibrary
    {
        private readonly List<(Reference Reference, string Text)> _entries = new();
        private readonly Random _random = new Random();
        private int _lastIndex = -1;

        public int Count => _entries.Count;

        /// <summary>Carrega escrituras do arquivo. Linhas inválidas são ignoradas.</summary>
        public void LoadFromFile(string path)
        {
            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
                {
                    continue;
                }

                string[] parts = line.Split('|', 2);
                if (parts.Length != 2)
                {
                    continue;
                }

                try
                {
                    _entries.Add((Reference.Parse(parts[0]), parts[1].Trim()));
                }
                catch (FormatException)
                {
                    // Linha mal formatada: ignora e segue.
                }
            }
        }

        /// <summary>Adiciona escrituras padrão, usadas caso o arquivo não exista.</summary>
        public void AddDefaults()
        {
            _entries.Add((new Reference("João", 3, 16),
                "Porque Deus amou o mundo de tal maneira que deu o seu Filho unigênito, " +
                "para que todo aquele que nele crê não pereça, mas tenha a vida eterna."));
            _entries.Add((new Reference("Provérbios", 3, 5, 6),
                "Confia no Senhor de todo o teu coração e não te estribes no teu próprio entendimento. " +
                "Reconhece-o em todos os teus caminhos, e ele endireitará as tuas veredas."));
        }

        /// <summary>Sorteia uma escritura, evitando repetir a anterior quando possível.</summary>
        public Scripture GetRandomScripture()
        {
            if (_entries.Count == 0)
            {
                throw new InvalidOperationException("A biblioteca está vazia.");
            }

            int index;
            do
            {
                index = _random.Next(_entries.Count);
            } while (_entries.Count > 1 && index == _lastIndex);

            _lastIndex = index;
            var entry = _entries[index];
            return new Scripture(entry.Reference, entry.Text);
        }
    }
}