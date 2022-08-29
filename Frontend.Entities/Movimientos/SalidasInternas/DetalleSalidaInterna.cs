using Frontend.Business.Almacenes;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.SalidasInternas
{
    public class DetalleSalidaInterna : SyncEntity
    {
        public string Posicion { get; set; }
        public string UnidadDeMedida { get; set; }
        public string DestinatarioMercancia { get; set; }
        public string TextoPosicion { get; set; }
        public double CantidadPendiente { get; set; }
        public double CantidadEnviada { get; set; }
        public bool EsContado { get; set; }

        [ForeignKey(typeof(Material))]
        public int MaterialId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int ClaseDeValoracionId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? AlmacenId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(SalidaInterna))]
        public int SalidaInternaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public SalidaInterna SalidaInterna { get; set; }

        [Ignore]
        public string DisplayCantidad => CantidadEnviada + " / " + CantidadPendiente + " " + UnidadDeMedida;
    }
}
