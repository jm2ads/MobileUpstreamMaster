using Frontend.Business.Movimientos.Traslados;
using Frontend.Business.Movimientos.Traslados.Core;
using Frontend.Business.Movimientos.Traslados.Searchers;
using Frontend.Business.Movimientos.Traslados.Validators;
using Frontend.Business.Synchronizer;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class TrasladoService : ITrasladoService
    {
        private readonly TrasladoUpdater trasladoUpdater;
        private readonly DetalleTrasladoDeleter detalleTrasladoDeleter;
        private readonly TasladoDeleter tasladoDeleter;
        private readonly DetalleTrasladoUpdater detalleTrasladoUpdater;
        private readonly TrasladoValidator trasladoValidator;
        private readonly TrasladoSearcher trasladoSearcher;

        public TrasladoService(TrasladoUpdater trasladoUpdater, DetalleTrasladoDeleter detalleTrasladoDeleter,
            TasladoDeleter tasladoDeleter, DetalleTrasladoUpdater detalleTrasladoUpdater, TrasladoValidator trasladoValidator,
            TrasladoSearcher trasladoSearcher)
        {
            this.trasladoUpdater = trasladoUpdater;
            this.detalleTrasladoDeleter = detalleTrasladoDeleter;
            this.tasladoDeleter = tasladoDeleter;
            this.detalleTrasladoUpdater = detalleTrasladoUpdater;
            this.trasladoValidator = trasladoValidator;
            this.trasladoSearcher = trasladoSearcher;
        }

        public async Task<Traslado> GetWithChildren(int id)
        {
            return await trasladoSearcher.GetById(id);
        }

        public async Task Delete(DetalleTraslado detalleTraslado)
        {
            await detalleTrasladoDeleter.Delete(detalleTraslado);
        }

        public async Task Delete(Traslado traslado)
        {
            await tasladoDeleter.Delete(traslado);
        }

        public async Task DeleteDetalle(IList<DetalleTraslado> list)
        {
            await detalleTrasladoDeleter.Delete(list);
        }

        public async Task Update(Traslado traslado, SyncState syncState)
        {
            await trasladoUpdater.Update(traslado, syncState);
        }

        public async Task Update(Traslado traslado)
        {
            await trasladoUpdater.Update(traslado);
        }

        public async Task Update(DetalleTraslado detalleTraslado)
        {
            await detalleTrasladoUpdater.Update(detalleTraslado);
        }

        public bool Validate(Traslado traslado)
        {
            return trasladoValidator.Validate(traslado);
        }
    }
}
