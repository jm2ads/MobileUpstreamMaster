using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IData
{
    public interface IParametricData<T>
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
    }
}
