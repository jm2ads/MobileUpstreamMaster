using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.ClasesDeValoracion.Deleters;
using Frontend.Business.ClasesDeValoracion.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class ClaseDeValoracionService : IClaseDeValoracionService
    {
        private readonly ClaseDeValoracionSearcher claseDeValoracionSearcher;
        private readonly ClaseDeValoracionDeleter claseDeValoracionDeleter;

        public ClaseDeValoracionService(ClaseDeValoracionSearcher claseDeValoracionSearcher, ClaseDeValoracionDeleter claseDeValoracionDeleter )
        {
            this.claseDeValoracionSearcher = claseDeValoracionSearcher;
            this.claseDeValoracionDeleter = claseDeValoracionDeleter;
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            return await claseDeValoracionSearcher.GetAllCodigoAutocomplete();
        }

        public async Task<IList<ClaseDeValoracion>> GetAll()
        {
            return await claseDeValoracionSearcher.GetAll();
        }

        public async Task<ClaseDeValoracion> GetByCodigo(string codigo)
        {
            return await claseDeValoracionSearcher.GetByCodigo(codigo);
        }

        public async Task<ClaseDeValoracion> GetBy(int id)
        {
            return await claseDeValoracionSearcher.GetById(id);
        }

        public async Task DeleteAll()
        {
            await claseDeValoracionDeleter.DeleteAll();
        }
    }
}
