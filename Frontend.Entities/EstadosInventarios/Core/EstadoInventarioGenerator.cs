using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.EstadosInventarios.Core
{
    public class EstadoInventarioGenerator
    {
        private readonly IRepository<EstadoInventario> repository;

        public EstadoInventarioGenerator(IRepository<EstadoInventario> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(EstadoInventario estadoInventario)
        {
            await repository.SaveWithChildren(estadoInventario);
        }
    }
}
