using System;
using System.Collections.Generic;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;

namespace Frontend.Core.Areas.Movimientos.Traslados.ViewModels
{
    public class VisualizarCabeceraTrasladoViewModel : BaseViewModel, IVisualizarCabeceraTrasladoViewModel
    {
        private Traslado _traslado;
        private readonly INavigationService navigationService;

        public Traslado traslado
        {
            get { return _traslado; }
            set
            {
                SetProperty(ref _traslado, value);
            }
        }

        private string _fechaContabilizacion;
        public string FechaContabilizacion
        {
            get { return _fechaContabilizacion; }
            set
            {
                SetProperty(ref _fechaContabilizacion, value);
            }
        }

        private string _fechaDocumentacion;
        public string FechaDocumentacion
        {
            get { return _fechaDocumentacion; }
            set
            {
                SetProperty(ref _fechaDocumentacion, value);
            }
        }

        private string _claseDeMovimiento;
        public string ClaseDeMovimiento
        {
            get { return _claseDeMovimiento; }
            set
            {
                SetProperty(ref _claseDeMovimiento, value);
            }
        }
        private Dictionary<int, string> claseDeMovimientoTraslado { get; set; }


        public VisualizarCabeceraTrasladoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Init();
        }

        private void Init()
        {
            traslado = navigationService.GetNavigationParams<VisualizarCabeceraTrasladoView>() as Traslado;
            Title = "Traslado " + traslado.NumeroProvisorio;
            _claseDeMovimiento = traslado.ClaseDeMovimientoCodigo;
            FechaContabilizacion = traslado.FechaContabilizacion.ToString("dd/MM/yyyy");
            FechaDocumentacion = traslado.FechaDocumento.ToString("dd/MM/yyyy");
        }
    }
}
