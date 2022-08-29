namespace Frontend.Business.Almacenes.Core
{
    public class AlmacenFactory
    {
        public Almacen Create(int idCentro)
        {
            return new Almacen()
            {
                IdCentro = idCentro
            };
        }
    }
}
