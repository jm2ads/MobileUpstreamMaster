
namespace Frontend.WebApi.DTOs.Users
{
    public class UserAutenticationResponseDto
    {
        public int ErrType { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public Data Data { get; set; }
    }
}
