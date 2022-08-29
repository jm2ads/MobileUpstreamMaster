using Frontend.Business.Commons;

namespace Frontend.Business.Movimientos.Traslados.Searchers
{
    public class DetalleTrasladoSearcher
    {
        private readonly IRepository<Traslado> repository;

        public DetalleTrasladoSearcher(IRepository<Traslado> repository)
        {
            this.repository = repository;
        }
    }
}
