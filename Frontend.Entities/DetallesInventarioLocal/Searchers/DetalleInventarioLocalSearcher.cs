using Frontend.Business.Commons;
using Frontend.Business.DetallesInventarioLocal.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.DetallesInventarioLocal.Searchers
{
    public class DetalleInventarioLocalSearcher
    {
        private readonly IRepository<DetalleInventarioLocal> repository;
        private readonly DetalleInventarioLocalValidator detalleInventarioValidator;

        public DetalleInventarioLocalSearcher(IRepository<DetalleInventarioLocal> repository, DetalleInventarioLocalValidator detalleInventarioValidator)
        {
            this.repository = repository;
            this.detalleInventarioValidator = detalleInventarioValidator;
        }
        public DetalleInventarioLocal GetDetalleInventarioDuplicated(DetalleInventarioLocal detalleInventario, List<DetalleInventarioLocal> list)
        {
            return list.FirstOrDefault(x => detalleInventarioValidator.IsEqual(x, detalleInventario));
        }
    }
}
