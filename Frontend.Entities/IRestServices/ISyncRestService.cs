using Frontend.Business.Synchronizer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.IRestServices
{
    public interface ISyncRestService<T> where T : SyncEntity
    {
        Task<IList<T>> DoGet(object parameters);

        Task<T> DoGetEntity(object parameters);

        Task<T> DoPost(object body);
    }
}
