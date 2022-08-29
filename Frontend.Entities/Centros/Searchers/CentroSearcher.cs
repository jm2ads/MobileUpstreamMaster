using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Centros.Searchers
{
    public class CentroSearcher
    {
        private readonly IRepository<Centro> repository;

        public CentroSearcher(IRepository<Centro> repository)
        {
            this.repository = repository;
        }

        //public async Task<Centro> GetByRemoteId(int remoteId)
        //{
        //    return await repository.First(x => x.RemoteId == remoteId);
        //}

        public async Task<Centro> GetById(int Id)
        {
            return await repository.First(x => x.Id == Id);
        }

        public async Task<IList<Centro>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }
    }
}
