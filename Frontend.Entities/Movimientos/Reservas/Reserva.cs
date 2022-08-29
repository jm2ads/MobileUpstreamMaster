using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Reservas
{
    public class Reserva : SyncEntity
    {
        public string Numero { get; set; }
        public string Imputacion { get; set; }
        public string ClaseDeMovimiento { get; set; }
        public TipoReserva TipoReserva { get; set; }
        public EstadoMovimiento Estado { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleReserva> DetallesReserva { get; set; }        
    }
}
