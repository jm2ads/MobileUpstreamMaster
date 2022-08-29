using Frontend.Business.Centros;
using Frontend.Business.InventariosMasivos;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Almacenes
{
    public class Almacen : SyncEntity
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ManyToMany(typeof(InventarioMasivoAlmacen), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<InventarioMasivo> InventariosMasivos { get; set; }

        [Ignore]
        public string DisplayDescription => string.Format("{0} - {1}", Codigo, Nombre);
    }
}
