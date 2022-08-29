using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.Traslados.Core
{
    public class TrasladoGenerator
    {
        private readonly IRepository<Traslado> repository;
        private readonly TrasladoFactory trasladoFactory;

        public TrasladoGenerator(IRepository<Traslado> repository, TrasladoFactory trasladoFactory)
        {
            this.repository = repository;
            this.trasladoFactory = trasladoFactory;
        }

        public async Task<Traslado> Generate(string usuario)
        {

            var traslado = trasladoFactory.Create(usuario);
            return await repository.SaveWithChildren(traslado);
        }
    }
}
