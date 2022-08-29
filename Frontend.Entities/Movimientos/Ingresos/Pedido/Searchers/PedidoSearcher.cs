using Frontend.Business.Commons;
using Frontend.Commons.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Searchers
{
    public class PedidoSearcher
    {
        private readonly IRepository<Pedido> repository;

        public PedidoSearcher(IRepository<Pedido> repository)
        {
            this.repository = repository;
        }
        public async Task<IList<Pedido>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<IList<string>> GetAllNumeroDePedidosAutocomplete()
        {
            var pedidos = await repository.GetAll();
            return pedidos.Select(x => x.NumeroPedido).Distinct().ToList();

        }
        public async Task<IList<Pedido>> GetAllByIds(IList<int> idList)
        {
            return await repository.GetAllByIds(idList);
        }

        public async Task<Pedido> GetById(int id)
        {
            return await repository.GetWithChildren(id);
        }

        public async Task<IList<Pedido>> GetAllWithChildrenByIds(IList<int> idList)
        {
            var pedidoList = new List<Pedido>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (idList.Count / tamanioPagina); i++)
            {
                var list = idList.Skip(i * tamanioPagina).Take(tamanioPagina);
                pedidoList.AddRange((await repository.GetAllWithChildren()).Where(x => list.Contains(x.Id)));
            }
            return pedidoList;
        }

        public async Task<IList<Pedido>> GetAllBy(EstadoMovimiento estadoIngreso)
        {
            return await repository.Where(x => x.Estado == estadoIngreso);
        }
    }
}
