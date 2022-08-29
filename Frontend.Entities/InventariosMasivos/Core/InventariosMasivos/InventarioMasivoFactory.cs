using Frontend.Business.Centros;
using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.InventariosMasivos.Core
{
    public class InventarioMasivoFactory
    {
        private readonly DetalleInventarioMasivoFactory detalleInventarioMasivoFactory;

        public InventarioMasivoFactory(DetalleInventarioMasivoFactory detalleInventarioMasivoFactory)
        {
            this.detalleInventarioMasivoFactory = detalleInventarioMasivoFactory;
        }

        public InventarioMasivo Create(Centro centro)
        {
            return new InventarioMasivo()
            {
                Centro = centro,
                IdCentro = centro.Id,
                FechaCreacion = DateTime.Now,
                FechaDocumento = DateTime.Now,
                NumeroProvisorio = "-" + DateTime.Now.ToString("ddMMyyhhmmssff"),
                AlmacenesExcluidos = new List<Almacenes.Almacen>(),
                DetallesInventarioMasivo = new List<DetalleInventarioMasivo>(),
                Estado = EstadoInventario.Provisorio
            };
        }

        public async Task<InventarioMasivo> Create(InventarioMasivo inventarioMasivo, IList<InventarioMasivoOrden> orden)
        {
            var inventarioMasivoDistribuido = new InventarioMasivo();

            inventarioMasivoDistribuido.Centro = inventarioMasivo.Centro;
            inventarioMasivoDistribuido.IdCentro = inventarioMasivo.IdCentro;
            inventarioMasivoDistribuido.Estado = EstadoInventario.PendienteAprobacionSap;
            inventarioMasivoDistribuido.NumeroProvisorio = inventarioMasivo.NumeroProvisorio;
            inventarioMasivoDistribuido.UsuarioCreacion = inventarioMasivo.UsuarioCreacion;
            inventarioMasivoDistribuido.UsuarioModificacion = inventarioMasivo.UsuarioModificacion;
            inventarioMasivoDistribuido.FechaCreacion = inventarioMasivo.FechaCreacion;
            inventarioMasivoDistribuido.FechaDocumento = inventarioMasivo.FechaDocumento;
            inventarioMasivoDistribuido.Ubicacion = inventarioMasivo.Ubicacion;
            inventarioMasivoDistribuido.AlmacenesExcluidos = inventarioMasivo.AlmacenesExcluidos;
            inventarioMasivoDistribuido.DetallesInventarioMasivo = new List<DetalleInventarioMasivo>();

            inventarioMasivoDistribuido.DetallesInventarioMasivo.AddRange(await detalleInventarioMasivoFactory.Create(inventarioMasivo.DetallesInventarioMasivo, orden));

            return inventarioMasivoDistribuido;
        }

    }
}
