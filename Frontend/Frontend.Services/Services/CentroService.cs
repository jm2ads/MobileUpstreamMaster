using Frontend.Business.Centros;
using Frontend.Business.Centros.Core;
using Frontend.Business.Centros.Searchers;
using Frontend.Business.IAzureRestServices;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class CentroService : ICentroService
    {
        private readonly CentroSearcher centroSearcher;
        private readonly ICentroAzureRestService centroAzureRestService;
        private readonly CentroDeleter centroDeleter;
        private readonly CentroGenerator centroGenerator;

        public CentroService(CentroSearcher centroSearcher, ICentroAzureRestService centroAzureRestService, CentroDeleter centroDeleter,
            CentroGenerator centroGenerator)
        {
            this.centroSearcher = centroSearcher;
            this.centroAzureRestService = centroAzureRestService;
            this.centroDeleter = centroDeleter;
            this.centroGenerator = centroGenerator;
        }

        public async Task<IList<Centro>> GetAll()
        {
            return await centroSearcher.GetAll();
        }

        public async Task<IList<Centro>> GetAllByIdRed(string idRed)
        {
            var centros = await centroAzureRestService.GetByIdRed(idRed);
            return centros;
        }

        public async Task ReplaceAll(IList<Centro> centros)
        {
            await centroDeleter.DeleteAll();
            await centroGenerator.Generate(centros);
        }
    }
}
