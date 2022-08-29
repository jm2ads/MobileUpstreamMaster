using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IData
{
    public interface ISyncRestService<T>
    {
        Task<IList<T>> DoGet(object parameters);

        Task<T> DoGetEntity(object parameters);

        Task<IList<T>> DoPost(object body);
    }
}
