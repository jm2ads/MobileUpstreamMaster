using Frontend.Business.Commons;
using Frontend.Business.Stocks;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class DetalleTrasladoGenerator
    {
        private readonly IRepository<DetalleTraslado> repository;
        private readonly DetalleTrasladoFactory detalleTrasladoFactory;

        public DetalleTrasladoGenerator(IRepository<DetalleTraslado> repository, DetalleTrasladoFactory detalleTrasladoFactory)
        {
            this.detalleTrasladoFactory = detalleTrasladoFactory;
            this.repository = repository;
        }

        public async Task<DetalleTraslado> Generate(Traslado traslado, Stock stock)
        {
            var detalle = await detalleTrasladoFactory.Create(traslado, stock);
            return await repository.SaveWithChildren(detalle);
        }
    }
}
