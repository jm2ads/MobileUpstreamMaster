using Frontend.Business.CambiosUbicacion;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ICambioUbicacionService
    {
        Task<CambioUbicacion> Create();
        Task<CambioUbicacion> Insert(CambioUbicacion cambioUbicacion);
        Task Update(CambioUbicacion cambioUbicacion);
    }
}
