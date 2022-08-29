using Frontend.Business.Centros;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.Usuarios;
using Frontend.Business.Usuarios.Core;
using Frontend.Business.Usuarios.Searchers;
using Frontend.Business.Usuarios.Validators;
using Frontend.IServices.IServices;
using System;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioSearcher usuarioSearcher;
        private readonly UsuarioFactory usuarioFactory;
        private readonly UsuarioGenerator usuarioGenerator;
        private readonly UsuarioValidator usuarioValidator;
        private readonly UsuarioUpdater usuarioUpdater;
        private readonly IAplicacionUsuarioService aplicacionUsuarioService;
        private readonly ISettingsService settingsService;
        private readonly IUserAzureRestService userAzureRestService;
        private readonly IFuncionalidadService funcionalidadService;

        public UsuarioService(UsuarioSearcher usuarioSearcher, UsuarioFactory usuarioFactory, UsuarioGenerator usuarioGenerator,
            UsuarioValidator usuarioValidator, UsuarioUpdater usuarioUpdater, IAplicacionUsuarioService aplicacionUsuarioService, ISettingsService settingsService,
            IUserAzureRestService userAzureRestService, IFuncionalidadService funcionalidadService)
        {
            this.usuarioSearcher = usuarioSearcher;
            this.usuarioFactory = usuarioFactory;
            this.usuarioGenerator = usuarioGenerator;
            this.usuarioValidator = usuarioValidator;
            this.usuarioUpdater = usuarioUpdater;
            this.aplicacionUsuarioService = aplicacionUsuarioService;
            this.settingsService = settingsService;
            this.userAzureRestService = userAzureRestService;
            this.funcionalidadService = funcionalidadService;
        }

        public async Task<Usuario> GetByIdRed(string idRed)
        {
            return await usuarioSearcher.GetByIdRed(idRed);
        }

        public async Task<Usuario> GetById(int id)
        {
            return await usuarioSearcher.GetById(id);
        }

        public Usuario Create()
        {
            return usuarioFactory.Create();
        }

        public async Task<Usuario> Generate(Usuario usuario)
        {
            return await usuarioGenerator.Generate(usuario);
        }

        public async Task Update(Usuario usuario)
        {
            await usuarioUpdater.Update(usuario);
        }

        public async Task<bool> ValidatePin(Usuario usuario, string pin)
        {
            return await usuarioValidator.ValidatePin(usuario, pin);
        }

        public async Task DoLogin(string idRed, string password)
        {
            var usuario = await usuarioSearcher.GetByIdRed(idRed);
            if (usuario == null)
            {
                usuario = usuarioFactory.Create(idRed, password);
            }
            usuario.Contraseña = password;

            var userHasToken = await HasToken(idRed);
            if (userHasToken)
            {
                MergeUsuario(usuario, await userAzureRestService.Login(usuario));
                await usuarioUpdater.Update(usuario);
            }
            else
            {
                MergeUsuario(usuario, await userAzureRestService.Register(usuario));
                await this.usuarioGenerator.Generate(usuario);
            }

            var settings = await settingsService.GetWithChildren();
            settings.UsuarioActivo = usuario;
            settings.UsuarioActivoId = usuario.Id;
            await settingsService.Update(settings);
        }

        private void MergeUsuario(Usuario usuario, Usuario usuarioValidado)
        {
            usuario.Nombre = usuarioValidado.Nombre;
            usuario.Token = usuarioValidado.Token;
        }

        public async Task<bool> HasToken(string idRed)
        {
            var usuario = await usuarioSearcher.GetByIdRed(idRed);
            return !string.IsNullOrEmpty(usuario?.Token);
        }

        public async Task<bool> ValidateToken(Usuario usuario)
        {
            var userSettings = await settingsService.GetWithChildren();
            if (string.IsNullOrEmpty(usuario.Token))
            {
                return false; //throw new AuthenticationException(BusinessErrorCode.NoUserSettings, "No existe Token");
            }
            try
            {
                var userValidated = await userAzureRestService.Validate(usuario);
                return true;
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
        
        public async Task<bool> ExistUser(string userlogin)
        {
            var setting = await settingsService.GetWithChildren();
            return setting.UsuarioActivo.IdRed == userlogin;
        }

        public async Task DeleteCurrentUser()
        {
            var setting = await settingsService.GetWithChildren();

            setting.UsuarioActivo = null;

            await settingsService.Update(setting);
        }

        public void DropData()
        {
            usuarioGenerator.DropData();
        }

        public async Task UpdateFuncionalidades(Usuario usuario, int centroId)
        {
            var funcionalidades = await funcionalidadService.GetByIdRed(usuario.IdRed, centroId);
            usuario.Funcionalidades.Clear();
            usuario.Funcionalidades.AddRange(funcionalidades);
            await usuarioUpdater.Update(usuario);
        }

    }
}
