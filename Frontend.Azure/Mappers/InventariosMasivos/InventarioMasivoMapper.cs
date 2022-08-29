using Frontend.Azure.DTOs.InventariosMasivos;
using Frontend.Business.InventariosMasivos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Azure.Mappers.InventariosMasivos
{
    public class InventarioMasivoMapper
    {
        private readonly DetalleInventarioMasivoMapper detalleInventarioMasivoMapper;

        public InventarioMasivoMapper(DetalleInventarioMasivoMapper detalleInventarioMasivoMapper)
        {
            this.detalleInventarioMasivoMapper = detalleInventarioMasivoMapper;
        }

        public InventarioMasivoOutputDto MapToDto(InventarioMasivo inventarioMasivo)
        {
            var inventarioMasivoOutputDto = new InventarioMasivoOutputDto();

            inventarioMasivoOutputDto.CentroId = inventarioMasivo.IdCentro;
            inventarioMasivoOutputDto.FechaCreacion = inventarioMasivo.FechaCreacion;
            inventarioMasivoOutputDto.FechaDocumento = inventarioMasivo.FechaDocumento;
            inventarioMasivoOutputDto.NumeroProvisorio = inventarioMasivo.NumeroProvisorio;
            inventarioMasivoOutputDto.UsuarioCreacion = inventarioMasivo.UsuarioCreacion;
            inventarioMasivoOutputDto.AlmacenesExcluidos = String.Join(";", inventarioMasivo.AlmacenesExcluidos.Select(almacen => almacen.Codigo));

            inventarioMasivoOutputDto.DetallesInventarioMasivo = detalleInventarioMasivoMapper.MapToDto(inventarioMasivo.DetallesInventarioMasivo).ToList();

            return inventarioMasivoOutputDto;
        }

        public IEnumerable<InventarioMasivoOutputDto> MapToDto(IList<InventarioMasivo> listInventarioMasivo)
        {
            foreach (var inventarioMasivo in listInventarioMasivo)
            {
                yield return MapToDto(inventarioMasivo);
            }
        }
    }
}
