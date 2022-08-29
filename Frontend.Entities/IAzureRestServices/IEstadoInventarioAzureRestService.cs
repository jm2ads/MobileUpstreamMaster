using Frontend.Business.EstadosInventarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IEstadoInventarioAzureRestService
    {
        Task<IList<EstadoInventario>> GetAll();
    }
}
