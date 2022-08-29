using Frontend.Business.DetallesInventario;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class VisualizarDetalleMaterialViewModel: BaseViewModel, IVisualizarDetalleMaterialViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ITipoStockService tipoStockService;

        public ICommand AceptarCommand { get; set; }

        private string _comentario;
        public string Comentario
        {
            get { return _comentario; }
            set
            {
                SetProperty(ref _comentario, value);
            }
        }

        private string _displayTipoStock;
        public string DisplayTipoStock
        {
            get { return _displayTipoStock; }
            set
            {
                SetProperty(ref _displayTipoStock, value);
            }
        }

        private DetalleInventarioModel _detalleInventarioModel;
        public DetalleInventarioModel DetalleInventarioModel
        {
            get { return _detalleInventarioModel; }
            set
            {
                SetProperty(ref _detalleInventarioModel, value);
            }
        }

        public DetalleInventario detalleInventario { get; set; }

        public bool IsPepEnabled { get { return detalleInventario.Inventario.StockEspecial.Codigo.Trim() == "Q"; } }
        public bool ShowComentario { get { return DetalleInventarioModel.ShowComentario; } }
        public bool ShowCantidad { get { return DetalleInventarioModel.ShowCantidad; } }

        public bool IsProveedorEnabled { get { return detalleInventario.Inventario.StockEspecial.Codigo.Trim() == "K" || detalleInventario.Inventario.StockEspecial.Codigo.Trim() == "O"; } }

        public VisualizarDetalleMaterialViewModel(INavigationService navigationService, IClaseDeValoracionService claseDeValoracionService,
            IDetalleInventarioService detalleInventarioService, IDisplayAlertService displayAlertService, ITipoStockService tipoStockService)
        {
            this.navigationService = navigationService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.detalleInventarioService = detalleInventarioService;
            this.displayAlertService = displayAlertService;
            this.tipoStockService = tipoStockService;
            Init();
        }

        private void Init()
        {
            DetalleInventarioModel = navigationService.GetNavigationParams<VisualizarDetalleMaterialView>() as DetalleInventarioModel;
            detalleInventario = DetalleInventarioModel.DetalleInventario;
            Comentario = detalleInventario.Comentario;
            Title = "Detalle del material";
            AceptarCommand = new Command(Aceptar);

            DisplayTipoStock = tipoStockService.GetById(detalleInventario.TipoStockId).Descripcion;
        }

        private void Aceptar(object obj)
        {
            if (detalleInventario.Comentario != Comentario) detalleInventario.Comentario = Comentario;
            detalleInventarioService.Update(detalleInventario);
            navigationService.PopAsync<ConsultaStockView>();
        }
    }
}
