using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.NotasDeReservas
{
    public class DetalleNotaDeReserva : SyncLocalEntity
    {
        public double CantidadIngresada { get; set; }
        public string TextoPosicion { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string Destinatario { get; set; }
        public bool EsEntregaFinal { get; set; }
        public bool EsContado { get; set; }
        public string TipoStockCodigo { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int AlmacenId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(DetalleReserva))]
        public int DetalleReservaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetalleReserva DetalleReserva { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int StockEspecialId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }        

        [ForeignKey(typeof(NotaDeReserva))]
        public int NotaDeReservaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public NotaDeReserva NotaDeReserva { get; set; }

        [Ignore]
        public string DisplayCantidad => CantidadIngresada + " / " + DetalleReserva.CantidadReserva + " " + DetalleReserva.Unidad;
    }
}
