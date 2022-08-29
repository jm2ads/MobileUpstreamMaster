using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.SalidasInternas.Core
{
    public class SalidaInternaGenerator
    {
        private readonly IRepository<SalidaInterna> repository;

        public SalidaInternaGenerator(IRepository<SalidaInterna> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(SalidaInterna salida)
        {
            await repository.SaveWithChildren(salida);
        }
    }
}