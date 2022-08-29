using Frontend.Business.Commons;

namespace Frontend.Business.Users.Devices
{
    public class AplicacionUsuario : PersistebleEntity
    {
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
