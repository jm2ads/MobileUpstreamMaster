using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace Frontend.Business.Movimientos.Ingresos
{
    public class DetalleNotaDeEntrega : SyncLocalEntity
    {
        public bool EntregaFinal{ get; set; }
        public string TextoPosicion { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string DestinatarioMercancia { get; set; }
        public string TipoStockId { get; set; }
        public int CentroId { get; set; }

        [ForeignKey(typeof(DetallePedido))]
        public int DetallePedidoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetallePedido DetallePedido { get; set; }

        [ForeignKey(typeof(NotaDeEntrega))]
        public int NotaDeEntregaId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public NotaDeEntrega NotaDeEntrega { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int? ClaseDeValoracionId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? AlmacenId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int StockEspecialId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleNotaDeEntregaPosicion> DetalleNotaDeEntregaPosicion { get; set; }
    }
}
