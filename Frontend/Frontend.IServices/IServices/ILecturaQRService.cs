using Frontend.Business.LecturaQRs;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ILecturaQRService
    {
        Task<LecturaQR> GetLecturaQR(string lectura);
    }
}
