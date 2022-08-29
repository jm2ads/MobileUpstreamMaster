using Frontend.Business.Attributes;
using Frontend.Business.Synchronizer;

namespace Frontend.Business.Usuarios.Dispositivos
{
    [IgnoreDbReset]
    public class Dispositivo : SyncLocalEntity
    {
        public string Fabricante { get; set; }

        public string Plataforma { get; set; }

        public string Modelo { get; set; }

        public string Serial { get; set; }

        public string Version { get; set; }

        public string Uuid { get; set; }
    }
}
