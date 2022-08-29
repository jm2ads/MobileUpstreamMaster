using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Business.InventariosMasivos
{
    public class InventarioMasivo : SyncLocalEntity
    {
        public string NumeroProvisorio { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public EstadoInventario Estado { get; set; }

        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }
        [ManyToOne(foreignKey: "IdCentro", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleInventarioMasivo> DetallesInventarioMasivo { get; set; }

        [ManyToMany(typeof(InventarioMasivoAlmacen), CascadeOperations = CascadeOperation.All)]        
        public List<Almacen> AlmacenesExcluidos { get; set; }

        public InventarioMasivo()
        {
        }
    }
}
