using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IClaseDeValoracionAzureRestService
    {
        Task<IList<Business.ClasesDeValoracion.ClaseDeValoracion>> GetAll();
    }
}
