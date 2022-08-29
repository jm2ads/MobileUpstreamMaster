using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IAzureRestServices
{
    public interface IMaterialAzureRestService
    {
        Task<IList<Business.Materiales.Material>> GetAll();
    }
}
