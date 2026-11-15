using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Representa uma escritura: uma referência e o texto dividido em palavras.
    /// </summary>
    public class Scripture
    {
        private readonly Reference _reference;
        private readonly List<Word> _words;
        private readonly Random _random = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new Word(w))
                .ToList();
        }

        public Reference Reference => _reference;

        /// <summary>Quantidade total de palavras (ignora símbolos soltos).</summary>
        public int TotalWords => _words.Count(w => w.HasLetters);

        /// <summary>Porcentagem (0-100) de palavras já ocultas.</summary>
        public int PercentHidden
        {
            get
            {
                int total = TotalWords;
                if (total == 0) return 100;
                int hidden = _words.Count(w => w.HasLetters && w.IsHidden);
                return hidden * 100 / total;
            }
        }

        /// <summary>
        /// Oculta até 'count' palavras escolhidas aleatoriamente
        /// entre as que AINDA NÃO estão ocultas (desafio adicional).
        /// </summary>
        public void HideRandomWords(int count)
        {
            var visible = _words.Where(w => w.HasLetters && !w.IsHidden).ToList();

            // Embaralha (Fisher-Yates) e oculta as primeiras 'count'.
            for (int i = visible.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (visible[i], visible[j]) = (visible[j], visible[i]);
            }

            foreach (Word word in visible.Take(count))
            {
                word.Hide();
            }
        }

        public bool IsCompletelyHidden()
        {
            return _words.Where(w => w.HasLetters).All(w => w.IsHidden);
        }

        public string GetDisplayText(bool showFirstLetterHint)
        {
            string text = string.Join(" ", _words.Select(w => w.GetDisplayText(showFirstLetterHint)));
            return $"{_reference} - {text}";
        }
    }
}