using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class RecuentoInventarioViewModel : BaseViewModel, IRecuentoInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;

        public ICommand SearchInventarioCommand { get; set; }
        public ICommand GetAllInventarioCommand { get; set; }


        private System.Collections.IList _listCodigoInventario;
        public System.Collections.IList ListCodigoInventario
        {
            get { return _listCodigoInventario; }
            set
            {
                SetProperty(ref _listCodigoInventario, value);
            }
        }

        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set { SetProperty(ref _searchValue, value); }
        }

        public RecuentoInventarioViewModel(INavigationService navigationService, IInventarioService inventarioService)
        {
            Title = "Inventario";
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            SearchInventarioCommand = new Command(SearchInventario);
            GetAllInventarioCommand = new Command(GetAllInventario);
        }

        private async void GetAllInventario(object obj)
        {
            ListCodigoInventario = (System.Collections.IList)(await inventarioService.GetAllCodigoAutocomplete());
        }

        private async void SearchInventario()
        {

            try
            {
                var inventario = await inventarioService.GetInventarioBy(SearchValue.PadLeft(10,'0'), Frontend.Commons.Enums.EstadoInventario.Recuento);
                navigationService.PushAsync<RecuentoInventarioView, RecuentoDetalleInventarioView>(inventario);
            }
            catch (System.Exception e)
            {
                Toast.ShowMessage("Por favor, ingrese un número válido y vuelva a intentar.");
            }
        }
    }
}
