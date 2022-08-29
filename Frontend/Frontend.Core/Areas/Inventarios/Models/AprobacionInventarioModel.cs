using Frontend.Business.Inventarios;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class AprobacionInventarioModel
    {
        public Inventario Inventario { get; set; }
        public bool IsSelected { get; set; }
        public string DisplayCodigoAlmacen => Inventario.Almacen != null ? Inventario.Almacen.Codigo : " - ";
        public Color ColorHayDiferencia
        {
            get
            {
                return Inventario.HayDiferencia ?
                    (Color)Application.Current.Resources["RedColor"] :
                    (Color)Application.Current.Resources["GreenColor"];
            }
        }
        public Color ColorHayConteoErroneo
        {
            get
            {
                return Inventario.HayConteoErroneo ?
                    (Color)Application.Current.Resources["YellowColor"] :
                    Color.White;
            }
        }

        public AprobacionInventarioModel(Inventario inventario)
        {
            Inventario = inventario;
            IsSelected = false;
        }

    }
}
