using Frontend.Business.Centros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ICentroService
    {
        Task<IList<Centro>> GetAll();
        Task<IList<Centro>> GetAllByIdRed(string idRed);
        Task ReplaceAll(IList<Centro> centros);
    }
}
