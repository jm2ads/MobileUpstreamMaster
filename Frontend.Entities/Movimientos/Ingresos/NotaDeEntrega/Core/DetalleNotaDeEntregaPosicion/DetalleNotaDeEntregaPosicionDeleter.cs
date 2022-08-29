using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaPosicionDeleter
    {
        private readonly IRepository<DetalleNotaDeEntregaPosicion> repository;

        public DetalleNotaDeEntregaPosicionDeleter(IRepository<DetalleNotaDeEntregaPosicion> repository)
        {
            this.repository = repository;
        }

        public async Task Delete(DetalleNotaDeEntregaPosicion detalleNotaDeEntregaPosicion)
        {
            await repository.Delete(detalleNotaDeEntregaPosicion);
        }

        public async Task Delete(IList<DetalleNotaDeEntregaPosicion> detalleNotaDeEntregaPosicionList)
        {
            foreach (var detalleNotaDeEntregaPosicion in detalleNotaDeEntregaPosicionList)
            {
                await Delete(detalleNotaDeEntregaPosicion);
            }
        }
    }
}
