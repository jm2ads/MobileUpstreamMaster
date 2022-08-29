using Frontend.Business.GruposDeArticulos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IGrupoDeArticuloService
    {
        Task<IList<GrupoDeArticulo>> GetAll();
        Task<GrupoDeArticulo> GetByCodigo(string codigo);
        Task<IList<string>> GetAllCodigoAutocomplete();
    }
}
