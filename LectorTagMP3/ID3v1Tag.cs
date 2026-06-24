namespace EntornoID3v1Tag
{
    // enum Genero
    // {
    //     Pop = 'p',
    //     Rock = 'r',
    //     Metal = 'm'
    // }
    public class ID3v1Tag
    {
        string header;// = "TAG";
        string titulo;
        string artista;
        string album;
        string anio;
        string comentario;
        string genero;

        public ID3v1Tag(string header, string titulo, string artista, string album, string anio, string comentario, string genero)
        {
            this.header = header;
            this.titulo = titulo;
            this.artista = artista;
            this.album = album;
            this.anio = anio;
            this.comentario = comentario;
            this.genero = genero;
        }

        public string Header { get => header; }
        public string Titulo { get => titulo; }
        public string Artista { get => artista; }
        public string Album { get => album; }
        public string Anio { get => anio; }
        public string Comentario { get => comentario; }
        public string Genero { get => genero; }
    }
}