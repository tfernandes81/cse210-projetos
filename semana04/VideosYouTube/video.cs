using System.Collections.Generic;

public class Video
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int DuracaoEmSegundos { get; set; }
    private List<Comentario> _comentarios = new List<Comentario>();

    public Video(string titulo, string autor, int duracaoEmSegundos)
    {
        Titulo = titulo;
        Autor = autor;
        DuracaoEmSegundos = duracaoEmSegundos;
    }

    public void AdicionarComentario(Comentario comentario)
    {
        _comentarios.Add(comentario);
    }

    public int ObterNumeroDeComentarios()
    {
        return _comentarios.Count;
    }

    public List<Comentario> ObterComentarios()
    {
        return _comentarios;
    }
}