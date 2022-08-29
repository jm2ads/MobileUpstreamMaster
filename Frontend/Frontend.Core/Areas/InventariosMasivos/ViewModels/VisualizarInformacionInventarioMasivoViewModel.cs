using Frontend.Business.InventariosMasivos;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class VisualizarInformacionInventarioMasivoViewModel : BaseViewModel, IVisualizarInformacionInventarioMasivoViewModel
    {
        private readonly INavigationService navigationService;
        private InventarioMasivo _inventarioMasivo;
        public InventarioMasivo inventarioMasivo
        {
            get { return _inventarioMasivo; }
            set
            {
                SetProperty(ref _inventarioMasivo, value);
            }
        }

        private string _FechaDocumento;
        public string FechaDocumento
        {
            get { return _FechaDocumento; }
            set
            {
                SetProperty(ref _FechaDocumento, value);
            }
        }

        private string _almacenesExcluidos;
        public string AlmacenesExcluidos
        {
            get { return _almacenesExcluidos; }
            set
            {
                SetProperty(ref _almacenesExcluidos, value);
            }
        }

        private string _FechaCreacion;
        public string FechaCreacion
        {
            get { return _FechaCreacion; }
            set
            {
                SetProperty(ref _FechaCreacion, value);
            }
        }
        
        public VisualizarInformacionInventarioMasivoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Init();
        }

        private void Init()
        {
            inventarioMasivo = navigationService.GetNavigationParams<VisualizarInformacionInventarioMasivoView>() as InventarioMasivo;
            Title = "Inventario masivo" + inventarioMasivo.NumeroProvisorio;
            FechaCreacion = inventarioMasivo.FechaCreacion.ToString("dd/MM/yyyy");
            FechaDocumento = inventarioMasivo.FechaDocumento.ToString("dd/MM/yyyy");
            AlmacenesExcluidos = inventarioMasivo.AlmacenesExcluidos.Count() == 0 ? "No hay almacenes excluidos" : "Almacenes excluidos: " + String.Join(Environment.NewLine, inventarioMasivo.AlmacenesExcluidos.Select(x => x.DisplayDescription));
        }
    }
}
