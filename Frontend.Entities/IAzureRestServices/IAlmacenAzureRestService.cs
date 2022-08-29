using Frontend.Business.Almacenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IAlmacenAzureRestService
    {
        Task<IList<Almacen>> GetAll();
    }
}
