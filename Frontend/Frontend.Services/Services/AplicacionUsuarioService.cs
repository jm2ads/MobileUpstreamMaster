using Frontend.Business.Commons;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Users.Devices;
using Frontend.IServices.IServices;
using Frontend.WebApi.DTOs.AplicacionUsuarios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class AplicacionUsuarioService : IAplicacionUsuarioService
    {
        private readonly IRepository<AplicacionUsuario> repository;
        private readonly IAplicacionUsuarioAzureRestService restService;
        private readonly SettingSearcher settingSearcher;
        private readonly AplicacionUsuarioFactory factory;

        public AplicacionUsuarioService(IRepository<AplicacionUsuario> repository,
                                        IAplicacionUsuarioAzureRestService restService,
                                        SettingSearcher settingSearcher,
                                        AplicacionUsuarioFactory factory)
        {
            this.repository = repository;
            this.restService = restService;
            this.settingSearcher = settingSearcher;
            this.factory = factory;
        }

        public async Task<IList<AplicacionUsuario>> GetAll()
        {
            return await this.repository.GetAllWithChildren();
        }

        // Este servicio se ejecuta 1 unica vez, cuando el usuario inicia la aplicacion posterior
        // a validacion de login exitosa. Se actualizan las credenciales a la fecha
        public async Task GetCredentialsApp(string userId)
        {
            var apps = await this.GetAll();

            if (apps.Count() > 0)
            {
                await this.repository.DeleteAll();
            }
            var setting = await settingSearcher.GetWithChildren();
            if (setting == null)
            {
                return;
            }

            //var userId = setting.UsuarioActivo.IdRed;
            var credentials = await restService.GetCredentialsApp(userId);

            var entities = credentials.Select(x => this.factory.Create(x)).ToList();
            entities.ForEach(x => this.repository.Save(x));
        }
    }
}
