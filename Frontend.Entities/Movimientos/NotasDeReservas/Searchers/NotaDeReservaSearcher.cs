using Frontend.Business.Commons;
using Frontend.Business.Movimientos.NotasDeReservas.Core.NotasDeReservas;
using Frontend.Business.Movimientos.Reservas;
using System.Threading.Tasks;

namespace Frontend.Business.Movimientos.NotasDeReservas.Searchers
{
    public class NotaDeReservaSearcher
    {
        private readonly IRepository<NotaDeReserva> repository;
        private readonly NotaDeReservaGenerator notaDeReservaGenerator;

        public NotaDeReservaSearcher(IRepository<NotaDeReserva> repository, NotaDeReservaGenerator notaDeReservaGenerator)
        {
            this.repository = repository;
            this.notaDeReservaGenerator = notaDeReservaGenerator;
        }

        public async Task<NotaDeReserva> GetOrGenerate(Reserva reserva)
        {
            var notaDeReserva = await repository.FindFirstWithChildren(x => x.ReservaId == reserva.Id);
            return notaDeReserva ?? await notaDeReservaGenerator.Generate(reserva);
        }
    }
}
