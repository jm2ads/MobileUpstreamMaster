using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class RespuestaDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Success { get; set; }
        public string Data { get; set; }
        public List<RespuestaDto> Adicional { get; set; }
    }
}
