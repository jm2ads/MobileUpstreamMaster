using Frontend.Business.Almacenes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IAlmacenService
    {
        Task<IList<Almacen>> GetAll();
        Task<IList<Almacen>> GetByIdCentro(int IdCentro);
        Task<Almacen> GetByCodigo(string codigo);
        Task<Almacen> GetByNombre(string nombre);
        Task<IList<string>> GetAllNombreAutocomplete();
        Task<IList<string>> GetAllCodigoAutocomplete();
    }
}
