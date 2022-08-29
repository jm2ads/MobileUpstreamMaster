using Frontend.Business.Centros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface ICentroAzureRestService
    {
        Task<IList<Centro>> GetAll();
        Task<IList<Centro>> GetByIdRed(string idRed);
    }
}
