using Frontend.Business.Centros;
using Frontend.Business.Commons;
using Frontend.Business.Settings;
using Frontend.Commons.Commons;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.IPlataformControls;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.Commons.Validations;
using Frontend.Core.IViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.ViewModels
{
    public class CambioCentroViewModel : BaseViewModel, ICambioCentroViewModel
    {
        private readonly ISettingsService settingsService;
        private readonly INavigationService navigationService;
        private readonly ICentroService centroService;
        private readonly IDatabaseManager databaseManager;
        private readonly IUsuarioService usuarioService;
        private readonly ISyncService syncService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly INetworkConnection networkConnection;

        public ICommand CambiarCentroCommand { get; set; }

        private Setting _setting;
        public Setting setting
        {
            get { return _setting; }
            set { SetProperty(ref _setting, value); }
        }

        private ObservableRangeCollection<Centro> listaCentros;
        public ObservableRangeCollection<Centro> ListaCentros
        {
            get { return listaCentros; }
            set { SetProperty(ref listaCentros, value); }
        }

        private ValidatableObject<Centro> centro;
        public ValidatableObject<Centro> Centro
        {
            get { return centro; }
            set { SetProperty(ref centro, value); }
        }

        public CambioCentroViewModel(ISettingsService settingsService, INavigationService navigationService,
            ICentroService centroService, IDatabaseManager databaseManager, IUsuarioService usuarioService,
            ISyncService syncService, IDisplayAlertService displayAlertService)
        {
            this.settingsService = settingsService;
            this.navigationService = navigationService;
            this.centroService = centroService;
            this.databaseManager = databaseManager;
            this.usuarioService = usuarioService;
            this.syncService = syncService;
            this.displayAlertService = displayAlertService;
            this.networkConnection = DependencyService.Get<INetworkConnection>();

            Init();
            InitAsync();
        }

        private void Init()
        {
            Title = "Cambio de centro";
            Centro = new ValidatableObject<Centro>();
            ListaCentros = new ObservableRangeCollection<Centro>();
            CambiarCentroCommand = new Command(CambiarCentro);
            AddValidations();
        }

        private async void CambiarCentro(object obj)
        {
            if (!Validate()) return;

            var respuesta = await displayAlertService.Show("Cambio de centro", "Al cambiar de centro se perderán los cambios no finalizados. ¿Confirma que desea continuar?", "Aceptar", "Cancelar");
            if (respuesta)
            {
                try
                {
                    await StartSpinner("Cambiando centro...");
                    await syncService.SyncDataParcial();
                    setting.LastSync = ApplicationConstants.DefaultDateSync;
                    await databaseManager.ResetDB();

                    setting.CentroActivoId = Centro.Value.Id;
                    setting.CentroActivo = Centro.Value;
                    await settingsService.Update(setting);
                    await usuarioService.UpdateFuncionalidades(setting.UsuarioActivo, setting.CentroActivoId);

                    navigationService.PushFromRootAsync<HomeView>(setting.UsuarioActivo);

                }
                catch (BusinessException businessException)
                {
                    displayAlertService.Show("Cambio de centro", businessException.Mensaje, "Cerrar");
                }
                finally
                {
                    await StopSpinner();
                }
            }            
        }

        private async void InitAsync()
        {
            if (CheckConnection())
            {
                await StartSpinner("Obteniendo centros habilitados...");
                setting = await settingsService.GetWithChildren();
                await FillCentros();
                await StopSpinner();
            }
        }

        public bool Validate()
        {

            Centro.Validate();
            return Centro.IsValid && CheckConnection() && IsCentroChanged();
        }

        private bool CheckConnection()
        {
            networkConnection.CheckConnection();
            if (!networkConnection.IsConnected)
            {
                displayAlertService.Show("Sin Conexion", "Intente obtener una conexión estable de datos 3G, 4G o WIFI para poder realizar la validación", "Cerrar");
                return false;
            }
            return true;
        }

        private bool IsCentroChanged()
        {
            return setting.CentroActivoId != Centro.Value.Id;
        }

        private async Task FillCentros()
        {
            var centrosList = await centroService.GetAllByIdRed(setting.UsuarioActivo.IdRed);
            ListaCentros.AddRange(centrosList.Where(centro => centro.Id != setting.CentroActivoId).OrderBy(x=>x.Codigo).ToList());
        }

        private void AddValidations()
        {
            Centro.Validations.Clear();
            Centro.Validations.Add(new IsNotNullOrEmptyRule<Centro>
            {
                ValidationMessage = "El centro es obligatorio."
            });
        }
    }
}
