using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.CambiosUbicacion.Core
{
    public class CambioUbicacionUpdater
    {
        private readonly IRepository<CambioUbicacion> repository;

        public CambioUbicacionUpdater(IRepository<CambioUbicacion> repository)
        {
            this.repository = repository;
        }

        public async Task Update(CambioUbicacion cambioUbicacion)
        {
            await this.repository.UpdateWithChildren(cambioUbicacion);
        }
    }
}
