using Frontend.Business.Movimientos.NotasDeReservas.Searchers;
using Frontend.Business.Movimientos.NotasDeReservas.Validators;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.StocksEspeciales;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas
{
    public class DetalleNotaDeReservaFactory
    {
        private readonly DetalleNotaDeReservaValidator detalleNotaDeReservaValidator;

        public DetalleNotaDeReservaFactory(DetalleNotaDeReservaValidator detalleNotaDeReservaValidator)
        {
            this.detalleNotaDeReservaValidator = detalleNotaDeReservaValidator;
        }

        public DetalleNotaDeReserva Create(DetalleReserva detalleReserva, StockEspecial stockEspecial)
        {
            var detalleNotaDeReserva = new DetalleNotaDeReserva()
            {
                DetalleReservaId = detalleReserva.Id,
                DetalleReserva = detalleReserva,
                CantidadIngresada = detalleReserva.CantidadReserva,
                TextoPosicion = detalleReserva.TextoPosicion,
                Destinatario = detalleReserva.Destinatario,
                PuestoDeDescarga = detalleReserva.PuestoDeDescarga,
                TipoStockCodigo = TipoStockSearcher.LibreUtilizacion,
                StockEspecialId = stockEspecial.Id,
                StockEspecial = stockEspecial,
                AlmacenId = detalleReserva.AlmacenId.GetValueOrDefault(),
                Almacen = detalleReserva.Almacen,
                ClaseDeValoracionId = detalleReserva.ClaseDeValoracionId.GetValueOrDefault(),
                ClaseDeValoracion = detalleReserva.ClaseDeValoracion,
                SyncState = Synchronizer.SyncState.New
            };
            detalleNotaDeReserva.EsContado = detalleNotaDeReservaValidator.Validate(detalleNotaDeReserva);

            return detalleNotaDeReserva;
        }

        public IList<DetalleNotaDeReserva> Create(IList<DetalleReserva> detalleReservaList, StockEspecial stockEspecial)
        {
            var detalleNotaDeReservaList = new List<DetalleNotaDeReserva>();

            foreach (var detalleReserva in detalleReservaList)
                detalleNotaDeReservaList.Add(Create(detalleReserva, stockEspecial));

            return detalleNotaDeReservaList;
        }
    }
}
