using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Ingresos.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Searchers
{
    public class NotaDeEntregaSearcher
    {
        private readonly IRepository<NotaDeEntrega> repository;
        private readonly NotaDeEntregaGenerator notaDeEntregaGenerator;

        public NotaDeEntregaSearcher(IRepository<NotaDeEntrega> repository, NotaDeEntregaGenerator notaDeEntregaGenerator)
        {
            this.repository = repository;
            this.notaDeEntregaGenerator = notaDeEntregaGenerator;
        }

        public async Task<NotaDeEntrega> GetOrGenerate(Pedido pedido)
        {
            try
            {
                var notaDeEntrega = await repository.FindFirstWithChildren(x => x.PedidoId == pedido.Id);
                return notaDeEntrega ?? await notaDeEntregaGenerator.Generate(pedido);
            }
            catch(Exception e)
            {
                return await notaDeEntregaGenerator.Generate(pedido); ;
            }
        }
    }
}
