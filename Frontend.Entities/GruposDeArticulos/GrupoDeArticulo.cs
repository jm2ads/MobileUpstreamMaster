using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.GruposDeArticulos
{
    public class GrupoDeArticulo : SyncEntity
    {
        public string Codigo { get; set; }

        //[ForeignKey(typeof(Material))]
        //public int IdMaterial { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public IList<Material> Materiales { get; set; }
    }
}
