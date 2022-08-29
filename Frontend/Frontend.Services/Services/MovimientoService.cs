using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.Searchers;
using Frontend.IServices.IServices;

namespace Frontend.Services.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly MovimientoSearcher movimientoSearcher;

        public MovimientoService(MovimientoSearcher movimientoSearcher)
        {
            this.movimientoSearcher = movimientoSearcher;
        }

        public async Task<Movimiento> GetBy(string nombre)
        {
            return await movimientoSearcher.GetWithChildrenBy(nombre);
        }

        public async Task<IList<string>> GetListaParaPosicionesSinClaseDeMovimiento()
        {
            var movimiento = await movimientoSearcher.GetWithChildrenBy(Movimiento.Ingreso);

            var listaClaseDeMovimiento = new List<string>();
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_101]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_Z01]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103]);

            return movimiento.ClasesDeMovimientos.Select(x => x.Codigo).Intersect(listaClaseDeMovimiento).ToList();
        }
        public async Task<IList<string>> GetListaParaPosicionesAlMenosUnaClase103()
        {
            var movimiento = await movimientoSearcher.GetWithChildrenBy(Movimiento.Ingreso);

            var listaClaseDeMovimiento = new List<string>();
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_101]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_Z01]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_103]);
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_105]);

            return movimiento.ClasesDeMovimientos.Select(x => x.Codigo).Intersect(listaClaseDeMovimiento).ToList();
        }

        public async Task<string> GetValorParaPosicionesConTodasClases103()
        {
            var movimiento = await movimientoSearcher.GetWithChildrenBy(Movimiento.Ingreso);

            var listaClaseDeMovimiento = new List<string>();
            listaClaseDeMovimiento.Add(ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_105]);

            return movimiento.ClasesDeMovimientos.FirstOrDefault(x => x.Codigo == ClaseDeMovimientoPedido.ClaseDeMovimiento[ClaseDeMovimientoPedido.CLASE_105]).Codigo;
        }
    }
}
