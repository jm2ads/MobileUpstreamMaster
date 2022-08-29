using Frontend.Business.Funcionalidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IFuncionalidadService
    {
        Task<IList<Funcionalidad>> GetByIdRed(string idRed, int centroId);
    }
}
