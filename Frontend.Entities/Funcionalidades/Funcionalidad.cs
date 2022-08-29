using Frontend.Business.Attributes;
using Frontend.Business.Synchronizer;
using Frontend.Business.Usuarios;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Funcionalidades
{
    public class Funcionalidad : SyncEntity
    {
        public string Nombre { get; set; }
        public int Orden { get; set; }

        [ManyToMany(typeof(UsuarioFuncionalidad), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<Usuario> Usuarios { get; set; }
    }
}
