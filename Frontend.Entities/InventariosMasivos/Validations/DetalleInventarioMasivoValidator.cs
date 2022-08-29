using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.Materiales;
using Frontend.Business.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Business.InventariosMasivos.Validations
{
    public class DetalleInventarioMasivoValidator
    {
        public bool Validate(DetalleInventarioMasivo detalleInventarioMasivo)
        {
            return detalleInventarioMasivo.Material != null
                && detalleInventarioMasivo.Cantidad > 0
                ;
        }

        public List<string> ValidatePep(IList<Stock> listaStock, int stockEspecialId, string ubicacion, int? almacenId, int? claseDeValoracionId)
        {
            var detallesStock = listaStock.Where(stock => stock.DetalleStockEspecial.IdStockEspecial == stockEspecialId
            && (almacenId == null || stock.IdAlmacen == almacenId)
            && (claseDeValoracionId == null || stock.IdClaseDeValoracion == claseDeValoracionId)
            && stock.Ubicacion == ubicacion);
            return detallesStock?.Select(x => x.DetalleStockEspecial.Detalle).ToList();
        }

        public List<ClaseDeValoracion> ValidateLote(IList<ClaseDeValoracion> lotes)
        {
            return lotes.Where(x => x.EsUsado).ToList();
        }

        public DetalleInventarioMasivo EsDuplicado(InventarioMasivo inventarioMasivo, DetalleInventarioMasivo detalleInventarioMasivo)
        {
            return inventarioMasivo.DetallesInventarioMasivo.FirstOrDefault(dim =>
                                        dim.IdMaterial == detalleInventarioMasivo.IdMaterial
                                        && dim.IdAlmacen == detalleInventarioMasivo.IdAlmacen
                                        && dim.Ubicacion == detalleInventarioMasivo.Ubicacion
                                        && dim.IdLote == detalleInventarioMasivo.IdLote
                                        && dim.PEP == detalleInventarioMasivo.PEP
                                        && dim.TipoStockId == detalleInventarioMasivo.TipoStockId
                                        && !inventarioMasivo.DetallesInventarioMasivo.Contains(detalleInventarioMasivo));
        }

        public bool ValidateMaterial(Material material, IList<Stock> stocks)
        {
            return stocks.Any(stock => stock.IdMaterial == material.Id);
        }
    }
}
