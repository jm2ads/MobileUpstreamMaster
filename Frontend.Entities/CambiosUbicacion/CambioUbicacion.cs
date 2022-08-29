using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.CambiosUbicacion
{
    public class CambioUbicacion : SyncLocalEntity
    {
        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }
        [ManyToOne(foreignKey: "IdCentro", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ManyToMany(typeof(CambioUbicacionAlmacen), CascadeOperations = CascadeOperation.All)]
        public List<Almacen> AlmacenesIncluidos { get; set; }

        [ForeignKey(typeof(Material))]
        public int IdMaterial { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }

        public string Ubicacion { get; set; }

        public string Usuario { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
