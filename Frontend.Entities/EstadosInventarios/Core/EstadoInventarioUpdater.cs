using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.EstadosInventarios.Core
{
    public class EstadoInventarioUpdater
    {
        private readonly IRepository<EstadoInventario> repository;

        public EstadoInventarioUpdater(IRepository<EstadoInventario> repository) 
        {
            this.repository = repository;
        }

        public async Task Update(EstadoInventario estadoInventario)
        {
            await repository.UpdateWithChildren(estadoInventario);
        }
    }
}
