using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.SalidasInternas.Core
{
    public class DetalleSalidaInternaGenerator
    {
        private readonly IRepository<DetalleSalidaInterna> repository;

        public DetalleSalidaInternaGenerator(IRepository<DetalleSalidaInterna> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(DetalleSalidaInterna detalleSalida)
        {
            await repository.SaveWithChildren(detalleSalida);
        }
    }
}