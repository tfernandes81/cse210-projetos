using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Vídeo 1
        Video video1 = new Video("Aprendendo C# em 10 Minutos", "Canal Programando", 600);
        video1.AdicionarComentario(new Comentario("Ana Souza", "Explicação muito clara, obrigada!"));
        video1.AdicionarComentario(new Comentario("Carlos Lima", "Me ajudou a entender classes."));
        video1.AdicionarComentario(new Comentario("Beatriz Alves", "Podia ter um vídeo sobre herança também."));
        videos.Add(video1);

        // Vídeo 2
        Video video2 = new Video("Receita de Pão Caseiro", "Cozinha da Maria", 845);
        video2.AdicionarComentario(new Comentario("João Pereira", "Fiz hoje e ficou perfeito!"));
        video2.AdicionarComentario(new Comentario("Fernanda Costa", "Posso usar fermento instantâneo?"));
        video2.AdicionarComentario(new Comentario("Rafael Gomes", "Receita simples e deliciosa."));
        video2.AdicionarComentario(new Comentario("Luciana Rocha", "Já salvei nos favoritos."));
        videos.Add(video2);

        // Vídeo 3
        Video video3 = new Video("Treino de 20 Minutos em Casa", "Fit com Paulo", 1215);
        video3.AdicionarComentario(new Comentario("Marcos Dias", "Treino puxado, mas ótimo!"));
        video3.AdicionarComentario(new Comentario("Patrícia Nunes", "Perfeito para quem tem pouco tempo."));
        video3.AdicionarComentario(new Comentario("Tiago Martins", "Já estou no terceiro dia seguido."));
        videos.Add(video3);

        // Vídeo 4
        Video video4 = new Video("Viagem para o Nordeste: Roteiro Completo", "Mundo Afora", 1530);
        video4.AdicionarComentario(new Comentario("Camila Ribeiro", "As praias são lindas demais!"));
        video4.AdicionarComentario(new Comentario("Eduardo Santos", "Qual o melhor mês para ir?"));
        video4.AdicionarComentario(new Comentario("Juliana Mendes", "Vou usar esse roteiro nas férias."));
        videos.Add(video4);

        // Exibição
        foreach (Video video in videos)
        {
            Console.WriteLine($"Título: {video.Titulo}");
            Console.WriteLine($"Autor: {video.Autor}");
            Console.WriteLine($"Duração: {video.DuracaoEmSegundos} segundos");
            Console.WriteLine($"Número de comentários: {video.ObterNumeroDeComentarios()}");
            Console.WriteLine("Comentários:");

            foreach (Comentario comentario in video.ObterComentarios())
            {
                Console.WriteLine($"  - {comentario.Nome}: {comentario.Texto}");
            }

            Console.WriteLine();
        }
    }
}