using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.Inventarios;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Stocks;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.DetallesInventario
{
    public class DetalleInventario : SyncEntity
    {
        public double Cantidad { get; set; }
        public double CantidadContada { get; set; } 
        public bool EsContado { get; set; }
        public string Posicion { get; set; }
        public int TipoStockId { get; set; }
        public string Ubicacion { get; set; }
        public string UnidadAlmacen { get; set; }
        public string Comentario { get; set; }
        public bool HayConteoErroneo { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }

        [ForeignKey(typeof(Inventario))]
        public int InventarioId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Inventario Inventario { get; set; }

        [ForeignKey(typeof(Stock))]
        public int StockId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Stock Stock { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion Lote { get; set; }

        [ForeignKey(typeof(DetalleStockEspecial))]
        public int DetalleStockEspecialId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public DetalleStockEspecial DetalleStockEspecial { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Stock> StocksDisponibles { get; set; }

        [Ignore]
        public double CantidadStock
        {
            get
            {
                return TipoStockId == 1 ? Stock.CantidadAlmacen :
                    TipoStockId == 2 ? Stock.CantidadBloqueado : Stock.CantidadCalidad;
            }
        }
        [Ignore]
        public string DisplayCodigoAlmacen => Inventario.Almacen != null ? Inventario.Almacen.Codigo : " - ";
        [Ignore]
        public string DisplayComentario => Comentario != null ? Comentario : " - ";
        [Ignore]
        public string DisplayCantidad => CantidadContada + " / " + CantidadStock + " " + UnidadAlmacen;
        [Ignore]
        public string DisplayTipoStock => Enum.GetName(typeof(TipoStockEnum), TipoStockId);

        public DetalleInventario()
        {
        }
    }
}
