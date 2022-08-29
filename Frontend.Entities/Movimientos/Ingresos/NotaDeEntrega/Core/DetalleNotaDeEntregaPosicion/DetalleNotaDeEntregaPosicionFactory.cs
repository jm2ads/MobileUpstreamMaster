using Frontend.Business.Movimientos.Ingresos.Validations;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaPosicionFactory
    {
        private readonly DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator;

        public DetalleNotaDeEntregaPosicionFactory(DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator)
        {
            this.detalleNotaDeEntregaValidator = detalleNotaDeEntregaValidator;
        }
        public DetalleNotaDeEntregaPosicion Create(DetallePedidoPosicion detallePedidoPosicion)
        {
            var detalleNotaDeEntregaPosicion = new DetalleNotaDeEntregaPosicion()
            {
                DocumentoReferencia = detallePedidoPosicion.DocumentoReferencia,
                PosicionDocumento = detallePedidoPosicion.PosicionDocumento,
                CantidadRecibida = detallePedidoPosicion.CantidadPendiente,
                ClaseDeMovimientoCodigo = detallePedidoPosicion.ClaseMovimientoCodigo,
                Ejercicio = detallePedidoPosicion.Ejercicio,
                DetallePedidoPosicion = detallePedidoPosicion,
                DetallePedidoPosicionId = detallePedidoPosicion.Id,
                SyncState = Synchronizer.SyncState.New
            };
            return detalleNotaDeEntregaPosicion;
        }

        public IList<DetalleNotaDeEntregaPosicion> Create(IList<DetallePedidoPosicion> detallesPedidoPosicion)
        {
            var detallesPedidoPosicionList = new List<DetalleNotaDeEntregaPosicion>();

            foreach (var detallePedidoPosicion in detallesPedidoPosicion)
                detallesPedidoPosicionList.Add(Create(detallePedidoPosicion));

            return detallesPedidoPosicionList;
        }
    }
}
