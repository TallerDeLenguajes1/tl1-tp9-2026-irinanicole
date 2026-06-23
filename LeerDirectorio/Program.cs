
/*  solicitar al usuario que ingrese el path de un directorio que desea analizar.  */
bool pathExiste = false;
Console.WriteLine("Ingresar el PATH del directorio que desea analizar:");
//string? path = Console.ReadLine();
string? path = @"C:\Ing_Informatica_2026\Repositorio-Taller-de-Lenguajes-I-2026\tl1-tp9-2026-irinanicole\LeerDirectorio\pruebaDirectorio";

/* validar si el directorio ingresado existe. Si no existe, deberá 
notificar al usuario y solicitarle que ingrese un path válido nuevamente. */
while ( !pathExiste )
{
    while ( string.IsNullOrEmpty(path) )
    {  
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("La cadena ingresada NO es valida. Ingresar otra: ");
        Console.ResetColor();
        path = Console.ReadLine();
    }
    pathExiste = Directory.Exists(path);
    if (!pathExiste)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("El directorio ingresado NO EXISTE. Ingresar otro: ");
        Console.ResetColor();
        path = Console.ReadLine();
    }
}
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nEl directorio ingresado EXISTE");
Console.ResetColor();

DirectoryInfo dirIngresado = new DirectoryInfo(path);

/* Una vez que se ha proporcionado un directorio válido, la aplicación deberá listar en la consola: */
/* i) Todas las *carpetas* que se encuentran en ese path (solo el nombre de carpeta) */
if (dirIngresado.Exists)
{
    string[] carpetas = Directory.GetDirectories(path);
    int cant_Carpetas = carpetas.Length;
    Console.WriteLine($"\nTodas las CARPETAS dentro de '{dirIngresado.Name}':");
    DirectoryInfo[] dirInfo = new DirectoryInfo[cant_Carpetas];
    for (int i=0; i < cant_Carpetas; i++)
    {
        dirInfo[i] = new DirectoryInfo(carpetas[i]);
        if (dirInfo[i].Exists)
        {
            Console.WriteLine(dirInfo[i].Name);
        }
    }

    /* ii) Todos los *archivos* que se encuentran directamente en esa carpeta */
    // (Junto a cada nombre de archivo, se deberá mostrar su tamaño en kilobytes [KB])
    string[] archivos = Directory.GetFiles(path);
    int cant_Archivos = archivos.Length;
    Console.WriteLine($"\nTodas los ARCHIVOS dentro de '{dirIngresado.Name}':");
    FileInfo[] filesInfo = new FileInfo[cant_Archivos];
    for (int i=0; i < cant_Archivos; i++)
    {
        filesInfo[i] = new FileInfo(archivos[i]);
        if (filesInfo[i].Exists)
        {
            Console.WriteLine($"Nombre: {filesInfo[i].Name} --> Tamaño: {filesInfo[i].Length} bytes");
        }
    }

    /* Después de listar los archivos, el programa creará un archivo con extensión csv, 
    llamado "reporte_archivos.csv" en el mismo directorio que se está analizando. */
    // ej: string archivoCSV = Path.Combine(ruta, "reporte_archivos.csv");

    // 1. Genero exactamente la ruta / directorio que tendrá el archivo CSV
    string archivoCSV = Path.Combine(path, "reporte_archivos.csv");
    
    // Para vaciar o (inicializar) el archivo antes de trabajar en él:
    //File.WriteAllText(archivoCSV, "");

    // Para borrarlo:
    //File.Delete(archivoCSV);

    // 2. Utilizo la clase 'System.IO.File.WriteAllLines()' para crear el archivo CSV, 
    // pero primero creo el ENCABEZADO del archivo
    string[] encabezadoArchivoCSV =
    {
        "Nombre del Archivo     |    Tamaño (KB)    |    Fecha de Última Modificación"
    };

    File.WriteAllLines(archivoCSV, encabezadoArchivoCSV); // crea el archivo y agrega la primera line-encabezado

    // Agrego la nueva linea usando la clase FILE-STREAM, con el método StreamWriter(): ==> esto también debería crear al archivo en caso de no existir... no?
    // using ( StreamWriter escribir = new StreamWriter( archivoCSV , true ) )
    // {
    //     escribir.WriteLine(encabezadoArchivoCSV);
    // }

    /*
    Este archivo CSV deberá contener la siguiente información en columnas separadas: 
        ■ Nombre del Archivo: El nombre completo del archivo (incluyendo su 
        extensión). 
        ■ Tamaño (KB): El tamaño del archivo, redondeado a dos decimales. 
        ■ Fecha de Última Modificación: La fecha y hora en que el archivo fue 
        modificado por última vez. 
    */
    FileInfo csvInfo = new FileInfo(archivoCSV); // genero un FileInfo del CSV para acceder a sus datos

    const double KB = 1024;

    // Genero una nuevaLinea con los datos a llenar: Nombre.csv | Tamaño(KB) | Fecha Última Modificación
    string nuevaLinea = csvInfo.Name + "    |    " + Math.Round(csvInfo.Length/KB, 2).ToString() + "    |    " + csvInfo.LastWriteTime.ToString("dd/MM/yyyy HH:mm");

    // Agrego la nueva linea usando la clase FILE-STREAM, con el método StreamWriter():
    using ( StreamWriter escribir = new StreamWriter( archivoCSV , true ) )
    {
        escribir.WriteLine(nuevaLinea);
    }

    // Si lo que me interesa es agregar varias lineas de una sola vez (por ejemplo n líneas):
    // List<string> nuevasLineas = new List<string>()
    // {
    //     csvInfo.Name + "    |    " + Math.Round(csvInfo.Length/KB, 2).ToString() + "    |    " + csvInfo.LastWriteTime.ToString("dd/MM/yyyy HH:mm"),
    //     "linea 2",
    //     "linea 3",
    //     "...",
    //     "linea n",
    // };
    // File.AppendAllLines(archivoCSV, nuevasLineas);
    
    // Utilizo StreamReader() para leer el archivo linea por linea
    Console.WriteLine("\n\n---- ARCHIVO CSV ----\n");
    using ( StreamReader leer = new StreamReader( archivoCSV ) )
    {
        string? linea = leer.ReadLine();
        while ( linea != null )
        {
            Console.WriteLine(linea);
            linea = leer.ReadLine();
        }
    }

    // También puedo usar:
    // List<string> lineas = File.ReadAllLines(archivoCSV).ToList(); // en el caso de usar listas

    // int cantidadLineas = File.ReadLines(archivoCSV).Count();
}