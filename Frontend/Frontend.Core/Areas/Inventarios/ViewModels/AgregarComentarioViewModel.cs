using Frontend.Business.Inventarios;
using Frontend.Core.Areas.Home.Views;
using Frontend.Core.Areas.Inventarios.IViewModels;
using Frontend.Core.Areas.Inventarios.Models;
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

namespace Frontend.Core.Areas.Inventarios.ViewModels
{
    public class AgregarComentarioViewModel : BaseViewModel, IAgregarComentarioViewModel
    {
        public ICommand AgregarCommand { get; set; }

        private AgregarComentarioModel _agregarComentarioModel;
        public AgregarComentarioModel agregarComentarioModel
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

        public AgregarComentarioViewModel(IDisplayAlertService displayAlertService, INavigationService navigationService,
            IInventarioService inventarioService)
        {
            this.displayAlertService = displayAlertService;
            this.navigationService = navigationService;
            this.inventarioService = inventarioService;

            Init();
        }

        private void Init()
        {
            AgregarCommand = new Command(async () => await Agregar());
            Comentario = new ValidatableObject<string>();
            agregarComentarioModel = navigationService.GetNavigationParams<AgregarComentarioView>() as AgregarComentarioModel;
            Title = agregarComentarioModel.EsGenerico ? "Agregar comentario genérico" : "Agregar comentario";
            ComentarioHistorico = GetComentarioHistorico();
            ComentarioDisponible = GetComentarioDisponible();

            AddValidations();
        }

        private string GetComentarioHistorico()
        {
            if (!agregarComentarioModel.EsGenerico)
            {
                return String.IsNullOrEmpty(agregarComentarioModel.Inventarios.First().ComentarioRechazo) ? "" : agregarComentarioModel.Inventarios.First().ComentarioRechazo;
            }
            else
            {
                return "";
            }
        }
        private bool GetComentarioDisponible()
        {
            return agregarComentarioModel.EsGenerico || String.IsNullOrEmpty(agregarComentarioModel.Inventarios.First().ComentarioRechazo);
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
                await inventarioService.SetComentario(agregarComentarioModel.Inventarios, Comentario.Value);
                Toast.ShowMessage("Agregó el comentario correctamente.");
                if (!agregarComentarioModel.Retornar)
                {
                    await inventarioService.SetToRechazado(agregarComentarioModel.Inventarios);
                    Toast.ShowMessage("Inventario/s rechazado/s");
                    navigationService.PushFromAsync<HomeView, AprobacionInventarioView>();
                }
                else
                {
                    navigationService.PopAsync<AprobacionInventarioView>();
                }

            }
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
