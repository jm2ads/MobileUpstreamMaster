using Frontend.Azure.DTOs.DetallesInventarios;
using Frontend.Azure.DTOs.Inventarios;
using Frontend.Azure.Mappers.DetallesInventarioLocal;
using Frontend.Business.DetallesInventarioLocal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers.InventarioLocal
{
    public class InventarioLocalMapper
    {
        private readonly DetalleInventarioLocalMapper detalleInventarioMapper;

        public InventarioLocalMapper(DetalleInventarioLocalMapper detalleInventarioMapper)
        {
            this.detalleInventarioMapper = detalleInventarioMapper;
        }

        public async Task<CrearInventarioInputDto> MapToDto(Business.InventariosLocales.InventarioLocal inventario)
        {
            var inventarioDto = new CrearInventarioInputDto();
            inventarioDto.DetallesInventario = new List<DetalleCrearInventarioInputDto>();

            inventarioDto.NumeroProvisorio = inventario.NumeroProvisorio;
            inventarioDto.ProvisorioAnterior = inventario.ProvisorioAnterior;
            inventarioDto.EsProvisorio = inventario.EsProvisorio;
            inventarioDto.FechaCreacion = inventario.FechaCreacion.ToString("yyyy-MM-dd");
            inventarioDto.FechaRecuento = inventario.FechaRecuento.ToString("yyyy-MM-dd");
            inventarioDto.UsuarioCreacion = inventario.UsuarioCreacion;
            inventarioDto.UsuarioModificacion = inventario.UsuarioModificacion;
            inventarioDto.EstadoInventarioId = Convert.ToInt32(inventario.Estado); 
            inventarioDto.CentroId = inventario.IdCentro;
            inventarioDto.AlmacenId = inventario.IdAlmacen;
            inventarioDto.StockEspecialId = inventario.IdStockEspecial;
            inventarioDto.Comentario = inventario.ComentarioRechazo;

            foreach (var item in inventario.DetallesInventario)
            {
                inventarioDto.DetallesInventario.Add(await MapDetalleInventario(item, inventario.IdStockEspecial));
            }

            return inventarioDto;
        }

        private async Task<DetalleCrearInventarioInputDto> MapDetalleInventario(DetalleInventarioLocal detalleInventario, int stockEspecialId)
        {
            return await detalleInventarioMapper.MapToDto(detalleInventario, stockEspecialId);
        }

        public async Task<IList<CrearInventarioInputDto>> MapToDto(IList<Business.InventariosLocales.InventarioLocal> inventarios)
        {
            IList<CrearInventarioInputDto> listInventariosDto = new List<CrearInventarioInputDto>();

            foreach (var inventario in inventarios)
            {
                listInventariosDto.Add(await MapToDto(inventario));
            }

            return listInventariosDto;
        }
    }
}
