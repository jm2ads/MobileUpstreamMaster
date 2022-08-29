using Frontend.Business.Inventarios;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class ListaInventarioRechazadoModel
    {
        public Inventario Inventario { get; set; }
        public Color ColorHayConteoErroneo
        {
            get
            {
                return Inventario.HayConteoErroneo ?
                    (Color)Application.Current.Resources["YellowColor"] :
                    Color.White;
            }
        }

        public ListaInventarioRechazadoModel(Inventario inventario)
        {
            Inventario = inventario;
        }

    }
}
