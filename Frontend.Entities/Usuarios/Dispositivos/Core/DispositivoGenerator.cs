using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Dispositivos.Core
{
    public class DispositivoGenerator
    {
        private readonly IRepository<Dispositivo> repository;

        public DispositivoGenerator(IRepository<Dispositivo> repository)
        {
            this.repository = repository;
        }

        public async Task Generate(Dispositivo dispositivo)
        {
            await repository.SaveWithChildren(dispositivo);
        }
    }
}
