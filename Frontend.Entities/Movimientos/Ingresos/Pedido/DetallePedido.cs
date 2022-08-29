using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class DetallePedido : SyncEntity
    {
        public string Posicion { get; set; }
        public string Unidad { get; set; }
        public double Tolerancia { get; set; }
        public string TipoStockId { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int StockEspecialId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [ForeignKey(typeof(Material))]
        public int MaterialId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? AlmacenId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(Pedido))]
        public int PedidoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Pedido Pedido { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int? ClaseDeValoracionId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetallePedidoPosicion> DetallesPedidoPosicion { get; set; }
    }
}
