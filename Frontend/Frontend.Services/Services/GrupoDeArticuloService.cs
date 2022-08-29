using Frontend.Business.GruposDeArticulos;
using Frontend.Business.GruposDeArticulos.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class GrupoDeArticuloService: IGrupoDeArticuloService
    {
        private readonly GrupoDeArticuloSearcher grupoDeArticuloSearcher;

        public GrupoDeArticuloService(GrupoDeArticuloSearcher grupoDeArticuloSearcher)
        {
            this.grupoDeArticuloSearcher = grupoDeArticuloSearcher;
        }

        public async Task<IList<GrupoDeArticulo>> GetAll()
        {
            return await grupoDeArticuloSearcher.GetAll();
        }

        public async Task<GrupoDeArticulo> GetByCodigo(string codigo)
        {
            return await grupoDeArticuloSearcher.GetByCodigo(codigo);
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            return await grupoDeArticuloSearcher.GetAllCodigoAutocomplete();
        }
    }
}
