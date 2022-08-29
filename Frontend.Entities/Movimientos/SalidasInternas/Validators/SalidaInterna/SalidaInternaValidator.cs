using System.Linq;

namespace Frontend.Business.Movimientos.SalidasInternas.Validators
{
    public class SalidaInternaValidator
    {
        private readonly DetalleSalidaInternaValidator detalleSalidaInternaValidator;

        public SalidaInternaValidator(DetalleSalidaInternaValidator detalleSalidaInternaValidator)
        {
            this.detalleSalidaInternaValidator = detalleSalidaInternaValidator;
        }

        public bool Validate(SalidaInterna salidaInterna)
        {
            return detalleSalidaInternaValidator.Validate(salidaInterna.DetallesSalidaInterna);
        }

        public bool HasContados(SalidaInterna salidaInterna)
        {
            return salidaInterna.DetallesSalidaInterna.Count(x => x.EsContado) > 0;
        }
    }
}
