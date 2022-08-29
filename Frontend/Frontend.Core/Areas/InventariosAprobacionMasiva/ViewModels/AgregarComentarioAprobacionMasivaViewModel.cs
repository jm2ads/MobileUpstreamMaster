using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Commons.Commons.Errors;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.InventariosAprobacionMasiva.IViewModels;
using Frontend.Core.Areas.InventariosAprobacionMasiva.Models;
using Frontend.Core.Commons.Alerts;
using Frontend.Core.Commons.Navigation;
using Frontend.Core.Commons.Validations;
using Frontend.Core.Resources;
using Frontend.Core.ViewModels;
using Frontend.Core.Views;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Frontend.Core.Areas.InventariosAprobacionMasiva.ViewModels
{
    public class AgregarComentarioAprobacionMasivaViewModel : BaseViewModel, IAgregarComentarioAprobacionMasivaViewModel
    {
        public ICommand AgregarCommand { get; set; }

        private IList<Inventario> _inventarios;
        public IList<Inventario> inventarios
        {
            get { return _inventarios; }
            set
            {
                SetProperty(ref _inventarios, value);
            }
        }

        private AgregarComentarioModel _agregarComentarioModel;
        public AgregarComentarioModel AgregarComentarioModel
        {
            get { return _agregarComentarioModel; }
            set
            {
                SetProperty(ref _agregarComentarioModel, value);
            }
        }

        private ValidatableObject<string> comentario;
        public ValidatableObject<string> Comentario
        {
            get { return comentario; }
            set { SetProperty(ref comentario, value); }
        }

        private string comentarioHistorico;
        public string ComentarioHistorico
        {
            get { return comentarioHistorico; }
            set { SetProperty(ref comentarioHistorico, value); }
        }

        private bool esGenerico;
        public bool EsGenerico
        {
            get { return esGenerico; }
            set { SetProperty(ref esGenerico, value); }
        }

        private bool comentarioDisponible;
        public bool ComentarioDisponible
        {
            get { return comentarioDisponible; }
            set { SetProperty(ref comentarioDisponible, value); }
        }

        private readonly IDisplayAlertService displayAlertService;
        private readonly INavigationService navigationService;
        private readonly IInventarioService inventarioService;
        private readonly IInventarioLocalService inventarioLocalService;

        public AgregarComentarioAprobacionMasivaViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService,
            IInventarioService inventarioService, IInventarioLocalService inventarioLocalService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;
            this.inventarioLocalService = inventarioLocalService;

            Init();
        }

        private void Init()
        {
            AgregarCommand = new Command(async () => await Agregar());
            Comentario = new ValidatableObject<string>();
            AgregarComentarioModel = navigationService.GetNavigationParams<AgregarComentarioAprobacionMasivaView>() as AgregarComentarioModel;
            inventarios = GetSelected(AgregarComentarioModel.EsAprobacion);
            Title = "Agregar comentario genérico";
            AddValidations();
        }

        private async Task Agregar()
        {
            if (!Validate())
            {
                Toast.ShowMessage("El comentario contiene errores.");
                return;
            }
            var answer = await displayAlertService.Show("Agregar comentario", "¿Desea agregar el comentario al/los inventario/s?", "Aceptar", "Cancelar");
            if (answer)
            {
                if (AgregarComentarioModel.EsAprobacion)
                {
                    await Aprobar();
                }
                else
                {
                    await Rechazar();
                }
                Toast.ShowMessage("Agregó el comentario correctamente. Las posiciones han sido rechazadas y/o aprobadas.");
                navigationService.PushFromAsync<HomeView, ListadoDeMaterialesAprobacionView>();
            }
        }

        private async Task Rechazar()
        {
            foreach (var inventario in inventarios)
            {
                if (AllSelected(inventario))
                {
                    try
                    {
                        await inventarioService.SetComentario(inventario, Comentario.Value);
                        await inventarioService.SetToRechazado(inventario);
                    }
                    catch (BusinessException be)
                    {
                        Toast.ShowMessage("Los inventarios seleccionados no se pueden rechazar.");
                        return;
                    }
                }
                else
                {
                    var listaAprobados = new List<DetalleInventario>(AgregarComentarioModel.ListaDetalles.Where(x => !x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(z => z.DetalleInventario));
                    var listaDesaprobados = new List<DetalleInventario>(AgregarComentarioModel.ListaDetalles.Where(x => x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id).Select(z => z.DetalleInventario));
                    await RechazoParcial(inventario, listaAprobados, listaDesaprobados);
                }
            }
        }

        private async Task Aprobar()
        {
            foreach (var inventario in inventarios)
            {
                var listaAprobados = new List<DetalleInventario>(AgregarComentarioModel.ListaDetalles.Where(x => x.IsSelected && x.DetalleInventario.Inventario.Equals(inventario)).Select(z => z.DetalleInventario));
                var listaDesaprobados = new List<DetalleInventario>(AgregarComentarioModel.ListaDetalles.Where(x => !x.IsSelected && x.DetalleInventario.Inventario.Equals(inventario)).Select(z => z.DetalleInventario));
                await RechazoParcial(inventario, listaAprobados, listaDesaprobados);
            }
        }

        private bool AllSelected(Inventario inventario)
        {
            return AgregarComentarioModel.ListaDetalles.Where(x => x.IsSelected && x.DetalleInventario.InventarioId == inventario.Id)
                .Count() == inventario.DetallesInventario.Count();
        }

        private async Task RechazoParcial(Inventario inventario, List<DetalleInventario> listaAprobados, List<DetalleInventario> listaDesaprobados)
        {
            inventario.DetallesInventario = listaDesaprobados;
            try
            {
                await inventarioLocalService.SetToRechazadoParcial(inventario, Comentario.Value);
            }
            catch (BusinessException be)
            {
                Toast.ShowMessage("Los inventarios seleccionados no se pueden rechazar.");
                return;
            }

            inventario.DetallesInventario = listaAprobados;
            try
            {
                await inventarioService.SetToAprobadoParcial(inventario);
            }
            catch (BusinessException be)
            {
                Toast.ShowMessage("Los inventarios seleccionados no se pueden aprobar.");
                return;
            }
        }

        private IList<Inventario> GetSelected(bool EsAprobacion)
        {
            return EsAprobacion ? AgregarComentarioModel.ListaDetalles.Where(x => !x.IsSelected).Select(x => x.DetalleInventario.Inventario).Distinct().ToList()
                                : AgregarComentarioModel.ListaDetalles.Where(x => x.IsSelected).Select(x => x.DetalleInventario.Inventario).Distinct().ToList();
        }

        #region Validations
        private void AddValidations()
        {
            AddComentarioValidations();
        }

        private void AddComentarioValidations()
        {
            Comentario.Validations.Clear();
            Comentario.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = MessageText.FieldRequired
            });
        }

        public bool ValidateComentario()
        {
            return Comentario.Validate();
        }

        private bool Validate()
        {
            bool isValidComentario = ValidateComentario();


            return isValidComentario;
        }
        #endregion
    }
}
