using Frontend.Business.Attributes;
using Frontend.Business.Funcionalidades;
using Frontend.Business.Synchronizer;
using Frontend.Business.Usuarios.Dispositivos;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Usuarios
{
    [IgnoreDbReset]
    public class Usuario : SyncLocalEntity
    {
        public string Nombre { get; set; }

        [Ignore]
        public string Contraseña { get; set; }

        public string IdRed { get; set; }

        public string Token { get; set; }

        public string Pin { get; set; }

        [ForeignKey(typeof(Dispositivo))]
        public int DispositivoId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Dispositivo Dispositivo { get; set; }

        [ManyToMany(typeof(UsuarioFuncionalidad), CascadeOperations = CascadeOperation.All)]
        public List<Funcionalidad> Funcionalidades { get; set; }
    }
}
