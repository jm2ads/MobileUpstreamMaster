using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Centros.Core
{
    public class CentroGenerator
    {
        private readonly IRepository<Centro> repository;

        public CentroGenerator(IRepository<Centro> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Centro centro)
        {
            await repository.SaveWithChildren(centro);
        }

        public async Task Generate(IList<Centro> centroList)
        {
            await repository.InsertAll(centroList);
        }
    }
}
