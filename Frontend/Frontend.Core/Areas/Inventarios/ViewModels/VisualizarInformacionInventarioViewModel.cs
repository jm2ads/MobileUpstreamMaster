using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class VisualizarInformacionInventarioViewModel : BaseViewModel, IVisualizarInformacionInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;
        public Inventario inventario { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaRecuento { get; set; }

        private bool _isMotivoRechazoVisible = false;
        public bool IsMotivoRechazoVisible
        {
            get { return _isMotivoRechazoVisible; }
            set
            {
                SetProperty(ref _isMotivoRechazoVisible, value);
            }
        }
        private bool _isEnableAlmacen = false;
        public bool IsEnableAlmacen
        {
            get { return _isEnableAlmacen; }
            set
            {
                SetProperty(ref _isEnableAlmacen, value);
            }
        }

        public VisualizarInformacionInventarioViewModel(INavigationService navigationService, IInventarioService inventarioService)
        {
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            inventario = navigationService.GetNavigationParams<VisualizarInformacionInventarioView>() as Inventario;
            Title = "Inventario " + inventario.Codigo;
            FechaCreacion = inventario.FechaCreacion.ToString("dd/MM/yyyy");
            FechaRecuento = inventario.FechaRecuento == new DateTime() ? " - " : inventario.FechaRecuento.ToString("dd/MM/yyyy");
            IsEnableAlmacen = inventario.StockEspecial.Codigo.Trim() != "O";

            IsMotivoRechazoVisible = !string.IsNullOrEmpty(inventario.ComentarioRechazo);
        }
    }
}
