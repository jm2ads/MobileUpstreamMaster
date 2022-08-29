using System.Linq;

namespace Frontend.Business.InventariosMasivos.Validations
{
    public class InventarioMasivoValidator
    {
        private readonly DetalleInventarioMasivoValidator detalleInventarioMasivoValidator;

        public InventarioMasivoValidator(DetalleInventarioMasivoValidator detalleInventarioMasivoValidator)
        {
            this.detalleInventarioMasivoValidator = detalleInventarioMasivoValidator;
        }

        public bool Validate(InventarioMasivo inventarioMasivo)
        {
            var isValid = true;

            foreach (var detalleInventarioMasivo in inventarioMasivo.DetallesInventarioMasivo)
            {
                isValid = isValid && detalleInventarioMasivoValidator.Validate(detalleInventarioMasivo);
            }

            return isValid;
        }

        public bool ValidateDistribuido(InventarioMasivo inventarioMasivo)
        {
            return inventarioMasivo.DetallesInventarioMasivo.All(detalleInventarioMasivo => detalleInventarioMasivo.Cantidad != 0
                                || (detalleInventarioMasivo.TipoStockId == 1 ? detalleInventarioMasivo.Stock.CantidadAlmacen :
                                    detalleInventarioMasivo.TipoStockId == 2 ? detalleInventarioMasivo.Stock.CantidadBloqueado :
                                    detalleInventarioMasivo.Stock.CantidadCalidad) == 0);
        }
    }
}
