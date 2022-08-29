using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.NotasDeReservas.Core.DetallesNotasDeReservas;
using Frontend.Business.Movimientos.NotasDeReservas.Core.NotasDeReservas;
using Frontend.Business.Movimientos.NotasDeReservas.Searchers;
using Frontend.Business.Movimientos.NotasDeReservas.Validators;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Synchronizer;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class NotaDeReservaService : INotaDeReservaService
    {
        private readonly NotaDeReservaFactory notaDeReservaFactory;
        private readonly DetalleNotaDeReservaFactory detalleNotaDeReservaFactory;
        private readonly NotaDeReservaSearcher notaDeReservaSearcher;
        private readonly NotaDeReservaUpdater notaDeReservaUpdater;
        private readonly DetalleNotaDeReservaUpdater detalleNotaDeReservaUpdater;
        private readonly DetalleNotaDeReservaSearcher detalleNotaDeReservaSearcher;
        private readonly DetalleNotaDeReservaDeleter detalleNotaDeReservaDeleter;
        private readonly IStockEspecialService stockEspecialService;
        private readonly DetalleNotaDeReservaValidator detalleNotaDeReservaValidator;

        public NotaDeReservaService(NotaDeReservaFactory notaDeReservaFactory, DetalleNotaDeReservaFactory detalleNotaDeReservaFactory,
            NotaDeReservaSearcher notaDeReservaSearcher, NotaDeReservaUpdater notaDeReservaUpdater, DetalleNotaDeReservaUpdater detalleNotaDeReservaUpdater,
            DetalleNotaDeReservaSearcher detalleNotaDeReservaSearcher, DetalleNotaDeReservaDeleter detalleNotaDeReservaDeleter, IStockEspecialService stockEspecialService,
            DetalleNotaDeReservaValidator detalleNotaDeReservaValidator)
        {
            this.notaDeReservaFactory = notaDeReservaFactory;
            this.detalleNotaDeReservaFactory = detalleNotaDeReservaFactory;
            this.notaDeReservaSearcher = notaDeReservaSearcher;
            this.notaDeReservaUpdater = notaDeReservaUpdater;
            this.detalleNotaDeReservaUpdater = detalleNotaDeReservaUpdater;
            this.detalleNotaDeReservaSearcher = detalleNotaDeReservaSearcher;
            this.detalleNotaDeReservaDeleter = detalleNotaDeReservaDeleter;
            this.stockEspecialService = stockEspecialService;
            this.detalleNotaDeReservaValidator = detalleNotaDeReservaValidator;
        }

        public async Task<NotaDeReserva> GetOrCreate(Reserva reserva)
        {
            return await notaDeReservaSearcher.GetOrGenerate(reserva);
        }

        public async Task<IList<DetalleNotaDeReserva>> CreateDetalle(IList<DetalleReserva> listDetalleReserva)
        {
            var stockEspecial = await stockEspecialService.GetByCodigo("S");
            return detalleNotaDeReservaFactory.Create(listDetalleReserva, stockEspecial);
        }

        public async Task Update(NotaDeReserva notaDeReserva, SyncState syncState = SyncState.Updated)
        {
            await notaDeReservaUpdater.Update(notaDeReserva, syncState);
        }

        public async Task Update(DetalleNotaDeReserva detalleNotaDeReserva, SyncState syncState = SyncState.Updated)
        {
            await detalleNotaDeReservaUpdater.Update(detalleNotaDeReserva, syncState);
        }

        public async Task<IList<DetalleNotaDeReserva>> GetAllDetalles()
        {
            return await detalleNotaDeReservaSearcher.GetAll();
        }

        public async Task DeleteDetalle(IList<DetalleNotaDeReserva> detalleNotaDeReservaList)
        {
            await detalleNotaDeReservaDeleter.Delete(detalleNotaDeReservaList);
        }

        public bool Validate(DetalleNotaDeReserva detalleNotaDeReserva)
        {
            return detalleNotaDeReservaValidator.Validate(detalleNotaDeReserva);
        }
    }
}
