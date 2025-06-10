internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Ingrese la path del directorio que desea analizar: ");
        string path = Console.ReadLine();
        bool directoryExists = Directory.Exists(path);
        while (!directoryExists)
        {
            Console.Write("El path ingresado es erroneo, ingrese uno valido: ");
            path = Console.ReadLine();
            directoryExists = Directory.Exists(path);
        }
        string[] directorios = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);
        string nesting = "├";
        Console.WriteLine("Directorios: ");
        for (int i = 0; i < directorios.Count(); i++)
        {
            if ((i + 1) == directorios.Count())
            {
                nesting = "└";
            }
            Console.WriteLine(nesting + " " + (i + 1) + "- " + directorios[i]);
        }
        nesting = "├";

        List<string> lineasCsv = new List<string>{
            "Nombre del Archivo;Tamaño(KB);Fecha de Última Modificacion"
        };
        Console.WriteLine("Archivos: ");
        for (int i = 0; i < files.Count(); i++)
        {
            if ((i + 1) == files.Count())
            {
                nesting = "└";
            }
            FileInfo fileInfo = new FileInfo(files[i]);
            float length = fileInfo.Length;
            length /= 1024;
            Console.WriteLine(nesting + " " + (i + 1) + "- " + files[i] + " - (" + length.ToString("F2") + " kb)");

            lineasCsv.Add($"{fileInfo.Name};{length};{fileInfo.LastWriteTime.ToString()}");
        }

        string rutaCsv = Path.Combine(path, "reporte_archivos.csv");
        File.WriteAllLines(rutaCsv, lineasCsv);



    }
}