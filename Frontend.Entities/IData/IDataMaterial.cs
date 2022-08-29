using Frontend.Business.Materiales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IData
{
    public interface IDataMaterial : ITransactionalData<Material>
    {
        Task<IList<Material>> GetAll();
    }
}
