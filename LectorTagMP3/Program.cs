
using System.Text;
using EntornoID3v1Tag;

Console.WriteLine("\n***Aplicación de consola en C# para leer el tag de un archivo MP3***\n");

/*
La Estructura Tag ID3v1 (son los últimos 128 bytes del archivo): 

Campo       | Offset (Desde el inicio del tag) | Longitud (Bytes) |  Descripción  

Header                       0                           3           Siempre debe ser la cadena "TAG" 
Título                       3                          30           Título de la canción 
Artista                     33                          30           Nombre del artista 
Álbum                       63                          30           Nombre del álbum 
Año                         93                           4           Año de publicación como texto (ej: "2025") 
Comentario                  97                          30           Comentario 
Género                     127                           1           Un byte que representa un código de género 

El programa deberá leer esta información, cargar en una instancia de una clase Id3v1Tag y luego 
mostrar por consola el título, artista, álbum y año de la canción. 
*/

// Guardo en una cadena el direcotrio completo del archivo MP3
string nombreArchivoMP3 = @"C:\Ing_Informatica_2026\Repositorio-Taller-de-Lenguajes-I-2026\tl1-tp9-2026-irinanicole\LectorTagMP3\Philoeidos - El Intersticio.mp3";

// Creo el objeto / Genero una instancia de FileStream para abrir (solo leer, no modificar) el archivo MP3
FileStream miArchivo = new FileStream(nombreArchivoMP3, FileMode.Open);

// Creo el buffer donde guardaré los ultimos 128bits del arhcivoMP3
byte[] buffer = new byte[128];

// Genero un objeto FileInfo para obtener informacion del tamaño del archivo y luego guardar 
FileInfo mp3Info = new FileInfo(nombreArchivoMP3);
// int posicion_final = (int)mp3Info.Length;
// int posicion_inicial = (int)(mp3Info.Length - 128); // para empezar desde los ultimos 128 bits

// Me posiciono en el lugar donde el archivo guarda la info qeu me interesa (los ultimos 128 bytes)
miArchivo.Seek(-128, SeekOrigin.End);
// guardo esos ultimos 128 bits del archivo en el buffer
int cant_bytes_leidos = miArchivo.Read(buffer, 0, 128);

if (cant_bytes_leidos == 128)
{
    string header = Encoding.Default.GetString(buffer, 0, 3);
    string titulo = Encoding.Default.GetString(buffer, 3, 30);
    string artista = Encoding.Default.GetString(buffer, 33, 30);
    string album = Encoding.Default.GetString(buffer, 63, 30);
    string anio = Encoding.Default.GetString(buffer, 93, 4);
    string comentario = Encoding.Default.GetString(buffer, 97, 30);
    string genero = Encoding.Default.GetString(buffer, 127, 1);

    ID3v1Tag nuevoTag = new ID3v1Tag(header, titulo, artista, album, anio, comentario, genero);

    Console.WriteLine("--- TAG MP3 ---");
    Console.WriteLine($"Título: {nuevoTag.Titulo}");
    Console.WriteLine($"Artista: {nuevoTag.Artista}");
    Console.WriteLine($"Álbum: {nuevoTag.Album}");
    Console.WriteLine($"Año: {nuevoTag.Anio}");
}