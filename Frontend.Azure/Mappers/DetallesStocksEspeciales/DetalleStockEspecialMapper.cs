using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.DetallesStocksEspeciales;
using Frontend.Business.DetallesStocksEspeciales;
using Frontend.Business.DetallesStocksEspeciales.Core;
using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class DetalleStockEspecialMapper
    {
        private readonly DetalleStockEspecialFactory detalleStockEspecialFactory;
        private readonly StockEspecialSearcher stockEspecialSearcher;

        public DetalleStockEspecialMapper(DetalleStockEspecialFactory detalleStockEspecialFactory, StockEspecialSearcher stockEspecialSearcher)
        {
            this.detalleStockEspecialFactory = detalleStockEspecialFactory;
            this.stockEspecialSearcher = stockEspecialSearcher;
        }

        public async Task<DetalleStockEspecial> MapFromDto(DetalleStockEspecialInputDto detalleStockEspecialInputDto)
        {
            var detalleStockEspecial = detalleStockEspecialFactory.Create();

            detalleStockEspecial.Id = detalleStockEspecialInputDto.Id;
            detalleStockEspecial.Detalle = detalleStockEspecialInputDto.Detalle;
            detalleStockEspecial.IdStockEspecial = detalleStockEspecialInputDto.StockEspecialId;

            return detalleStockEspecial;
        }

        public async Task<IList<DetalleStockEspecial>> MapFromDto(IList<DetalleStockEspecialInputDto> detallesInputDto)
        {
            IList<DetalleStockEspecial> listDetalles = new List<DetalleStockEspecial>();

            foreach (var detalleInputDto in detallesInputDto)
            {
                listDetalles.Add(await MapFromDto(detalleInputDto));
            }

            return listDetalles;
        }

        public async Task<DetalleStockEspecialOutputDto> MapToDto(Setting setting)
        {
            var detalleStockEspecialOutputDto = new DetalleStockEspecialOutputDto();

            detalleStockEspecialOutputDto.delta = setting.LastSync.ToString("O");

            return detalleStockEspecialOutputDto;
        }
    }
}
