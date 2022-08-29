namespace Frontend.Business.ClasesDeValoracion.Validations
{
    public class ClaseDeValoracionValidator
    {
        public bool IsEqual(ClaseDeValoracion claseDeValoracionA, ClaseDeValoracion claseDeValoracionB)
        {
            return claseDeValoracionA.Codigo == claseDeValoracionB.Codigo;
        }
    }
}
