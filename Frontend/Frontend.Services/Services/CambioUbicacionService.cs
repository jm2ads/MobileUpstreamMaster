using Frontend.Business.CambiosUbicacion;
using Frontend.Business.CambiosUbicacion.Core;
using Frontend.IServices.IServices;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class CambioUbicacionService : ICambioUbicacionService
    {
        private readonly ISettingsService settingsService;
        private readonly CambioUbicacionFactory cambioUbicacionFactory;
        private readonly CambioUbicacionGenerator cambioUbicacionGenerator;
        private readonly CambioUbicacionUpdater cambioUbicacionUpdater;


        public CambioUbicacionService(ISettingsService settingsService, CambioUbicacionFactory cambioUbicacionFactory,
            CambioUbicacionGenerator cambioUbicacionGenerator, CambioUbicacionUpdater cambioUbicacionUpdater)
        {
            this.settingsService = settingsService;
            this.cambioUbicacionFactory = cambioUbicacionFactory;
            this.cambioUbicacionGenerator = cambioUbicacionGenerator;
            this.cambioUbicacionUpdater = cambioUbicacionUpdater;
        }

        public async Task<CambioUbicacion> Create()
        {
            var setting = await settingsService.GetWithChildren();
            return cambioUbicacionFactory.Create(setting.CentroActivo, setting.UsuarioActivo.IdRed);
        }

        public async Task<CambioUbicacion> Insert(CambioUbicacion cambioUbicacion)
        {
            return await cambioUbicacionGenerator.Generate(cambioUbicacion);
        }

        public async Task Update(CambioUbicacion cambioUbicacion)
        {
            await cambioUbicacionUpdater.Update(cambioUbicacion);
        }
    }
}
