using Frontend.Business.Commons;
using Frontend.Business.Movimientos.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Ingresos.Core
{
    public class DetalleNotaDeEntregaDeleter
    {
        private readonly IRepository<DetalleNotaDeEntrega> repository;

        public DetalleNotaDeEntregaDeleter(IRepository<DetalleNotaDeEntrega> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
    }
}
