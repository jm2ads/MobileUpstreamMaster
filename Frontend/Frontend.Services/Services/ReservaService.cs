using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.Business.Movimientos.NotasDeReservas.Searchers;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Business.Movimientos.Reservas.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ReservaSearcher reservaSearcher;
        private readonly DetalleReservaSearcher detalleReservaSearcher;
        private readonly MaterialSearcher materialSearcher;
        private readonly TipoStockSearcher tipoStockSearcher;

        public ReservaService(ReservaSearcher reservaSearcher, DetalleReservaSearcher detalleReservaSearcher, MaterialSearcher materialSearcher,
            TipoStockSearcher tipoStockSearcher)
        {
            this.reservaSearcher = reservaSearcher;
            this.detalleReservaSearcher = detalleReservaSearcher;
            this.materialSearcher = materialSearcher;
            this.tipoStockSearcher = tipoStockSearcher;
        }

        public async Task<IList<Reserva>> GetAll()
        {
            return await reservaSearcher.GetAll();
        }

        public async Task<IList<Reserva>> GetAllBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento)
        {
            return await reservaSearcher.GetAllBy(tipoReserva, estadosMovimiento);
        }

        public async Task<IList<Reserva>> GetAllBy(params EstadoMovimiento[] estadosMovimiento)
        {
            return await reservaSearcher.GetAllBy(estadosMovimiento);
        }

        public async Task<Reserva> GetWithChildren(int id)
        {
            return await reservaSearcher.GetWithChildren(id);
        }

        public async Task<IList<Material>> GetAllMaterial()
        {
            return await detalleReservaSearcher.GetAllMaterial();
        }

        public async Task<IList<Material>> GetAllMaterialBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento)
        {
            return await detalleReservaSearcher.GetAllMaterialBy(tipoReserva, estadosMovimiento);
        }

        public async Task<IList<Material>> GetAllMaterialBy(params EstadoMovimiento[] estadosMovimiento)
        {
            return await detalleReservaSearcher.GetAllMaterialBy(estadosMovimiento);
        }

        public async Task<IList<Reserva>> GetAllBy(int materialId, TipoReserva tipoReserva)
        {
            var detalles = await detalleReservaSearcher.GetAllBy(materialId);
            return await reservaSearcher.GetAllByIds(detalles.Select(x => x.ReservaId).Distinct().ToList(), tipoReserva);
        }

        public async Task<IList<Reserva>> GetAllBy(LecturaQR lecturaQR, TipoReserva tipoReserva)
        {
            var detalles = await detalleReservaSearcher.GetAllBy(lecturaQR);
            var reservas = await reservaSearcher.GetAllByIds(detalles.Select(x => x.ReservaId).Distinct().ToList(), tipoReserva);
            return reservas.Where(reserva => string.IsNullOrWhiteSpace(lecturaQR.NumeroMovimiento) || reserva.Numero == lecturaQR.NumeroMovimiento).ToList();
        }

        public async Task<IList<Reserva>> GetAllBy(LecturaQR lecturaQR)
        {
            var detalles = await detalleReservaSearcher.GetAllBy(lecturaQR);
            var reservas = await reservaSearcher.GetAllByIds(detalles.Select(x => x.ReservaId).Distinct().ToList());
            return reservas.Where(reserva => string.IsNullOrWhiteSpace(lecturaQR.NumeroMovimiento) || reserva.Numero == lecturaQR.NumeroMovimiento).ToList();
        }

        public async Task<IList<DetalleReserva>> GetAllDetallesByReserva(int reservaId)
        {
            return await detalleReservaSearcher.GetAllByReserva(reservaId);
        }

        public async Task<IList<DetalleReserva>> GetAllDetalles()
        {
            return await detalleReservaSearcher.GetAll();
        }

        public IList<TipoStock> GetAllTipoStock()
        {
            return tipoStockSearcher.GetAll();
        }

        public TipoStock GetTipoStockBy(string codigo)
        {
            return tipoStockSearcher.GetByCodigo(codigo);
        }

        public async Task<IList<Reserva>> GetAllBy(int materialId, string claseValoracion, TipoReserva tipoReserva)
        {
            var detalles = await detalleReservaSearcher.GetAllBy(materialId, claseValoracion);
            return await reservaSearcher.GetAllByIds(detalles.Select(x => x.ReservaId).Distinct().ToList(), tipoReserva);
        }
    }
}
