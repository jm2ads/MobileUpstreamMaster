using Frontend.Business.Centros;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.SalidasInternas
{
    public class SalidaInterna : SyncEntity
    {
        public string NumeroPedido { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaDocumento{ get; set; }
        public EstadoMovimiento Estado { get; set; }
        public string Usuario { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroReceptorId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro CentroReceptor { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroEmisorId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro CentroEmisor { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleSalidaInterna> DetallesSalidaInterna{ get; set; }
    }
}
