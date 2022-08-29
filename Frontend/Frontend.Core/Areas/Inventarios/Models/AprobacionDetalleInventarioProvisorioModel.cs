using Frontend.Business.DetallesInventario;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class AprobacionDetalleInventarioProvisorioModel
    {
        public DetalleInventario DetalleInventario { get; set; }
        public bool IsSelected { get; set; }
        public string DisplayCodigoAlmacen => DetalleInventario.Inventario.Almacen != null ? DetalleInventario.Inventario.Almacen.Codigo : " - ";
        public Color ColorHayConteoErroneo
        {
            get
            {
                return DetalleInventario.HayConteoErroneo ?
                    (Color)Application.Current.Resources["YellowColor"] :
                    Color.White;
            }
        }
    }
}
