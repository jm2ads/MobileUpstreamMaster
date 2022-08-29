using Frontend.Business.InventariosMasivos;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.InventariosMasivos.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.InventariosMasivos.ViewModels
{
    public class ListadoPosicionesInventarioMasivoViewModel : BaseViewModel, IListadoPosicionesInventarioMasivoViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IInventarioMasivoService inventarioMasivoService;
        private readonly ISettingsService settingsService;
        private readonly IStockService stockService;

        public ICommand FiltroPosicionCommand { get; set; }
        public ICommand FinalizarCommand { get; set; }
        public ICommand GoToCabeceraCommand { get; set; }
        public ICommand DeleteMaterialCommnad { get; set; }
        public ICommand AgregarMaterialCommnad { get; set; }
        public ICommand OnBackButtonPressedCommnad { get; set; }
        public ICommand DuplicarMaterialCommnad { get; set; }


        private DetalleInventarioMasivo detalleInventarioMasivoSelected;
        public DetalleInventarioMasivo DetalleInventarioMasivoSelected
        {
            get { return detalleInventarioMasivoSelected; }
            set
            {
                SetProperty(ref detalleInventarioMasivoSelected, value);
                GoToDetalle(detalleInventarioMasivoSelected);
            }
        }

        private InventarioMasivo _inventarioMasivo;
        public InventarioMasivo inventarioMasivo
        {
            get { return _inventarioMasivo; }
            set
            {
                SetProperty(ref _inventarioMasivo, value);
            }
        }

        public ObservableRangeCollection<DetalleInventarioMasivo> ListaDetallesInventarioMasivo { get; set; }
        private List<DetalleInventarioMasivo> detalleInventarioMasivoList;

        public ListadoPosicionesInventarioMasivoViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            IInventarioMasivoService inventarioMasivoService, ISettingsService settingsService, IStockService stockService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.inventarioMasivoService = inventarioMasivoService;
            this.settingsService = settingsService;
            this.stockService = stockService;
            Init();
        }

        private async void Init()
        {
            inventarioMasivo = navigationService.GetNavigationParams<ListadoPosicionesInventarioMasivoView>() as InventarioMasivo;
            Title = "Inventario masivo" + inventarioMasivo.NumeroProvisorio;
            DeleteMaterialCommnad = new Command<DetalleInventarioMasivo>(async (detalleInventario) => await DeleteMaterial(detalleInventario));
            AgregarMaterialCommnad = new Command(AgregarMaterial);
            DuplicarMaterialCommnad = new Command<DetalleInventarioMasivo>(DuplicarMaterial);
            ListaDetallesInventarioMasivo = new ObservableRangeCollection<DetalleInventarioMasivo>();
            FiltroPosicionCommand = new Command<string>(FiltroPosicion);
            OnBackButtonPressedCommnad = new Command(OnBackButtonPressed);
            FinalizarCommand = new Command(Finalizar);
            GoToCabeceraCommand = new Command(GoToCabecera);
            detalleInventarioMasivoList = new List<DetalleInventarioMasivo>();

            FillListaDetallesInventarioMasivo();
        }

        private async void OnBackButtonPressed()
        {
            var answer = await displayAlertService.Show("Aviso", "Si continua perderá el progreso ¿Desea continuar?", "Aceptar", "Cancelar");
            if (answer)
            {
                navigationService.PopAsync<ListadoPosicionesInventarioMasivoView>();
            }
        }

        private async void DuplicarMaterial(DetalleInventarioMasivo detalleInventario)
        {
            var detalleInventarioMasivo = await inventarioMasivoService.Duplicar(detalleInventario);
            if (detalleInventarioMasivo.TipoLote == TipoLote.Mixto)
            {
                var respuesta = await displayAlertService.Show("Duplicar", "Este material posee stock que pertenece a lotes nuevos y usados. Seleccione cual está contando.", "Nuevo", "Usado");
                if (respuesta)
                {
                    detalleInventarioMasivo.TipoLote = TipoLote.Nuevo;
                }
                else
                {
                    detalleInventarioMasivo.TipoLote = TipoLote.Usado;
                }
            }
            navigationService.PushAsync<ListadoPosicionesInventarioMasivoView, DetalleMaterialInventarioMasivoView>(detalleInventarioMasivo);
        }

        private void AgregarMaterial()
        {
            navigationService.PushAsync<ListadoPosicionesInventarioMasivoView, SearchMaterialMasivoView>(inventarioMasivo);
        }

        private void GoToCabecera()
        {
            navigationService.PushAsync<ListadoPosicionesInventarioMasivoView, VisualizarInformacionInventarioMasivoView>(inventarioMasivo);
        }

        private void FiltroPosicion(string value)
        {
            ListaDetallesInventarioMasivo.ReplaceRange(detalleInventarioMasivoList.Where(x => string.IsNullOrEmpty(value) || x.DisplayPosicion.Contains(value)));
        }

        public void FillListaDetallesInventarioMasivo()
        {
            ListaDetallesInventarioMasivo.Clear();
            detalleInventarioMasivoList.AddRange(inventarioMasivo.DetallesInventarioMasivo.OrderBy(x => x.Posicion));
            ListaDetallesInventarioMasivo.AddRange(detalleInventarioMasivoList);

        }

        private async void Finalizar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El inventario masivo contiene errores.");
                return;
            }

            var answerFinalizar = await displayAlertService.Show("Finalizar inventario masivo", "¿Desea finalizar el inventario?", "Aceptar", "Cancelar");
            if (answerFinalizar)
            {
                try
                {
                    IsBusy = true;
                    var inventarioMasivoDistribuido = await inventarioMasivoService.Distribuir(inventarioMasivo);
                    if (!inventarioMasivoService.ValidateDistribuido(inventarioMasivoDistribuido))
                    {
                        var materialesSinStock = inventarioMasivoDistribuido.DetallesInventarioMasivo
                            .Where(detalleInventarioMasivo => detalleInventarioMasivo.Cantidad == 0
                                && (detalleInventarioMasivo.TipoStockId == 1 ? detalleInventarioMasivo.Stock.CantidadAlmacen :
                                    detalleInventarioMasivo.TipoStockId == 2 ? detalleInventarioMasivo.Stock.CantidadBloqueado : 
                                    detalleInventarioMasivo.Stock.CantidadCalidad) != 0)
                            .Select(detalleInventarioMasivo => detalleInventarioMasivo.Stock.Material.Codigo).Distinct().ToList();

                        var answerMaterialesSinStock = await displayAlertService.Show(
                            "Finalizar inventario masivo",
                            string.Format("La cantidad ingresada en algunos materiales no contempla todos los almacenes. Materiales: {0}.", string.Join(", ", materialesSinStock.ToArray()))
                            , "Continuar"
                            , "Modificar");
                        if (!answerMaterialesSinStock)
                        {
                            return;
                        }

                    }

                    await inventarioMasivoService.Insert(inventarioMasivoDistribuido, Business.Synchronizer.SyncState.PendingToSync);
                    await settingsService.SetPendingToSync(true);
                    await inventarioMasivoService.Delete(inventarioMasivo);

                    #region ASOSA flagSync Crear inventario masivo
                    Frontend.Core.Commons.Globals.flagSync = "ListadoPosicionesInventarioMasivoViewModel";
                    #endregion

                    Toast.ShowMessage("El inventario ha finalizado con éxito.");
                    navigationService.PushFromRootAsync<HomeView>();
                }
                catch (BusinessException be)
                {
                    Toast.ShowMessage(be.Mensaje);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private async Task DeleteMaterial(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            var answer = await displayAlertService.Show("Eliminar material", "¿Desea eliminar el material del inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                inventarioMasivo.DetallesInventarioMasivo.Remove(detalleInventarioMasivo);
                ListaDetallesInventarioMasivo.Remove(detalleInventarioMasivo);

                await inventarioMasivoService.Delete(detalleInventarioMasivo);
                await inventarioMasivoService.Update(inventarioMasivo);

                if (inventarioMasivo.DetallesInventarioMasivo.Count == 0)
                {
                    await inventarioMasivoService.Delete(inventarioMasivo);
                    navigationService.PushFromRootAsync<HomeView>();
                }
            }
        }

        private bool Validate()
        {
            return inventarioMasivoService.Validate(inventarioMasivo);
        }

        private void GoToDetalle(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            if (detalleInventarioMasivo != null)
            {
                navigationService.PushAsync<ListadoPosicionesInventarioMasivoView, DetalleMaterialInventarioMasivoView>(detalleInventarioMasivo);
            }
        }
    }
}
