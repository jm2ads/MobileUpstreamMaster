
using Frontend.Business.Commons;
using Frontend.Business.LecturaQRs;
using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Searchers;
using Frontend.Commons.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Reservas.Searchers
{
    public class DetalleReservaSearcher
    {
        private readonly IRepository<DetalleReserva> repository;
        private readonly MaterialSearcher materialSearcher;
        private readonly ReservaSearcher reservaSearcher;

        public DetalleReservaSearcher(IRepository<DetalleReserva> repository, MaterialSearcher materialSearcher,
            ReservaSearcher reservaSearcher)
        {
            this.repository = repository;
            this.materialSearcher = materialSearcher;
            this.reservaSearcher = reservaSearcher;
        }

        public async Task<IList<DetalleReserva>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<DetalleReserva>> GetAllBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento)
        {
            var reservas = await reservaSearcher.GetAllBy(tipoReserva, estadosMovimiento);
            var reservasIds = reservas.Select(x => x.Id).ToList();

            var detalleReservaList = new List<DetalleReserva>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (reservasIds.Count / tamanioPagina); i++)
            {
                var list = reservasIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                detalleReservaList.AddRange(await repository.Where(x => list.Contains(x.ReservaId)));
            }

            return detalleReservaList;
        }

        public async Task<IList<DetalleReserva>> GetAllBy(params EstadoMovimiento[] estadosMovimiento)
        {
            var reservas = await reservaSearcher.GetAllBy(estadosMovimiento);
            var reservasIds = reservas.Select(x => x.Id).ToList();

            var detalleReservaList = new List<DetalleReserva>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (reservasIds.Count / tamanioPagina); i++)
            {
                var list = reservasIds.Skip(i * tamanioPagina).Take(tamanioPagina);

                detalleReservaList.AddRange(await repository.Where(x => list.Contains(x.ReservaId)));
            }

            return detalleReservaList;
        }

        public async Task<IList<DetalleReserva>> GetAllBy(LecturaQR lecturaQR)
        {
            var materialIdHasValue = lecturaQR.MaterialId.HasValue;
            var almacenIdHasValue = lecturaQR.AlmacenId.HasValue;
            var loteIdHasValue = lecturaQR.LoteId.HasValue;

            return await repository.Where(x => (materialIdHasValue && x.MaterialId == lecturaQR.MaterialId)
            && (!almacenIdHasValue || x.AlmacenId == lecturaQR.AlmacenId)
            && (!loteIdHasValue || x.ClaseDeValoracionId == lecturaQR.LoteId));
        }

        public async Task<IList<DetalleReserva>> GetAllBy(int materialId)
        {
            return await repository.Where(x => x.MaterialId == materialId);
        }

        public async Task<IList<DetalleReserva>> GetAllBy(int materialId, string claseValoracion)
        {
            if (String.IsNullOrEmpty(claseValoracion))
            {
                return await repository.Where(x => x.MaterialId == materialId);
            }
            else
            {
                return await repository.FindWithChildren(x => x.MaterialId == materialId && x.ClaseDeValoracion.Codigo == claseValoracion);
            }
        }

        public async Task<IList<DetalleReserva>> GetAllByIds(IList<int> ids)
        {
            return await repository.GetAllByIds(ids);
        }

        public async Task<IList<DetalleReserva>> GetAllByReserva(int reservaId)
        {
            return await repository.FindWithChildren(x => x.ReservaId == reservaId);
        }

        public async Task<IList<Material>> GetAllMaterial()
        {
            var detallesReservas = await GetAll();
            return await materialSearcher.GetAllByIds(detallesReservas.Select(x => x.MaterialId).ToList());
        }

        public async Task<IList<Material>> GetAllMaterialBy(TipoReserva tipoReserva, params EstadoMovimiento[] estadosMovimiento)
        {
            var detallesReserva = await GetAllBy(tipoReserva, estadosMovimiento);
            return await materialSearcher.GetAllByIds(detallesReserva.Select(x => x.MaterialId).ToList());
        }

        public async Task<IList<Material>> GetAllMaterialBy(params EstadoMovimiento[] estadosMovimiento)
        {
            var detallesReserva = await GetAllBy(estadosMovimiento);
            return await materialSearcher.GetAllByIds(detallesReserva.Select(x => x.MaterialId).ToList());
        }
    }
}
