using Frontend.Business.ClasesDeValoracion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IClaseDeValoracionService
    {
        Task<IList<ClaseDeValoracion>> GetAll();
        Task<IList<string>> GetAllCodigoAutocomplete();
        Task<ClaseDeValoracion> GetByCodigo(string codigo);
        Task<ClaseDeValoracion> GetBy(int id);
        Task DeleteAll();
    }
}
