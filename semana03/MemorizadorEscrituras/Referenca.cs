using System;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Representa a referência de uma escritura, como "João 3:16" ou "Provérbios 3:5-6".
    /// </summary>
    public class Reference
    {
        private readonly string _book;
        private readonly int _chapter;
        private readonly int _startVerse;
        private readonly int _endVerse;

        /// <summary>Construtor para um único versículo (ex.: "João 3:16").</summary>
        public Reference(string book, int chapter, int verse)
            : this(book, chapter, verse, verse)
        {
        }

        /// <summary>Construtor para um intervalo de versículos (ex.: "Provérbios 3:5-6").</summary>
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                throw new ArgumentException("O livro não pode ser vazio.", nameof(book));
            }
            if (endVerse < startVerse)
            {
                throw new ArgumentException("O versículo final não pode ser menor que o inicial.");
            }

            _book = book.Trim();
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        /// <summary>
        /// Cria uma referência a partir de um texto como "1 Coríntios 13:4-7".
        /// </summary>
        public static Reference Parse(string text)
        {
            text = text.Trim();
            int colon = text.LastIndexOf(':');
            int space = colon > 0 ? text.LastIndexOf(' ', colon) : -1;

            if (colon < 0 || space < 0)
            {
                throw new FormatException($"Referência inválida: \"{text}\".");
            }

            string book = text.Substring(0, space);
            int chapter = int.Parse(text.Substring(space + 1, colon - space - 1));
            string verses = text.Substring(colon + 1).Trim();

            int dash = verses.IndexOf('-');
            if (dash < 0)
            {
                return new Reference(book, chapter, int.Parse(verses));
            }

            int start = int.Parse(verses.Substring(0, dash));
            int end = int.Parse(verses.Substring(dash + 1));
            return new Reference(book, chapter, start, end);
        }

        /// <summary>Livro (usado para evitar repetir a mesma escritura em sequência, etc.).</summary>
        public string Book => _book;

        public override string ToString()
        {
            return _startVerse == _endVerse
                ? $"{_book} {_chapter}:{_startVerse}"
                : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}