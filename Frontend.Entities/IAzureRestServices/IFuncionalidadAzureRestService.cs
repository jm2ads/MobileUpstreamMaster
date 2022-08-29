using Frontend.Business.Funcionalidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IFuncionalidadAzureRestService
    {
        Task<IList<Funcionalidad>> GetAll();
        Task<IList<Funcionalidad>> GetByIdRed(string idRed, int centroId);
    }
}
