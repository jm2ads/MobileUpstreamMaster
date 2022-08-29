using Frontend.Business.Movimientos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface IMovimientoService
    {
        Task<Movimiento> GetBy(string nombre);
        Task<IList<string>> GetListaParaPosicionesSinClaseDeMovimiento();
        Task<IList<string>> GetListaParaPosicionesAlMenosUnaClase103();
        Task<string> GetValorParaPosicionesConTodasClases103();
    }
}
