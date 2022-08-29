namespace Frontend.Azure.DTOs
{
    public class UsuarioInputDto
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

        public string IdRed { get; set; }

        public string Token { get; set; }

        public DispositivoInputDto Dispositivo { get; set; }
    }
}
