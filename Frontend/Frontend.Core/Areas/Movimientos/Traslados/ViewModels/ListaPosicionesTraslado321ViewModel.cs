using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Traslados;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Movimientos.Traslados.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Traslados.ViewModels
{
    public class ListaPosicionesTraslado321ViewModel : BaseViewModel, IListaPosicionesTraslado321ViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly ITrasladoService trasladoService;
        private readonly ISettingsService settingsService;

        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }
        public ICommand GoToCabeceraCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand DeleteMaterialCommnad { get; set; }
        public ICommand OnBackButtonPressedCommnad { get; set; }


        private DetalleTraslado detalleTrasladoSelected;
        public DetalleTraslado DetalleTrasladoSelected
        {
            get { return detalleTrasladoSelected; }
            set
            {
                SetProperty(ref detalleTrasladoSelected, value);
                GoToDetalle(detalleTrasladoSelected);
            }
        }

        private Traslado _traslado;
        public Traslado traslado
        {
            get { return _traslado; }
            set
            {
                SetProperty(ref _traslado, value);
            }
        }

        public ObservableRangeCollection<DetalleTraslado> ListaDetallesTraslado { get; set; }
        private IList<DetalleTraslado> detalleTrasladoList;

        public ListaPosicionesTraslado321ViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            ITrasladoService trasladoService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.trasladoService = trasladoService;
            this.settingsService = settingsService;
            Init();
        }

        private async void Init()
        {
            traslado = navigationService.GetNavigationParams<ListaPosicionesTraslado321View>() as Traslado;
            Title = "Traslado " + traslado.NumeroProvisorio;
            ListaDetallesTraslado = new ObservableRangeCollection<DetalleTraslado>();
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            FinalizarCommand = new Command(Finalizar);
            GoToCabeceraCommand = new Command(GoToCabecera);
            AgregarCommand = new Command(Agregar);
            DeleteMaterialCommnad = new Command<DetalleTraslado>(DeleteMaterial);
            OnBackButtonPressedCommnad = new Command(OnBackButtonPressed);
            detalleTrasladoList = traslado.DetallesTraslado;

            FillListaDetallesTraslado();
        }

        private async void OnBackButtonPressed()
        {
            var answer = await displayAlertService.Show("Aviso", "Si continua perderá el progreso ¿Desea continuar?", "Aceptar", "Cancelar");
            if (answer)
            {
                navigationService.PopAsync<ListaPosicionesTraslado321View>();
            }
        }

        private async void DeleteMaterial(DetalleTraslado detalleTraslado)
        {
            var answer = await displayAlertService.Show("Eliminar material", "¿Desea eliminar el material del traslado?", "Aceptar", "Cancelar");
            if (answer)
            {
                traslado.DetallesTraslado.Remove(detalleTraslado);
                ListaDetallesTraslado.Remove(detalleTraslado);
                await trasladoService.Delete(detalleTraslado);
                await trasladoService.Update(traslado);

                if (traslado.DetallesTraslado.Count == 0)
                {
                    await trasladoService.Delete(traslado);
                    navigationService.PushFromRootAsync<HomeView>();
                }
            }
        }

        private void Agregar()
        {
            navigationService.PushAsync<ListaPosicionesTraslado321View, Traslado321PorMaterialView>(traslado);
        }

        private void GoToCabecera()
        {
            navigationService.PushAsync<ListaPosicionesTraslado321View, VisualizarCabeceraTrasladoView>(traslado);
        }

        private void FiltroPosicion(string value)
        {
            ListaDetallesTraslado.ReplaceRange(detalleTrasladoList.Where(x => string.IsNullOrEmpty(value) || x.DisplayPosicion.Contains(value)));
        }

        public void FillListaDetallesTraslado()
        {
            ListaDetallesTraslado.Clear();
            ListaDetallesTraslado.AddRange(traslado.DetallesTraslado.OrderBy(x => x.Posicion));
        }

        private async void Finalizar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El traslado contiene errores.");
                return;
            }

            var answer = await displayAlertService.Show("Finalizar traslado", "¿Desea finalizar el traslado?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    traslado.Estado = EstadoMovimiento.PendienteAprobacionSap;
                    await trasladoService.Update(traslado, Business.Synchronizer.SyncState.PendingToSync);
                    await settingsService.SetPendingToSync(true);
                    Toast.ShowMessage("El/los material/es ha/n sido ingresado/s con éxito");
                    navigationService.PushFromRootAsync<HomeView>();
                }
                catch (BusinessException be)
                {
                    Toast.ShowMessage(be.Mensaje);
                }
            }
        }

        private bool Validate()
        {
            return trasladoService.Validate(traslado);
        }

        private void GoToDetalle(DetalleTraslado detalleTraslado)
        {
            if (detalleTraslado != null)
            {
                navigationService.PushAsync<ListaPosicionesTraslado321View, DetalleMaterialTraslado321View>(detalleTraslado);
            }
        }
    }
}