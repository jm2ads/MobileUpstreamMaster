using Frontend.Business.ClasesDeValoracion.Validations;
using Frontend.Business.Materiales.Validations;

namespace Frontend.Business.DetallesInventario.Validators
{
    public class DetalleInventarioValidator
    {
        private readonly ClaseDeValoracionValidator claseDeValoracionValidator;
        private readonly MaterialValidator materialValidator;

        public DetalleInventarioValidator(ClaseDeValoracionValidator claseDeValoracionValidator, MaterialValidator materialValidator)
        {
            this.claseDeValoracionValidator = claseDeValoracionValidator;
            this.materialValidator = materialValidator;
        }
        public bool IsEqual(DetalleInventario detalleInventarioA, DetalleInventario detalleInventarioB)
        {
            return  detalleInventarioA.TipoStockId == detalleInventarioB.TipoStockId
                && detalleInventarioA.DetalleStockEspecialId == detalleInventarioB.DetalleStockEspecialId
                && claseDeValoracionValidator.IsEqual(detalleInventarioA.Lote, detalleInventarioB.Lote)
                && materialValidator.IsEqual(detalleInventarioA.Stock.Material, detalleInventarioB.Stock.Material)
                && detalleInventarioA.Id != detalleInventarioB.Id;
        }
    }
}
