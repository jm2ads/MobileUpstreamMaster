namespace Frontend.Azure.DTOs
{
    public class DispositivoInputDto
    {
        public int Id { get; set; }
        public string Fabricante { get; set; }

        public string Plataforma { get; set; }

        public string Modelo { get; set; }

        public string Serial { get; set; }

        public string Version { get; set; }

        public string Uuid { get; set; }
    }
}
