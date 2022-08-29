using Frontend.Business.Materiales;
using Frontend.Business.Materiales.Core;
using Frontend.Business.Materiales.Searchers;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly MaterialSearcher materialSearcher;
        private readonly MaterialDeleter materialDeleter;

        public MaterialService(MaterialSearcher materialSearcher, MaterialDeleter materialDeleter)
        {
            this.materialSearcher = materialSearcher;
            this.materialDeleter = materialDeleter;
        }

        public async Task DeleteAll()
        {
            await materialDeleter.DeleteAll();
        }

        public async Task<IList<Material>> GetAllMaterial()
        {
            return await materialSearcher.GetAllWithChildren();
        }


        public async Task<IList<string>> GetAllCodigoMaterialAutocomplete()
        {
            return await materialSearcher.GetAllCodigoAutocomplete();
        }

        public async Task<Material> GetByCodigo(string searchValue)
        {
            return await materialSearcher.GetByCodigo(searchValue);
        }

        public async Task<IList<string>> GetAllDescripcionMaterialAutocomplete()
        {
            return await materialSearcher.GetAllDescripcionAutocomplete(); 
        }

        public async Task<Material> GetByDescripcion(string searchValue)
        {
            return await materialSearcher.GetByDescripcion(searchValue);
        }
    }
}
