using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaGenerator
    {
        private readonly IRepository<DetalleNotaDeEntrega> repository;

        public DetalleNotaDeEntregaGenerator(IRepository<DetalleNotaDeEntrega> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleNotaDeEntrega detallenotaDeEntrega)
        {
            await repository.SaveWithChildren(detallenotaDeEntrega);
        }
    }
}
