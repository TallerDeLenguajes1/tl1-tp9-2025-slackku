using System.Text;

namespace EspacioId3v1Tag
{
    class Id3v1Tag
    {
        public string? Header { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string? Album { get; set; }
        public string? Año { get; set; }
        public string? Comentario { get; set; }
        public string? Genero { get; set; }
        public Id3v1Tag Read(string nameOfFile)
        {
            string netDirectory = Directory.GetCurrentDirectory();
            netDirectory = netDirectory.Split("bin")[0];
            string path = netDirectory + nameOfFile;
            if (File.Exists(path))
            {
                Console.WriteLine("Archivo existe!");
                using (FileStream fs = File.OpenRead(path))
                {
                    if (fs.Length >= 128)
                    {
                        fs.Seek(-128, SeekOrigin.End);
                        byte[] b = new byte[128];
                        fs.Read(b, 0, 128);

                        string header = Encoding.ASCII.GetString(b, 0, 3);
                        if (header == "TAG")
                        {
                            // Título
                            string titulo = System.Text.Encoding.ASCII.GetString(b, 3, 30).TrimEnd('\0', ' ');
                            // Artista
                            string artista = System.Text.Encoding.ASCII.GetString(b, 33, 30).TrimEnd('\0', ' ');
                            // Álbum
                            string album = System.Text.Encoding.ASCII.GetString(b, 63, 30).TrimEnd('\0', ' ');
                            // Año
                            string año = System.Text.Encoding.ASCII.GetString(b, 93, 4).TrimEnd('\0', ' ');
                            // Comentario
                            string comentario = System.Text.Encoding.ASCII.GetString(b, 97, 30).TrimEnd('\0', ' ');
                            // Género (byte)
                            byte genero = b[127];

                            this.Header = header;
                            this.Titulo = titulo;
                            this.Artista = artista;
                            this.Album = album;
                            this.Año = año;
                            this.Comentario = comentario;
                            this.Genero = genero.ToString();
                        }

                    }
                }

            }

            return this;
        }

    }
}