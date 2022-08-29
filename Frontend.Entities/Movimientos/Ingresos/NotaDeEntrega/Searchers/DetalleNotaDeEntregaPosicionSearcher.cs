using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Searchers
{
    public class DetalleNotaDeEntregaPosicionSearcher
    {
        private readonly IRepository<DetalleNotaDeEntregaPosicion> repository;

        public DetalleNotaDeEntregaPosicionSearcher(IRepository<DetalleNotaDeEntregaPosicion> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<DetalleNotaDeEntregaPosicion>> GetAll()
        {
            return await this.repository.GetAllWithChildren();
        }
    }

}
