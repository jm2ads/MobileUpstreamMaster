using Frontend.Business.Movimientos.Ingresos.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaFactory
    {
        private readonly DetalleNotaDeEntregaPosicionFactory detalleNotaDeEntregaPosicionFactory;
        private readonly DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator;

        public DetalleNotaDeEntregaFactory(DetalleNotaDeEntregaPosicionFactory detalleNotaDeEntregaPosicionFactory, DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator)
        {
            this.detalleNotaDeEntregaPosicionFactory = detalleNotaDeEntregaPosicionFactory;
            this.detalleNotaDeEntregaValidator = detalleNotaDeEntregaValidator;
        }

        public DetalleNotaDeEntrega Create(DetallePedido detallePedido)
        {
            var detalleNotaDeEntrega =  new DetalleNotaDeEntrega()
            {
                CentroId = detallePedido.CentroId,
                DetallePedidoId = detallePedido.Id,
                DetallePedido = detallePedido,
                Almacen = detallePedido.Almacen,
                AlmacenId = detallePedido.AlmacenId,
                ClaseDeValoracion = detallePedido.ClaseDeValoracion,
                ClaseDeValoracionId = detallePedido.ClaseDeValoracionId,
                TipoStockId = detallePedido.TipoStockId,
                StockEspecial = detallePedido.StockEspecial,
                StockEspecialId = detallePedido.StockEspecialId,
                SyncState = Synchronizer.SyncState.New,
                DetalleNotaDeEntregaPosicion = detalleNotaDeEntregaPosicionFactory.Create(detallePedido.DetallesPedidoPosicion).ToList()
            };

            detalleNotaDeEntrega.DetalleNotaDeEntregaPosicion.ForEach(x => x.EsContado = false);

            return detalleNotaDeEntrega;
        }

        public IList<DetalleNotaDeEntrega> Create(IList<DetallePedido> detallesPedido)
        {
            var detalleNotaDeEntregaList = new List<DetalleNotaDeEntrega>();

            foreach (var detallePedido in detallesPedido)
                detalleNotaDeEntregaList.Add(Create(detallePedido));

            return detalleNotaDeEntregaList;
        }
    }
}
