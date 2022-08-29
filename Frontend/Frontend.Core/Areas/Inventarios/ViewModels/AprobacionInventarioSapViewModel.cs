using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class AprobacionInventarioSapViewModel : BaseViewModel, IAprobacionInventarioSapViewModel
    {
        public ICommand GetInventariosSapAprobacionCommand { get; set; }
        public ICommand AprobarCommand { get; set; }
        public ICommand RechazarCommand { get; set; }
        public Task Refresh { get; set; }


        public ObservableRangeCollection<AprobacionInventarioModel> ListaInventariosSapAprobacion { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        private bool _hasInventario = true;
        public bool HasInventario
        {
            get { return _hasInventario; }
            set
            {
                SetProperty(ref _hasInventario, value);
            }
        }

        private AprobacionInventarioModel inventarioSelected;
        private bool todosDetallesComentados;
        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;

        public AprobacionInventarioModel InventarioSelected
        {
            get { return inventarioSelected; }
            set
            {
                SetProperty(ref inventarioSelected, value);
                GoToDetalleInventario(inventarioSelected);
            }
        }
        public AprobacionInventarioSapViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService,
            IInventarioService inventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            Init();
        }

        private void Init()
        {
            Title = "SAP";
            GetInventariosSapAprobacionCommand = new Command(async () => await RefreshInventariosProvisoriosAprobacionSap());
            AprobarCommand = new Command(async () => await Aprobar());
            RechazarCommand = new Command(async () => await Rechazar());
            ListaInventariosSapAprobacion = new ObservableRangeCollection<AprobacionInventarioModel>();
            Refresh = GetAllPendienteAprobacionSap();
        }

        private IList<Inventario> GetSelected()
        {
            return ListaInventariosSapAprobacion.Where(x => x.IsSelected).Select(x => x.Inventario).ToList();
        }

        private async Task Aprobar()
        {
            IsBusy = true;
            var list = GetSelected();
            if (list.Count <= 0)
            {
                Toast.ShowMessage("Debe seleccionar al menos un inventario");
                IsBusy = false;
                return;
            }
            var answer = await displayAlertService.Show("Aprobar inventarios", "¿Confirma que desea aprobar el/los inventario/s?", "Aceptar", "Cancelar");
            if (answer)
            {
                await inventarioService.SetToAprobado(list);
                Toast.ShowMessage("Inventario/s aprobado/s");
                await RefreshInventariosProvisoriosAprobacionSap();
            }
            IsBusy = false;
        }

        private async Task Rechazar()
        {
            IsBusy = true;
            var list = GetSelected();
            if (list.Count <= 0)
            {
                Toast.ShowMessage("Debe seleccionar al menos un inventario");
                IsBusy = false;
                return;
            }
            var answer = await displayAlertService.Show("Rechazar inventarios", "¿Confirma que desea rechazar el/los inventario/s?", "Aceptar", "Cancelar");
            if (answer)
            {
                var actualizados = await inventarioService.GetById(list);
                var tienenComentarios = ValidateTodosRechazadosConComentario(actualizados);
                if (tienenComentarios)
                {
                    await inventarioService.SetToRechazado(list);
                    Toast.ShowMessage("Inventario/s rechazado/s");
                    await RefreshInventariosProvisoriosAprobacionSap();
                    IsBusy = false;
                    return;
                }

                var respuesta = actualizados.Count > 1 ?
                      await displayAlertService.Show("Agregar comentario genérico", "¿Desea agregar un comentario genérico para el/los inventario/s?", "Aceptar", "Cancelar") :
                      await displayAlertService.Show("Agregar comentario", "¿Desea agregar un comentario  para el inventario?", "Aceptar", "Cancelar");

                if (respuesta)
                {
                    AgregarComentarioModel comentarioModel = new AgregarComentarioModel()
                    {
                        EsGenerico = actualizados.Count > 1 ? true : false,
                        Inventarios = actualizados.ToList(),
                        Retornar = false
                    };
                    navigationService.PushAsync<AprobacionInventarioProvisorioView, AgregarComentarioView>(comentarioModel);
                }
                else
                {
                    if (todosDetallesComentados)
                    {
                        Toast.ShowMessage("Todos los inventarios deben tener un comentario");
                    }
                    else
                    {
                        Toast.ShowMessage("Todos los inventarios deben tener un comentario o todas sus posiciones deben tener uno");
                    }
                    IsBusy = false;
                    return;
                }
            }
            IsBusy = false;
        }
        private bool ValidateTodosRechazadosConComentario(IList<Inventario> inventarios)
        {
            var todosComentados = inventarios.All(x => !String.IsNullOrEmpty(x.ComentarioRechazo));
            todosDetallesComentados = inventarios.All(x => x.DetallesInventario.All(di => !String.IsNullOrEmpty(di.Comentario)));
            return todosComentados || todosDetallesComentados;
        }

        private async Task RefreshInventariosProvisoriosAprobacionSap()
        {
            await GetAllPendienteAprobacionSap();
        }

        private async Task GetAllPendienteAprobacionSap()
        {
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            var delay = Task.Delay(2000);
            ListaInventariosSapAprobacion.Clear();
            var list = await inventarioService.GetAllPendienteAprobacionSap();
            var inventariosSap = list.Where(x => !x.EsProvisorio).Select(Inventario => new AprobacionInventarioModel(Inventario)).OrderByDescending(x => x.Inventario.FechaModificacion);
            ListaInventariosSapAprobacion.AddRange(inventariosSap);
            HasInventario = ListaInventariosSapAprobacion.Count > 0;
            await delay;
            IsRefreshing = false;
        }

        private async Task GoToDetalleInventario(AprobacionInventarioModel aprobacionInventarioModel)
        {
            if (aprobacionInventarioModel != null)
            {
                var inventarioWithChildren = await this.inventarioService.GetInventarioById(aprobacionInventarioModel.Inventario.Id);
                navigationService.PushAsync<AprobacionInventarioSapView, AprobacionDetalleInventarioSapView>(inventarioWithChildren);
            }
        }
    }
}
