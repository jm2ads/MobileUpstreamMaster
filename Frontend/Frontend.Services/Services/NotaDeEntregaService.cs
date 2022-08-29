using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.Ingresos.Core;
using Frontend.Business.Movimientos.Ingresos.Searchers;
using Frontend.Business.Movimientos.Ingresos.Validations;
using Frontend.Business.Synchronizer;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class NotaDeEntregaService : INotaDeEntregaService
    {
        private readonly NotaDeEntregaSearcher notaDeEntregaSearcher;
        private readonly NotaDeEntregaUpdater notaDeEntregaUpdater;
        private readonly DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator;
        private readonly DetalleNotaDeEntregaPosicionUpdater detalleNotaDeEntregaPosicionUpdater;
        private readonly DetalleNotaDeEntregaPosicionDeleter detalleNotaDeEntregaPosicionDeleter;

        public NotaDeEntregaService (NotaDeEntregaSearcher notaDeEntregaSearcher, NotaDeEntregaUpdater notaDeEntregaUpdater, 
            DetalleNotaDeEntregaValidator detalleNotaDeEntregaValidator, DetalleNotaDeEntregaPosicionUpdater detalleNotaDeEntregaPosicionUpdater, DetalleNotaDeEntregaPosicionDeleter detalleNotaDeEntregaPosicionDeleter)
        {
            this.notaDeEntregaSearcher = notaDeEntregaSearcher;
            this.notaDeEntregaUpdater = notaDeEntregaUpdater;
            this.detalleNotaDeEntregaValidator = detalleNotaDeEntregaValidator;
            this.detalleNotaDeEntregaPosicionUpdater = detalleNotaDeEntregaPosicionUpdater;
            this.detalleNotaDeEntregaPosicionDeleter = detalleNotaDeEntregaPosicionDeleter;
        }

        public async Task<NotaDeEntrega> GetOrCreate(Pedido pedido)
        {
            return await notaDeEntregaSearcher.GetOrGenerate(pedido);
        }

        public async Task Update(NotaDeEntrega notaDeEntrega, SyncState syncState = SyncState.Updated)
        {
            await notaDeEntregaUpdater.Update(notaDeEntrega, syncState);
        }

        public async Task Update(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion, string claseMovimientoCodigo = null, SyncState syncState = SyncState.Updated)
        {
            await detalleNotaDeEntregaPosicionUpdater.Update(detalleNotaDeEntregaPosicion, claseMovimientoCodigo, syncState);
        }

        public bool Validate(DetalleNotaDeEntrega detalleNotaDeEntrega)
        {
            return detalleNotaDeEntregaValidator.Validate(detalleNotaDeEntrega);
        }

        public async Task DeleteDetalle(IList<DetalleNotaDeEntregaPosicion> detalleNotaDeEntregaPosicionList)
        {
            await detalleNotaDeEntregaPosicionDeleter.Delete(detalleNotaDeEntregaPosicionList);
        }

        public bool ValidatePosicion(DetalleNotaDeEntrega detalleNotaDeEntrega)
        {
            return detalleNotaDeEntregaValidator.Validate(detalleNotaDeEntrega);
        }
    }
}
