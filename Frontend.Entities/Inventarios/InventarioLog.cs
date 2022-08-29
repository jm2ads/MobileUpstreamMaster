using Frontend.Business.Commons;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Inventarios
{
    public class InventarioLog : LocalEntity
    {
        public int IdRemoto { get; set; }
        public string Label { get; set; }
        public bool Success { get; set; }
        public string Data { get; set; }
        public DateTime FechaCreacion { get; set; }

        [ForeignKey(typeof(InventarioLog))]
        public int ParentInventarioLogId { get; set; }
        [ManyToOne(inverseProperty: "Adicional", CascadeOperations = CascadeOperation.All)]
        public InventarioLog ParentInventarioLog { get; set; }

        [OneToMany(inverseProperty: "ParentInventarioLog", CascadeOperations = CascadeOperation.All)]
        public List<InventarioLog> Adicional { get; set; }
    }
}
