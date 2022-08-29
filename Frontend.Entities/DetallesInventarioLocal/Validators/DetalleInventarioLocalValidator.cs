using Frontend.Business.ClasesDeValoracion.Validations;
using Frontend.Business.Materiales.Validations;

namespace Frontend.Business.DetallesInventarioLocal.Validators
{
    public class DetalleInventarioLocalValidator
    {
        private readonly ClaseDeValoracionValidator claseDeValoracionValidator;
        private readonly MaterialValidator materialValidator;

        public DetalleInventarioLocalValidator(ClaseDeValoracionValidator claseDeValoracionValidator, MaterialValidator materialValidator)
        {
            this.claseDeValoracionValidator = claseDeValoracionValidator;
            this.materialValidator = materialValidator;
        }
        public bool IsEqual(DetalleInventarioLocal detalleInventarioA, DetalleInventarioLocal detalleInventarioB)
        {
            return detalleInventarioA.TipoStockId == detalleInventarioB.TipoStockId
                && claseDeValoracionValidator.IsEqual(detalleInventarioA.Lote, detalleInventarioB.Lote)
                && detalleInventarioA.DetalleStockEspecialId == detalleInventarioB.DetalleStockEspecialId
                && materialValidator.IsEqual(detalleInventarioA.Stock.Material, detalleInventarioB.Stock.Material);
        }
    }
}
