using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class DetalleInventarioMasivoGenerator
    {
        private readonly IRepository<DetalleInventarioMasivo> repository;

        public DetalleInventarioMasivoGenerator(IRepository<DetalleInventarioMasivo> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            await repository.SaveWithChildren(detalleInventarioMasivo);
        }
    }
}
