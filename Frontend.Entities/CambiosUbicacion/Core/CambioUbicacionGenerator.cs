using Frontend.Business.Centros;
using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.CambiosUbicacion.Core
{
    public class CambioUbicacionGenerator
    {
        private readonly CambioUbicacionFactory cambioUbicacionFactory;
        private readonly IRepository<CambioUbicacion> repository;

        public CambioUbicacionGenerator(IRepository<CambioUbicacion> repository, CambioUbicacionFactory cambioUbicacionFactory)
        {
            this.repository = repository;
            this.cambioUbicacionFactory = cambioUbicacionFactory;
        }

        public async Task<CambioUbicacion> Generate(Centro centro, string usuario)
        {
            var cambioUbicacion = cambioUbicacionFactory.Create(centro, usuario);
            return await repository.SaveWithChildren(cambioUbicacion);
        }

        public async Task<CambioUbicacion> Generate(CambioUbicacion cambioUbicacion)
        {
            return await repository.SaveWithChildren(cambioUbicacion);
        }
    }
}
