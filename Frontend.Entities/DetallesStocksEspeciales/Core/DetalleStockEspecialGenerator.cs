using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.DetallesStocksEspeciales.Core
{
    public class DetalleStockEspecialGenerator
    {
        private readonly IRepository<DetalleStockEspecial> repository;

        public DetalleStockEspecialGenerator(IRepository<DetalleStockEspecial> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleStockEspecial detalleStockEspecial)
        {
            await repository.SaveWithChildren(detalleStockEspecial);
        }
    }
}
