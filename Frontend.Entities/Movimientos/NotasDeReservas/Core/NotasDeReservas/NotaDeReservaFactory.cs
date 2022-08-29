using Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.StocksEspeciales;
using System;
using System.Linq;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.NotasDeReservas
{
    public class NotaDeReservaFactory
    {
        private readonly DetalleNotaDeReservaFactory detalleNotaDeReservaFactory;

        public NotaDeReservaFactory(DetalleNotaDeReservaFactory detalleNotaDeReservaFactory)
        {
            this.detalleNotaDeReservaFactory = detalleNotaDeReservaFactory;
        }

        public NotaDeReserva Create(Reserva reserva, StockEspecial stockEspecial)
        {
            return new NotaDeReserva()
            {
                DetallesNotasDeReservas = detalleNotaDeReservaFactory.Create(reserva.DetallesReserva, stockEspecial).ToList(),
                ReservaId = reserva.Id,
                Reserva = reserva,
                FechaContabilizacion = DateTime.Now,
                FechaDocumentacion = DateTime.Now,
                SyncState = Synchronizer.SyncState.New
            };
        }
    }
}
