using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Movimientos.Reservas
{
    public class DetalleReserva : SyncEntity
    {
        public string Posicion { get; set; }
        public double CantidadReserva { get; set; }
        public string Unidad { get; set; }
        public string TextoPosicion { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string Destinatario { get; set; }
        public string ClaseDeMovimientoCodigo { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        [ForeignKey(typeof(Reserva))]
        public int ReservaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Reserva Reserva { get; set; }

        [ForeignKey(typeof(ClaseDeValoracion))]
        public int? ClaseDeValoracionId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public ClaseDeValoracion ClaseDeValoracion { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? AlmacenId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(Material))]
        public int MaterialId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Material Material { get; set; }
    }
}
