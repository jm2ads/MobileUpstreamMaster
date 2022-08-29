using Frontend.Business.Commons;
using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.Movimientos.Reservas;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.NotasDeReservas.Core.NotasDeReservas
{
    public class NotaDeReservaGenerator
    {
        private readonly IRepository<NotaDeReserva> repository;
        private readonly NotaDeReservaFactory notaDeReservaFactory;
        private readonly StockEspecialSearcher stockEspecialSearcher;

        public NotaDeReservaGenerator(IRepository<NotaDeReserva> repository, NotaDeReservaFactory notaDeReservaFactory, StockEspecialSearcher stockEspecialSearcher)
        {
            this.repository = repository;
            this.notaDeReservaFactory = notaDeReservaFactory;
            this.stockEspecialSearcher = stockEspecialSearcher;
        }

        public async Task<NotaDeReserva> Generate(Reserva reserva)
        {
            var stockEspecial = await stockEspecialSearcher.GetByCodigo("S");
            var notaDeReserva = notaDeReservaFactory.Create(reserva, stockEspecial);
            return await repository.SaveWithChildren(notaDeReserva);
        }
    }
}
