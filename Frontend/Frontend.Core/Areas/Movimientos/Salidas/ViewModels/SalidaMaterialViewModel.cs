using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.Core.Areas.Movimientos.Salidas.IViewModels;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Movimientos.Salidas.ViewModels
{
    public class SalidaMaterialViewModel : BaseViewModel, ISalidaMaterialViewModel
    {
        public ICommand GoToCrearDetalleReservaCommand { get; set; }

        private System.Collections.IList listMaterialesFull;
        public System.Collections.IList ListMaterialesFull
        {
            get { return listMaterialesFull; }
            set
            {
                SetProperty(ref listMaterialesFull, value);
            }
        }

        private System.Collections.IList listDescripcionMateriales;
        public System.Collections.IList ListDescripcionMateriales
        {
            get { return listDescripcionMateriales; }
            set
            {
                SetProperty(ref listDescripcionMateriales, value);
            }
        }


        private System.Collections.IList listaFiltros;
        public System.Collections.IList ListaFiltros
        {
            get { return listaFiltros; }
            set
            {
                SetProperty(ref listaFiltros, value);
            }
        }

        private string searchValue;
        public string SearchValue
        {
            get
            {
                return searchValue;
            }
            set
            {
                SetProperty(ref searchValue, value);
            }
        }

        private string filtro;
        public string Filtro
        {
            get { return filtro; }
            set
            {
                SetProperty(ref filtro, value);
                SearchByCodigo = value == "Código";
                SearchValue = string.Empty;
            }
        }


        private bool searchByCodigo;
        private readonly INavigationService navigationService;
        private readonly IDisplayAlertService displayAlertService;
        private readonly IReservaService reservaService;
        private readonly ILecturaQRService lecturaQRService;

        public bool SearchByCodigo
        {
            get { return searchByCodigo; }
            set
            {
                SetProperty(ref searchByCodigo, value);
                OnPropertyChanged("SearchValue");
            }
        }

        public IList<Tuple<string, int>> CodigoMaterialList { get; set; }
        public IList<Tuple<string, int>> DescripcionMaterialList { get; set; }

        public SalidaMaterialViewModel(INavigationService navigationService, IDisplayAlertService displayAlertService,
            IReservaService reservaService, ILecturaQRService lecturaQRService)
        {
            this.navigationService = navigationService;
            this.displayAlertService = displayAlertService;
            this.reservaService = reservaService;
            this.lecturaQRService = lecturaQRService;
            Init();
        }

        private async void Init()
        {
            Title = "Material";

            GoToCrearDetalleReservaCommand = new Command(async () => await SearchMaterial());
            CodigoMaterialList = new List<Tuple<string, int>>();
            DescripcionMaterialList = new List<Tuple<string, int>>();

            ListaFiltros = new List<string>();
            ListaFiltros.Add("Código");
            ListaFiltros.Add("Texto corto");

            Filtro = "Código";

            await InitAsync();
        }

        private async Task InitAsync()
        {
            await StartSpinner();
            var listMateriales = await reservaService.GetAllMaterialBy(TipoReserva.Salida, EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);

            foreach (var item in listMateriales)
            {
                CodigoMaterialList.Add(new Tuple<string, int>(item.Codigo.TrimStart('0'), item.Id));
                DescripcionMaterialList.Add(new Tuple<string, int>(item.Descripcion, item.Id));
            }
            ListMaterialesFull = CodigoMaterialList.Select(x => x.Item1).ToList();
            ListDescripcionMateriales = DescripcionMaterialList.Select(x => x.Item1).ToList();

            await StopSpinner();
        }

        private async Task SearchMaterial()
        {
            try
            {
                await StartSpinner();
                var reservas = await reservaService.GetAllBy(await lecturaQRService.GetLecturaQR(SearchValue), TipoReserva.Salida);

                if (reservas.Count > 1)
                {
                    var answer = await displayAlertService.DisplayActionSheet("Salidas", "Cancelar", "", reservas.Select(x => x.Numero).ToArray());
                    if (!String.IsNullOrWhiteSpace(answer) && answer != "Cancelar")
                    {
                        GoToCabecera(await reservaService.GetWithChildren(reservas.First(x => x.Numero == answer).Id));
                    }
                }
                else if (reservas.Count == 1)
                {
                    GoToCabecera(await reservaService.GetWithChildren(reservas.First().Id));
                }
                else
                {
                    GoToCabecera(await reservaService.GetWithChildren(reservas.First().Id));
                }
            }
            catch (Exception e)
            {
                Toast.ShowMessage("Código o texto corto de material no válido. Por favor, vuelva a ingresarlo.");
            }
            finally
            {
                await StopSpinner();
            }
        }

        private void GoToCabecera(Reserva reserva)
        {
            navigationService.PushAsync<SalidaMaterialView, CabeceraSalidaView>(reserva);
        }
    }
}
