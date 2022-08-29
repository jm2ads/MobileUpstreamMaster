using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Dispositivos.Core
{
    public class DispositivoUpdater
    {
        private readonly IRepository<Dispositivo> repository;

        public DispositivoUpdater(IRepository<Dispositivo> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Dispositivo dispositivo)
        {
            await repository.UpdateWithChildren(dispositivo);
        }
    }
}
