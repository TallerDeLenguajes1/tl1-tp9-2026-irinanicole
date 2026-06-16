/*  solicitar al usuario que ingrese el path de un directorio que desea analizar.  */
bool pathExiste = false;
Console.WriteLine("Ingresar el PATH del directorio que desea analizar:");
//string? path = Console.ReadLine();
string? path = @"C:\Users\irina\Documents\MN-I-2026";
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
}
