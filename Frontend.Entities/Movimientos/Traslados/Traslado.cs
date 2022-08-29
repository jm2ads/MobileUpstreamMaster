using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Traslados
{
    public class Traslado : SyncLocalEntity
    {
        public string ClaseDeMovimientoCodigo { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string NumeroProvisorio { get; set; }
        public string Usuario { get; set; }
        public EstadoMovimiento Estado { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleTraslado> DetallesTraslado { get; set; }
    }
}
