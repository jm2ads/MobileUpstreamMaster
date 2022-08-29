namespace Frontend.Business.Movimientos.Traslados.Validators
{
    public class TrasladoValidator
    {
        private readonly DetalleTrasladoValidator detalleTrasladoValidator;

        public TrasladoValidator(DetalleTrasladoValidator detalleTrasladoValidator)
        {
            this.detalleTrasladoValidator = detalleTrasladoValidator;
        }

        public bool Validate(Traslado traslado)
        {
            return detalleTrasladoValidator.Validate(traslado.DetallesTraslado);
        }
    }
}
