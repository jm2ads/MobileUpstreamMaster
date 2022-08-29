using System;
using Frontend.Business.Stocks;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class DetalleStockViewModel: BaseViewModel, IDetalleStockViewModel
    {
        private readonly INavigationService navigationService;
        public Stock stock { get; set; }

        public DetalleStockViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Init();
        }

        private void Init()
        {
            Title = "Detalle de material";
            stock = navigationService.GetNavigationParams<DetalleStockView>() as Stock;
        }
    }
}
