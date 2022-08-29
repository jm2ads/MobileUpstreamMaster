using Frontend.Azure.DTOs;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.StocksEspeciales.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class StockEspecialMapper
    {
        private readonly StockEspecialFactory stockEspecialFactory;

        public StockEspecialMapper(StockEspecialFactory stockEspecialFactory)
        {
            this.stockEspecialFactory = stockEspecialFactory;
        }

        public async Task<StockEspecial> MapFromDto(StockEspecialInputDto stockEspecialInputDto)
        {
            var stockEspecial = stockEspecialFactory.Create();

            stockEspecial.Id = stockEspecialInputDto.Id;
            stockEspecial.Codigo = stockEspecialInputDto.Codigo;
            stockEspecial.NombreCampo = stockEspecialInputDto.NombreCampo;
            stockEspecial.Descripcion = stockEspecialInputDto.Descripcion;

            return stockEspecial;
        }

        public async Task<IList<StockEspecial>> MapFromDto(List<StockEspecialInputDto> listaStockEspecialInputDto)
        {
            IList<StockEspecial> listStock = new List<StockEspecial>();

            foreach (var stockInputDto in listaStockEspecialInputDto)
            {
                listStock.Add(await MapFromDto(stockInputDto));
            }

            return listStock;
        }
    }
}
