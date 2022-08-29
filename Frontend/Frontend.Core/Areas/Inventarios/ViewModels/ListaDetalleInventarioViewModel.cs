using Frontend.Business.Commons;
using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class ListaDetalleInventarioViewModel : BaseViewModel, IListaDetalleInventarioViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IInventarioService inventarioService;
        private readonly IDetalleInventarioService detalleInventarioService;
        private readonly IInventarioLocalService inventarioLocalService;

        public ICommand GoToSearchMaterialCommand { get; set; }
        public ICommand DeleteMaterialCommnad { get; set; }
        public ICommand FinalizarInventarioCommand { get; set; }
        public ICommand GoToInformacionInventarioCommand { get; set; }
        public ICommand DuplicarMaterialCommnad { get; set; }

        public ObservableRangeCollection<ListaDetalleInventarioModel> ListaDetallesDeInventario { get; set; }

        private ListaDetalleInventarioModel detalleInventarioSelected;
        public ListaDetalleInventarioModel DetalleInventarioSelected
        {
            get { return detalleInventarioSelected; }
            set
            {
                SetProperty(ref detalleInventarioSelected, value);
                GoToDetalleInventario(detalleInventarioSelected);
            }
        }

        public Inventario inventario { get; set; }

        public ListaDetalleInventarioViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService, IInventarioService inventarioService,
            IDetalleInventarioService detalleInventarioService, IInventarioLocalService inventarioLocalService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.inventarioService = inventarioService;
            this.detalleInventarioService = detalleInventarioService;
            this.inventarioLocalService = inventarioLocalService;
            Init();
        }

        private void Init()
        {
            GoToSearchMaterialCommand = new Command(GoToSearchMaterial);
            DeleteMaterialCommnad = new Command<ListaDetalleInventarioModel>(async (listaDetalleInventarioModel) => await DeleteMaterial(listaDetalleInventarioModel));
            FinalizarInventarioCommand = new Command(async () => await FinalizarInventario());
            GoToInformacionInventarioCommand = new Command(GoToInformacionInventario);
            DuplicarMaterialCommnad = new Command<ListaDetalleInventarioModel>(DuplicarMaterial);

            inventario = navigationService.GetNavigationParams<ListaDetalleInventarioView>() as Inventario;

            Title = "Inventario " + inventario.Codigo;
            ListaDetallesDeInventario = new ObservableRangeCollection<ListaDetalleInventarioModel>();

            var getDetallesInventario = GetDetallesInventario();
        }

        private void DuplicarMaterial(ListaDetalleInventarioModel listaDetalleInventarioModel)
        {
            var listaDetalleInventarioModelDuplicado = new ListaDetalleInventarioModel(detalleInventarioService.Duplicar(listaDetalleInventarioModel.DetalleInventario));
            GoToDetalleInventario(listaDetalleInventarioModelDuplicado);
        }

        private void GoToInformacionInventario()
        {
            navigationService.PushAsync<ListaDetalleInventarioView, VisualizarInformacionInventarioView>(inventario);
        }

        private async Task GetDetallesInventario()
        {
            IList<DetalleInventario> listaDetallesDeInventario;
            if (inventario.EsProvisorio)
                listaDetallesDeInventario = inventario.DetallesInventario;
            else
                listaDetallesDeInventario = await detalleInventarioService.GetByIdInventario(inventario.Id);

            ListaDetallesDeInventario.AddRange(listaDetallesDeInventario.Select(x=>new ListaDetalleInventarioModel(x)).OrderBy(x=>x.DetalleInventario.Posicion));
        }

        private async Task FinalizarInventario()
        {
            IsBusy = true;
            var answer = await displayAlertService.Show("Finalizar inventario", "¿Desea finalizar el inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                try
                {
                    if (inventarioService.IsValidToFinish(inventario))
                    {
                        if (inventario.inventarioLocalId == 0)
                        {
                            inventario = await inventarioService.GetInventarioById(inventario.Id);
                            await inventarioService.SetToPendienteAprobacion(inventario);
                        }
                        else
                        {
                            var inventarioLocal = await inventarioLocalService.GetInventarioById(inventario.inventarioLocalId);
                            await inventarioLocalService.SetToPendienteAprobacion(inventarioLocal);
                        }

                        #region ASOSA flagSync Crear Inventario
                        Frontend.Core.Commons.Globals.flagSync = "ListaDetalleInventarioViewModel";
                        #endregion

                        Toast.ShowMessage("El inventario ha finalizado con éxito.");
                        IsBusy = false;
                        navigationService.PushNextMasterDetailPage<HomeView, HomeView>();
                    }

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

            IsBusy = false;
        }

        private async Task DeleteMaterial(ListaDetalleInventarioModel listaDetalleInventarioModel)
        {
            var answer = await displayAlertService.Show("Eliminar material", "¿Desea eliminar el material del inventario?", "Aceptar", "Cancelar");
            if (answer)
            {
                inventario.DetallesInventario.Remove(listaDetalleInventarioModel.DetalleInventario);
                ListaDetallesDeInventario.Remove(listaDetalleInventarioModel);

                if (inventario.Id == 0) //Creando inventario
                {
                    var inventarioLocal = Helper.MapToInventarioLocal(inventario);
                    inventarioLocal.Id = inventario.inventarioLocalId;
                    var inventarioSaved = await inventarioLocalService.Save(inventarioLocal);
                    
                    if (inventario.DetallesInventario.Count == 0)
                    {
                        await inventarioService.Delete(inventario);
                        await inventarioLocalService.Delete(inventarioLocal);
                        navigationService.PushFromRootAsync<HomeView>();
                    }
                }
                else
                {
                    await detalleInventarioService.Delete(listaDetalleInventarioModel.DetalleInventario);
                    await inventarioService.UpdateWithChildren(inventario);

                    if (inventario.DetallesInventario.Count == 0)
                    {
                        await inventarioService.Delete(inventario);
                        navigationService.PushFromRootAsync<HomeView>();
                    }
                }
            }
        }

        private void GoToSearchMaterial()
        {
            navigationService.PushAsync<ListaDetalleInventarioView, SearchMaterialView>(inventario);
        }

        private void GoToDetalleInventario(ListaDetalleInventarioModel listaDetalleInventarioModel)
        {
            if (listaDetalleInventarioModel != null)
            {
                var detalle = new DetalleInventarioModel()
                {
                    DetalleInventario = listaDetalleInventarioModel.DetalleInventario,
                    ShowComentario = true,
                    ShowCantidad = true
                };
                navigationService.PushAsync<ListaDetalleInventarioView, CrearDetalleInventarioView>(detalle);
            }
        }
    }
}
