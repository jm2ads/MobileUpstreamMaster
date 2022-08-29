using Frontend.Business.Commons;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos
{
    public class MovimientoLog : LocalEntity
    {
        public int IdRemoto { get; set; }
        public string NumeroMovimiento { get; set; }
        public string Label { get; set; }
        public bool Success { get; set; }
        public string Data { get; set; }
        public DateTime FechaCreacion { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }

        [Ignore]
        public string DisplayLabel { get { return Label + ' ' + NumeroMovimiento; } }

        [ForeignKey(typeof(MovimientoLog))]
        public int ParentInventarioLogId { get; set; }
        [ManyToOne(inverseProperty: "Adicional", CascadeOperations = CascadeOperation.All)]
        public MovimientoLog ParentMovimientoLog { get; set; }

        [OneToMany(inverseProperty: "ParentMovimientoLog", CascadeOperations = CascadeOperation.All)]
        public List<MovimientoLog> Adicional { get; set; }
    }
}
