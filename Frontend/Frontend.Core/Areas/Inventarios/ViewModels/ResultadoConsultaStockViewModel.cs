using Frontend.Business.Stocks;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Observables;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class ResultadoConsultaStockViewModel : BaseViewModel, IResultadoConsultaStockViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ISettingsService settingsService;

        public ICommand VolverCommand { get; set; }

        public ObservableRangeCollection<Stock> ListaStock { get; set; }

        private Stock stockSelected;
        public Stock StockSelected
        {
            get { return stockSelected; }
            set
            {
                SetProperty(ref stockSelected, value);
                GoToDetalleStock(stockSelected);
            }
        }

        private bool _hasInventario = false;
        public bool HasInventario
        {
            get { return _hasInventario; }
            set
            {
                SetProperty(ref _hasInventario, value);
            }
        }

        public ResultadoConsultaStockViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            this.navigationService = navigationService;
            this.settingsService = settingsService;
            Init();
        }

        private void Init()
        {
            GetCentro();
            ListaStock = new ObservableRangeCollection<Stock>();
            ListaStock.AddRange( navigationService.GetNavigationParams<ResultadoConsultaStockView>() as List<Stock>);
            HasInventario = ListaStock.Count > 0;

            VolverCommand = new Command(Volver);
        }

        private void Volver(object obj)
        {
            navigationService.PopAsync<ConsultaStockView>();
        }

        private async void GetCentro()
        {
            var settings = await settingsService.GetWithChildren();

            Title = "Centro " + settings.CentroActivo.Codigo;
        }

        private void GoToDetalleStock(Stock stock)
        {
            if (stock != null)
            {
                navigationService.PushAsync<ResultadoConsultaStockView, DetalleStockView>(stock);
            }
        }
    }
}
