using Frontend.Business.GruposDeArticulos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IGrupoDeArticuloAzureRestService
    {
        Task<IList<GrupoDeArticulo>> GetAll();
    }
}
