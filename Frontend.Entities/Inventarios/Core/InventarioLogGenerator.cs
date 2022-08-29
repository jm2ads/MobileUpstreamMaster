using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Inventarios.Core
{
    public class InventarioLogGenerator
    {
        private readonly IRepository<InventarioLog> repository;

        public InventarioLogGenerator(IRepository<InventarioLog> repository)
        {
            this.repository = repository;
        }

        public async Task<InventarioLog> Generate(InventarioLog inventarioLog)
        {
            return await repository.SaveWithChildren(inventarioLog);
        }

        public async Task<IList<InventarioLog>> Generate(IList<InventarioLog> listInventarioLog)
        {
            try
            {
                foreach (var inventarioLog in listInventarioLog)
                {
                    await Generate(inventarioLog);
                }
                return listInventarioLog;

            }
            catch (System.Exception e )
            {

                throw;
            }
        }
    }
}
