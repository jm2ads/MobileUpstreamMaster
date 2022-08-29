using Frontend.Business.Users.Devices;

namespace Frontend.WebApi.DTOs.AplicacionUsuarios
{
    public class AplicacionUsuarioFactory
    {
        public AplicacionUsuario Create(AplicacionUsuario aplicacionUsuario)
        {
            return new AplicacionUsuario()
            {
                Nombre = aplicacionUsuario.Nombre,
                Activo = aplicacionUsuario.Activo
            };
        }
    }
}
