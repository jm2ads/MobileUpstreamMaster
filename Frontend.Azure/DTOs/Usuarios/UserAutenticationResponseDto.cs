namespace Frontend.Azure.DTOs
{
    public class UserAutenticationResponseDto
    {
        public int errType { get; set; }

        public string message { get; set; }

        public bool success { get; set; }

        public Data data { get; set; }
    }
}
