using System.Text;
using EspacioId3v1Tag;

internal class Program
{
    private static void Main(string[] args)
    {
        string nombreArchivo = "dummy.mp3";
        GenerateDummyMp3();



        Id3v1Tag infoMP3 = new Id3v1Tag().Read(nombreArchivo);
        Console.WriteLine("Titulo: " + infoMP3.Titulo);
        Console.WriteLine("Artista: " + infoMP3.Artista);
        Console.WriteLine("Album: " + infoMP3.Album);
        Console.WriteLine("Genero: " + infoMP3.Genero);
        Console.WriteLine("Comentario: " + infoMP3.Comentario);
        Console.WriteLine("Año: " + infoMP3.Año);
    }

    public static void GenerateDummyMp3()
    {
        string nameOfFile = "dummy.mp3";
        string netDirectory = Directory.GetCurrentDirectory();
        netDirectory = netDirectory.Split("bin")[0];
        string path = netDirectory + nameOfFile;
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            // Crear el tag ID3v1
            byte[] tag = new byte[128];

            // "TAG" - los primeros 3 bytes
            Encoding.ASCII.GetBytes("TAG").CopyTo(tag, 0);

            // Título, Artista, Álbum (pueden tener hasta 30 bytes)
            Encoding.ASCII.GetBytes("Corazon de Parapente").CopyTo(tag, 3);
            Encoding.ASCII.GetBytes("P.Diddy").CopyTo(tag, 33);
            Encoding.ASCII.GetBytes("WHITE").CopyTo(tag, 63);
            Encoding.ASCII.GetBytes("1945").CopyTo(tag, 93);
            Encoding.ASCII.GetBytes("All Hail Brittania").CopyTo(tag, 97);

            // Género (4 = Disco)
            tag[127] = 4;

            // Escribimos solo el tag (sin datos de audio)
            fs.Write(tag, 0, 128);
        }
    }

}