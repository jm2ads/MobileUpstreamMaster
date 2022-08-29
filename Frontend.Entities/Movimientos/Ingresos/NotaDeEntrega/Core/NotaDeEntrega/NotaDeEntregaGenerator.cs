using Frontend.Business.Commons;
using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.StocksEspeciales;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class NotaDeEntregaGenerator
    {
        private readonly IRepository<NotaDeEntrega> repository;
        private readonly NotaDeEntregaFactory notaDeEntregaFactory;

        public NotaDeEntregaGenerator(IRepository<NotaDeEntrega> repository, NotaDeEntregaFactory notaDeEntregaFactory)
        {
            this.repository = repository;
            this.notaDeEntregaFactory = notaDeEntregaFactory;
        }

        public async Task<NotaDeEntrega> Generate(Pedido pedido)
        {
            var notaDeEntrega = notaDeEntregaFactory.Create(pedido);
            return await repository.SaveWithChildren(notaDeEntrega);
        }
    }
}
