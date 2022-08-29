using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.NotasDeReservas
{
    public class NotaDeReserva : SyncLocalEntity
    {
        public string TextoCabecera { get; set; }
        public string UsuarioReserva { get; set; }
        public DateTime FechaDocumentacion { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string TextoPosicionGenerico { get; set; }

        [ForeignKey(typeof(Reserva))]
        public int ReservaId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Reserva Reserva { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleNotaDeReserva> DetallesNotasDeReservas { get; set; }
    }
}
