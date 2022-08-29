using Frontend.Business.DetallesInventario.TiposStock;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using Frontend.Business.Stocks.Searchers;
using Frontend.Commons.Enums;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class DetalleInventarioMasivoFactory
    {
        private readonly StockSearcher stockSearcher;

        #region Public Methods

        public DetalleInventarioMasivoFactory(StockSearcher stockSearcher)
        {
            this.stockSearcher = stockSearcher;
        }

        public async Task<DetalleInventarioMasivo> Duplicar(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            var detalleDuplicado = new DetalleInventarioMasivo();
            detalleDuplicado.IdMaterial = detalleInventarioMasivo.IdMaterial;
            detalleDuplicado.Material = detalleInventarioMasivo.Material;
            detalleDuplicado.Ubicacion = detalleInventarioMasivo.Ubicacion;
            detalleDuplicado.InventarioMasivo = detalleInventarioMasivo.InventarioMasivo;
            detalleDuplicado.InventarioId = detalleInventarioMasivo.InventarioId;
            detalleDuplicado.Posicion = CalcularPosicion(detalleInventarioMasivo.InventarioMasivo);
            detalleDuplicado.TipoStockId = TipoStockEnum.LibreUtilizacion.GetHashCode();
            detalleDuplicado.TipoLote = await CalcularTipoLote(detalleInventarioMasivo.IdMaterial);

            return detalleDuplicado;
        }

        public async Task<DetalleInventarioMasivo> Create(InventarioMasivo inventarioMasivo, Material material)
        {
            return new DetalleInventarioMasivo()
            {
                Material = material,
                IdMaterial = material.Id,
                InventarioId = inventarioMasivo.Id,
                InventarioMasivo = inventarioMasivo,
                TipoStockId = TipoStockEnum.LibreUtilizacion.GetHashCode(),
                Posicion = CalcularPosicion(inventarioMasivo),
                TipoLote = await CalcularTipoLote(material.Id)
            };
        }
        public DetalleInventarioMasivo Copiar(DetalleInventarioMasivo detalleInventario)
        {
            return new DetalleInventarioMasivo()
            {
                Material = detalleInventario.Material,
                IdMaterial = detalleInventario.IdMaterial,
                TipoStockId = detalleInventario.TipoStockId,
                Posicion = detalleInventario.Posicion,
                TipoLote = detalleInventario.TipoLote,
                Stock = detalleInventario.Stock,
                IdStock = detalleInventario.IdStock,
                Cantidad = detalleInventario.Cantidad,
                Ubicacion = detalleInventario.Ubicacion,
                HayConteoErroneo = detalleInventario.Cantidad != GetCantidadStock(detalleInventario.Stock, detalleInventario.TipoStockId),
                Unidad = detalleInventario.Unidad
            };
        }

        public async Task<IList<DetalleInventarioMasivo>> Create(IList<DetalleInventarioMasivo> detalleInventarioMasivos, IList<InventarioMasivoOrden> orden)
        {
            var listDetalleInventarioMasivoDistribuido = new List<DetalleInventarioMasivo>();
            foreach (var detalleInventarioMasivo in detalleInventarioMasivos)
            {
                //Separar detalles a distribuir de los que ya lo estan
                if (detalleInventarioMasivo.Lote == null && detalleInventarioMasivo.Almacen == null)
                {
                    await Distribuir(orden, detalleInventarioMasivo, listDetalleInventarioMasivoDistribuido);
                }
                else
                {
                    detalleInventarioMasivo.InventarioId = 0;
                    detalleInventarioMasivo.InventarioMasivo = null;
                    var detalleDuplicado = Copiar(detalleInventarioMasivo);
                    listDetalleInventarioMasivoDistribuido.Add(detalleDuplicado);
                }
            }
            AsignarEstadoDeConteo(listDetalleInventarioMasivoDistribuido);
            return listDetalleInventarioMasivoDistribuido;
        }

        private void AsignarEstadoDeConteo(List<DetalleInventarioMasivo> detallesDistribuidos)
        {
            var materiales = detallesDistribuidos.GroupBy(x => x.Stock.Material).ToList();
            foreach (var material in materiales)
            {
                if (detallesDistribuidos.Where(x => x.Stock.IdMaterial == material.Key.Id).All(x => x.HayConteoErroneo))
                {
                    MarcarConteo(detallesDistribuidos.Where(x => x.Stock.IdMaterial == material.Key.Id), EstadoConteoEnum.Erroneo);
                }
                else if (detallesDistribuidos.Where(x => x.Stock.IdMaterial == material.Key.Id).All(x => !x.HayConteoErroneo))
                {
                    MarcarConteo(detallesDistribuidos.Where(x => x.Stock.IdMaterial == material.Key.Id), EstadoConteoEnum.Completo);
                }
                else
                {
                    MarcarConteo(detallesDistribuidos.Where(x => x.Stock.IdMaterial == material.Key.Id), EstadoConteoEnum.Parcial);
                }
            }
        }
        #endregion

        #region Private Methods

        private async Task Distribuir(IList<InventarioMasivoOrden> listaOrden, DetalleInventarioMasivo detalleInventarioMasivo, List<DetalleInventarioMasivo> listDetalleInventarioMasivoDistribuido)
        {
            var stocks = await stockSearcher.GetBy(detalleInventarioMasivo.IdMaterial);
            var listaDetalleDistribuidoLocal = new List<DetalleInventarioMasivo>();
            var cantidadContada = detalleInventarioMasivo.Cantidad;

            foreach (var orden in listaOrden.OrderBy(x => x.Orden))
            {
                var stocksFiltered = GetStocksValue(stocks, orden, detalleInventarioMasivo, listDetalleInventarioMasivoDistribuido);

                foreach (var stock in stocksFiltered)
                {
                    var cantidadStock = GetCantidadStock(stock, detalleInventarioMasivo.TipoStockId);

                    if (cantidadStock == 0 && cantidadContada == 0)
                    {
                        continue;
                    }

                    var detalleInventarioMasivoDistribuido = new DetalleInventarioMasivo();
                    detalleInventarioMasivoDistribuido.Cantidad = cantidadStock > cantidadContada ? cantidadContada : cantidadStock;
                    detalleInventarioMasivoDistribuido.Stock = stock;
                    detalleInventarioMasivoDistribuido.IdStock = stock.Id;
                    detalleInventarioMasivoDistribuido.Posicion = detalleInventarioMasivo.Posicion;
                    detalleInventarioMasivoDistribuido.TipoStockId = detalleInventarioMasivo.TipoStockId;
                    detalleInventarioMasivoDistribuido.Ubicacion = detalleInventarioMasivo.Ubicacion;
                    detalleInventarioMasivoDistribuido.Unidad = detalleInventarioMasivo.Unidad;
                    detalleInventarioMasivoDistribuido.HayConteoErroneo = detalleInventarioMasivoDistribuido.Cantidad != cantidadStock;


                    listaDetalleDistribuidoLocal.Add(detalleInventarioMasivoDistribuido);

                    cantidadContada -= detalleInventarioMasivoDistribuido.Cantidad;
                }
            }

            listaDetalleDistribuidoLocal.RemoveAll(x => GetCantidadStock(x.Stock, x.TipoStockId) == 0 && x.Cantidad == 0);

            if (listaDetalleDistribuidoLocal.Count > 0 && cantidadContada > 0)
            {
                if (listaDetalleDistribuidoLocal.All(d => d.Cantidad == 0))
                {
                    var primeraPosicion = listaDetalleDistribuidoLocal.First();
                    primeraPosicion.Cantidad += cantidadContada;
                    primeraPosicion.HayConteoErroneo = primeraPosicion.Cantidad != GetCantidadStock(primeraPosicion.Stock, detalleInventarioMasivo.TipoStockId);
                }
                else
                {
                    var ultimaPosicion = listaDetalleDistribuidoLocal.Last();
                    ultimaPosicion.Cantidad += cantidadContada;
                    ultimaPosicion.HayConteoErroneo = ultimaPosicion.Cantidad != GetCantidadStock(ultimaPosicion.Stock, detalleInventarioMasivo.TipoStockId);
                }
            }
            else
            {
                Crashes.TrackError(new InvalidOperationException("Sequence contains no elements"), new Dictionary<string, string> { { "Material", detalleInventarioMasivo.Material.Codigo } });
            }
            listDetalleInventarioMasivoDistribuido.AddRange(listaDetalleDistribuidoLocal);
        }

        private void MarcarConteo(IEnumerable<DetalleInventarioMasivo> detalles, EstadoConteoEnum estado)
        {
            foreach (var detalle in detalles)
            {
                detalle.EstadoConteo = estado;
            }
        }

        private double GetCantidadStock(Stock stock, int tipoStockId)
        {
            return tipoStockId == 1 ? stock.CantidadAlmacen :
                       tipoStockId == 2 ? stock.CantidadBloqueado : stock.CantidadCalidad;
        }

        private IList<Stock> GetStocksValue(IList<Stock> stocks, InventarioMasivoOrden orden, DetalleInventarioMasivo detalleInventarioMasivo, IList<DetalleInventarioMasivo> listDetalleInventarioMasivoDistribuido)
        {
            var stocksFiltered = stocks.Where(stock => (orden.AlmacenId == 0 || stock.IdAlmacen == orden.AlmacenId)
                   && (string.IsNullOrWhiteSpace(detalleInventarioMasivo.Ubicacion) || stock.Ubicacion == detalleInventarioMasivo.Ubicacion)
                   && (stock.DetalleStockEspecial.StockEspecial.Codigo == "Q" || stock.DetalleStockEspecial.StockEspecial.Codigo == "S")
                   && (orden.StockEspecialId == 0 || stock.DetalleStockEspecial.IdStockEspecial == orden.StockEspecialId)
                   && (orden.ClaseDeValoracionId == 0 || stock.IdClaseDeValoracion == orden.ClaseDeValoracionId)
                   && !listDetalleInventarioMasivoDistribuido.Any(dim => dim.IdStock == stock.Id && dim.TipoStockId == detalleInventarioMasivo.TipoStockId))
                .OrderByDescending(stock => detalleInventarioMasivo.TipoStockId == 1 ? stock.CantidadAlmacen :
                       detalleInventarioMasivo.TipoStockId == 2 ? stock.CantidadBloqueado : stock.CantidadCalidad)
                .ToList();
            return stocksFiltered;
        }

        private int CalcularPosicion(InventarioMasivo inventarioMasivo)
        {
            var detalle = inventarioMasivo.DetallesInventarioMasivo.LastOrDefault();
            return detalle != null ? detalle.Posicion + 10 : 10;
        }

        private async Task<TipoLote> CalcularTipoLote(int materialId)
        {
            var lotes = (await stockSearcher.GetByUbicacion(materialId, null))
                .Where(stock => (stock.DetalleStockEspecial.StockEspecial.Codigo == "Q" || stock.DetalleStockEspecial.StockEspecial.Codigo == "S") && ValidateCantidades(stock))
                .Select(x => x.ClaseDeValoracion).ToList();

            if (lotes.Count > 0 && lotes.All(x => x.EsUsado))
                return TipoLote.Usado;
            else if (lotes.Count == 0 || lotes.All(x => !x.EsUsado))
                return TipoLote.Nuevo;
            else
                return TipoLote.Mixto;
        }

        private bool ValidateCantidades(Stock stock)
        {
            return (stock.CantidadAlmacen > 0 || stock.CantidadBloqueado > 0 || stock.CantidadCalidad > 0);
        }
        #endregion
    }
}
