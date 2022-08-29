using Frontend.Business.DetallesInventario;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class ListaDetalleInventarioModel
    {
        public DetalleInventario DetalleInventario { get; set; }

        public ListaDetalleInventarioModel(DetalleInventario detalleInventario)
        {
            DetalleInventario = detalleInventario;
        }
    }
}
