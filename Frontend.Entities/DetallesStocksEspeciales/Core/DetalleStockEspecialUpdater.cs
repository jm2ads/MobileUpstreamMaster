using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesStocksEspeciales.Core
{
    public class DetalleStockEspecialUpdater
    {
        private readonly IRepository<DetalleStockEspecial> repository;

        public DetalleStockEspecialUpdater(IRepository<DetalleStockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task Update(DetalleStockEspecial detalleStockEspecial)
        {
            await repository.UpdateWithChildren(detalleStockEspecial);
        }
    }
}
