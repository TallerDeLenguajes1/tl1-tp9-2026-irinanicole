# TP 9: Archivos
Bourdette, Irina Nicole

## Csharp: Clases (Directory, Files, Stream)

### DIRECTORY

Directory proporciona métodos estáticos para crear, mover y recorrer directorios y subdirectorios.

#### Comprobando la existencia del directorio: Directory.Exists(path)
- El método devuelve verdadero si la ruta especificada corresponde a un directorio existente.
- Realiza una simple comprobación booleana sin generar excepciones para rutas no válidas. Esto permite usarlo de forma segura antes de intentar otras operaciones con directorios.
- Siempre verificar la existencia del directorio antes de realizar operaciones que puedan fallar.
El siguiente ejemplo muestra cómo comprobar si existe un directorio:
```C#
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Temp\TestDirectory";
        
        if (Directory.Exists(path))
        {
            Console.WriteLine("Directory exists.");
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
    }
}
```

#### Creación de un directorio: Directory.CreateDirectory(path)
- El método crea todos los directorios y subdirectorios en la ruta especificada que no existan.
- Si el directorio ya existe, el método no realiza ninguna acción.
- Este método devuelve un **DirectoryInfo _objeto_** que representa el directorio creado. No hay el valor de retorno aquí, pero está disponible para operaciones posteriores.
El siguiente ejemplo muestra cómo crear un nuevo directorio (incluye un manejo básico de errores para casos en los que la creación pueda fallar):
```C#
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Temp\NewDirectory";
        
        try
        {
            Directory.CreateDirectory(path);
            Console.WriteLine("Directory created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

#### Obtención de archivos de directorio: Directory.GetFiles(path)
- Devuelve los nombres de todos los archivos en el directorio especificado en una matriz de rutas completas.
- Por defecto, no busca en los subdirectorios.
- El método cuenta con sobrecargas para patrones y opciones de búsqueda.
El siguiente ejemplo muestra cómo obtener todos los archivos en un directorio (incluye el manejo básico de errores para casos en los que el directorio podría no ser accesible, resultando útil para operaciones de listado de directorios):
```C#
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Temp";
        
        try
        {
            string[] files = Directory.GetFiles(path);
            
            Console.WriteLine("Files in directory:");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

#### Obtener subdirectorios: Directory.GetDirectories(path)
- Devuelve los nombres de todos los subdirectorios del directorio especificado en una matriz de rutas completas.
- Al igual que _GetFiles()_, tiene sobrecargas para patrones de búsqueda y opciones.
- El método no recorre los subdirectorios de forma recursiva por defecto.
- Las rutas devueltas están completamente cualificadas.
El siguiente ejemplo muestra cómo obtener todos los subdirectorios de un directorio (incluye el manejo básico de errores en un recorrido básico de directorios para casos en los que el directorio podría no ser accesible):
```C#
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Temp";
        
        try
        {
            string[] dirs = Directory.GetDirectories(path);
            
            Console.WriteLine("Subdirectories:");
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

#### Traslado de un directorio: Directory.Move(pathOrigen, pathDestino)
- El método mueve un directorio y su contenido a una nueva ubicación. El destino no debe existir.
- La operación es atómica en la misma unidad. Entre unidades, se trata de una operación de copiar y borrar.
- Este método mueve todos los subdirectorios, si los hay.
El siguiente ejemplo muestra cómo mover un directorio (incluye el manejo básico de errores para casos en los que la operación podría fallar, resultando útil para la reorganización de directorios):
```C#
using System; 
using System.IO; 

class Program 
{ 
    static void Main() 
    { 
        string sourceDir = @"C:\Temp\OldDir"; 
        string destDir = @"C:\Temp\NewDir"; 
        
        try 
        { 
            Directory.Move(sourceDir, destDir); 
            Console.WriteLine("Directorio movido correctamente."); 
        } 
        catch (Exception ex) 
        { 
            Console.WriteLine($"Error: {ex.Message}"); 
        } 
    } 
}
```

#### Eliminar un directorio: Directory.Delete(path, recursive_mode)
- El método elimina el directorio especificado y puede hacerlo de forma recursiva o no.
- El parámetro recursivo determina si se deben eliminar los subdirectorios y archivos. Sin él, el directorio debe estar vacío.
- La operación es atómica en la misma unidad. Entre unidades, se trata de una operación de copiar y borrar.
- Este método mueve todos los subdirectorios, si los hay.
El siguiente ejemplo muestra  la eliminación de un directorio con la opción recursiva habilitada (incluye el manejo básico de errores para casos en los que la eliminación podría fallar, resultando útil para las operaciones de limpieza):
```C#
using System; 
using System.IO; 

class Program 
{ 
    static void Main() 
    { 
        string path = @"C:\Temp\DirectoryToDelete"; 
        
        try 
        { 
            Directory.Delete(path, recursive: true); 
            Console.WriteLine("Directorio eliminado correctamente."); 
        } 
        catch (Exception ex) 
        { 
            Console.WriteLine($"Error: {ex.Message}"); 
        } 
    } 
}
```

#### Obtener información del directorio:
- La Directory() clase proporciona métodos para obtener varias marcas de tiempo. Estas incluyen la hora de creación, la hora del último acceso y la hora de la última modificación. Las horas se devuelven como DateTimevalores.
- El manejo de errores garantiza que el programa no falle si el directorio es inaccesible. Esto resulta útil para la monitorización y el registro de directorios.
El siguiente ejemplo muestra la recuperación básica de metadatos de un directorio, es decir, cómo obtener las horas de creación, acceso y escritura de un directorio utilizando diferentes métodos de clase _Directory()_:
```C#
using System; 
using System.IO; 

class Program 
{ 
    static void Main() 
    { 
        string path = @"C:\Temp"; 
        
        try 
        { 
            Console.WriteLine($"Hora de creación: {Directory.GetCreationTime(path)}"); 
            Console.WriteLine($"Hora del último acceso: {Directory.GetLastAccessTime(path)}"); 
            Console.WriteLine($"Hora de la última escritura: {Directory.GetLastWriteTime(path)}"); 
        } 
        catch (Exception ex) 
        { 
            Console.WriteLine($"Error: {ex.Message}"); 
        } 
    } 
}
```