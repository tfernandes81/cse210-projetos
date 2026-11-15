using System.Linq;
using System.Text;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Representa uma palavra da escritura, que pode estar oculta ou visível.
    /// A pontuação colada à palavra (vírgulas, pontos...) continua sempre visível.
    /// </summary>
    public class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        /// <summary>Palavras formadas só por pontuação (ex.: "—") não contam como palavras.</summary>
        public bool HasLetters => _text.Any(char.IsLetterOrDigit);

        public void Hide() => _isHidden = true;

        public void Show() => _isHidden = false;

        /// <summary>
        /// Retorna o texto a ser exibido. Se oculta, troca letras por "_".
        /// Com a dica ligada, a primeira letra continua visível.
        /// </summary>
        public string GetDisplayText(bool showFirstLetterHint)
        {
            if (!_isHidden)
            {
                return _text;
            }

            var result = new StringBuilder();
            bool firstLetterPending = showFirstLetterHint;

            foreach (char c in _text)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    result.Append(c);
                }
                else if (firstLetterPending)
                {
                    result.Append(c);
                    firstLetterPending = false;
                }
                else
                {
                    result.Append('_');
                }
            }

            return result.ToString();
        }
    }
}