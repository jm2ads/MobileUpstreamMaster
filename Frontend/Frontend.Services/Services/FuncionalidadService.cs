using Frontend.Business.Funcionalidades;
using Frontend.Business.Funcionalidades.Searchers;
using Frontend.Business.IAzureRestServices;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class FuncionalidadService: IFuncionalidadService
    {
        private readonly IFuncionalidadAzureRestService funcionalidadAzureRestService;
        private readonly FuncionalidadSearcher funcionalidadSearcher;

        public FuncionalidadService(IFuncionalidadAzureRestService funcionalidadAzureRestService, FuncionalidadSearcher funcionalidadSearcher)
        {
            this.funcionalidadAzureRestService = funcionalidadAzureRestService;
            this.funcionalidadSearcher = funcionalidadSearcher;
        }

        public async Task<IList<Funcionalidad>> GetByIdRed(string idRed, int centroId)
        {
            var listaFuncionalidades = await funcionalidadAzureRestService.GetAll();
            var listaFuncionalidadesIds = await funcionalidadAzureRestService.GetByIdRed(idRed, centroId);
            return listaFuncionalidades.Where(funcionalidad => listaFuncionalidadesIds.Any(id => id.Id == funcionalidad.Id)).ToList();
        }
    }
}
