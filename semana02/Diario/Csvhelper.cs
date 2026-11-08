// Funções para escrever CSV corretamente (vírgulas, aspas e quebras de linha no conteúdo).
public static class CsvHelper
{
    // Separador ';' é o padrão do Excel em português do Brasil (abre direto, sem assistente).
    public const char Separator = ';';

    // Envolve o campo em aspas quando necessário e duplica as aspas internas.
    public static string Escape(string field)
    {
        if (field.IndexOfAny(new[] { Separator, '"', '\n', '\r' }) >= 0)
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        return field;
    }

    public static string BuildLine(params string[] fields)
    {
        var escaped = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            escaped[i] = Escape(fields[i]);
        }
        return string.Join(Separator.ToString(), escaped);
    }
}