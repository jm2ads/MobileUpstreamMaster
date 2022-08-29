using Frontend.Azure.DTOs;
using Frontend.Business.Almacenes.Searchers;
using Frontend.Business.Centros;
using Frontend.Business.Centros.Searchers;
using Frontend.Business.ClasesDeValoracion.Searchers;
using Frontend.Business.DetallesStocksEspeciales.Searchers;
using Frontend.Business.Materiales.Searchers;
using Frontend.Business.Settings;
using Frontend.Business.Stocks;
using Frontend.Business.Stocks.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class StockMapper
    {
        private readonly StockFactory stockFactory;
        private readonly MaterialSearcher materialSearcher;
        private readonly AlmacenSearcher almacenSearcher;
        private readonly CentroSearcher centroSearcher;
        private readonly DetalleStockEspecialSearcher detalleStockEspecialSearcher;
        private readonly ClaseDeValoracionSearcher claseDeValoracionSearcher;

        public StockMapper(StockFactory stockFactory, MaterialSearcher materialSearcher, AlmacenSearcher almacenSearcher,
            CentroSearcher centroSearcher, DetalleStockEspecialSearcher detalleStockEspecialSearcher, ClaseDeValoracionSearcher claseDeValoracionSearcher)
        {
            this.stockFactory = stockFactory;
            this.materialSearcher = materialSearcher;
            this.almacenSearcher = almacenSearcher;
            this.centroSearcher = centroSearcher;
            this.detalleStockEspecialSearcher = detalleStockEspecialSearcher;
            this.claseDeValoracionSearcher = claseDeValoracionSearcher;
        }
        
        public async Task<Stock> MapFromDto(StockInputDto stockInputDto, int centroId)
        {
            var stock = stockFactory.Create();

            stock.Id = stockInputDto.Id;
            stock.CantidadAlmacen = stockInputDto.CantidadAlmacen;
            stock.CantidadCalidad = stockInputDto.CantidadCalidad;
            stock.CantidadBloqueado = stockInputDto.CantidadBloqueado;
            stock.Ubicacion = stockInputDto.Ubicacion; 
            stock.IdMaterial = stockInputDto.MaterialId;
            stock.IdCentro = centroId;
            if (stockInputDto.AlmacenId.HasValue)
            {
                stock.IdAlmacen = stockInputDto.AlmacenId.Value;
            }
            stock.IdClaseDeValoracion = stockInputDto.ClaseDeValoracionId;
            stock.IdDetalleStockEspecial = stockInputDto.DetalleStockEspecialId;

            return stock;
        }

        public async Task<IList<Stock>> MapFromDto(IList<StockInputDto> listStockInputDto, int centroId)
        {
            IList<Stock> listStock = new List<Stock>();

            foreach (var stockInputDto in listStockInputDto)
            {
                listStock.Add(await MapFromDto(stockInputDto, centroId));
            }

            return listStock;
        }

        public async Task<StockOutputDto> MapToDto(Setting setting)
        {
            var stockOutputDto = new StockOutputDto();

            stockOutputDto.CentroId = setting.CentroActivoId;
            stockOutputDto.delta = setting.LastSync.ToString("O");

            return stockOutputDto;
        }
    }
}
