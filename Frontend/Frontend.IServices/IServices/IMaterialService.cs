using Frontend.Business.Materiales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IMaterialService
    {
        Task<IList<Material>> GetAllMaterial();
        Task<IList<string>> GetAllCodigoMaterialAutocomplete();
        Task<IList<string>> GetAllDescripcionMaterialAutocomplete();
        
        Task<Material> GetByCodigo(string searchValue);
        Task DeleteAll();
        Task<Material> GetByDescripcion(string searchValue);
    }
}
