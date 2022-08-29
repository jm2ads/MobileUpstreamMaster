using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.DetallesInventarios;
using Frontend.Azure.DTOs.Inventarios;
using Frontend.Business.Almacenes.Searchers;
using Frontend.Business.Centros.Searchers;
using Frontend.Business.DetallesInventario;
using Frontend.Business.Inventarios;
using Frontend.Business.Inventarios.Core;
using Frontend.Business.Inventarios.StockEspeciales.Searchers;
using Frontend.Business.Settings;
using Frontend.Business.Settings.Searchers;
using Frontend.Commons.Commons;
using Frontend.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class InventarioMapper
    {
        private readonly InventarioFactory inventarioFactory;
        private readonly DetalleInventarioMapper detalleInventarioMapper;
        private readonly StockEspecialSearcher stockEspecialSearcher;
        private readonly CentroSearcher centroSearcher;
        private readonly SettingSearcher settingSearcher;
        private readonly AlmacenSearcher almacenSearcher;

        public InventarioMapper(InventarioFactory inventarioFactory, DetalleInventarioMapper detalleInventarioMapper, StockEspecialSearcher stockEspecialSearcher,
            CentroSearcher centroSearcher, SettingSearcher settingSearcher, AlmacenSearcher almacenSearcher)
        {
            this.inventarioFactory = inventarioFactory;
            this.detalleInventarioMapper = detalleInventarioMapper;
            this.stockEspecialSearcher = stockEspecialSearcher;
            this.centroSearcher = centroSearcher;
            this.settingSearcher = settingSearcher;
            this.almacenSearcher = almacenSearcher;
        }

        public async Task<Inventario> MapFromDto(InventarioInputDto inventarioResponseDto, int centroId)
        {
            var inventario = new Inventario();
            inventario.DetallesInventario = new List<DetalleInventario>();

            inventario.Id = inventarioResponseDto.Id;
            inventario.IdCentro = centroId;
            inventario.IdStockEspecial = inventarioResponseDto.StockEspecialId;
            inventario.NumeroProvisorio = inventarioResponseDto.NumeroProvisorio;
            inventario.NumeroSAP = inventarioResponseDto.NumeroSAP;
            inventario.EsProvisorio = inventarioResponseDto.EsProvisorio;
            inventario.FechaCreacion = DateTime.Parse(inventarioResponseDto.FechaCreacion, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            inventario.FechaModificacion = DateTime.Parse(inventarioResponseDto.FechaModificacion, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            inventario.FechaRecuento = DateTime.Parse(inventarioResponseDto.FechaRecuento, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            inventario.UsuarioCreacion = inventarioResponseDto.UsuarioCreacion;
            inventario.UsuarioModificacion = inventarioResponseDto.UsuarioModificacion;
            inventario.Estado = inventarioResponseDto.EstadoInventarioId;
            inventario.Ejercicio = inventarioResponseDto.Ejercicio;
            inventario.ComentarioRechazo = inventarioResponseDto.Comentario;
            inventario.StockEspecialDescripcion = inventarioResponseDto.StockEspecialDescripcion;
            inventario.HayDiferencia = inventarioResponseDto.HayDiferencia;
            inventario.HayConteoErroneo = inventarioResponseDto.HayConteoErroneo;
            inventario.EstadoConteo = inventarioResponseDto.EstadoConteo;


            if (inventarioResponseDto.AlmacenId.HasValue)
            {
                inventario.IdAlmacen = inventarioResponseDto.AlmacenId.Value;
                inventario.AlmacenCodigo = inventarioResponseDto.AlmacenCodigo;
            }
            
            foreach (var item in inventarioResponseDto.DetallesInventario)
            {
                inventario.DetallesInventario.Add(await MapDetalleInventario(item, inventario.Id));
            }

            return inventario;
        }

        public async Task<IList<Inventario>> MapFromDto(IList<InventarioInputDto> inventarioResponseDto, int centroId)
        {
            IList<Inventario> listInventarios = new List<Inventario>();

            foreach (var inventarioInputDto in inventarioResponseDto)
            {
                listInventarios.Add(await MapFromDto(inventarioInputDto, centroId));
            }

            return listInventarios;
        }

        private async Task<DetalleInventario> MapDetalleInventario(DetalleInventarioInputDto detalleInventarioResponseDto, int inventarioId)
        {
            return await detalleInventarioMapper.MapFromDto(detalleInventarioResponseDto, inventarioId);
        }

        public async Task<IList<InventarioInputDto>> MapToDto(IList<Inventario> inventarios)
        {
            IList<InventarioInputDto> listInventariosDto = new List<InventarioInputDto>();

            foreach (var inventario in inventarios)
            {
                listInventariosDto.Add(await MapToDto(inventario));
            }

            return listInventariosDto;
        }

        public async Task<InventarioInputDto> MapToDto(Inventario inventario)
        {
            var inventarioDto = new InventarioInputDto();
            inventarioDto.DetallesInventario = new List<DetalleInventarioInputDto>();

            inventarioDto.Id = inventario.Id;
            inventarioDto.NumeroSAP = inventario.NumeroSAP;
            inventarioDto.NumeroProvisorio = inventario.NumeroProvisorio;
            inventarioDto.ProvisorioAnterior = inventario.ProvisorioAnterior;
            inventarioDto.Ejercicio = inventario.Ejercicio;
            inventarioDto.Comentario = inventario.ComentarioRechazo;
            inventarioDto.FechaCreacion = inventario.FechaCreacion.ToString("yyyy-MM-dd");
            inventarioDto.FechaRecuento = inventario.FechaRecuento.ToString("yyyy-MM-dd");
            inventarioDto.UsuarioCreacion = inventario.UsuarioCreacion;
            inventarioDto.UsuarioModificacion = inventario.UsuarioModificacion;
            inventarioDto.EstadoInventarioId = inventario.Estado; 
            inventarioDto.CentroId = inventario.IdCentro;
            inventarioDto.EsProvisorio = inventario.EsProvisorio;
            inventarioDto.AlmacenId = inventario.IdAlmacen;
            inventarioDto.StockEspecialId = inventario.IdStockEspecial;
            inventarioDto.HayConteoErroneo = inventario.HayConteoErroneo;

            foreach (var item in inventario.DetallesInventario)
            {
                inventarioDto.DetallesInventario.Add(await MapDetalleInventario(item));
            }

            return inventarioDto;
        }

        private async Task<DetalleInventarioInputDto> MapDetalleInventario(DetalleInventario detalleInventario)
        {
            return await detalleInventarioMapper.MapToDto(detalleInventario);
        }

        public async Task<InventarioOutputDto> MapToDto(Setting setting, params EstadoInventario[] estadoInventarioIds)
        {
            var inventarioOutputDto = new InventarioOutputDto();
            inventarioOutputDto.CentroId = setting.CentroActivoId;
            inventarioOutputDto.delta = ApplicationConstants.DefaultDateSync.ToString("O");

            inventarioOutputDto.EstadoInventarioIds = estadoInventarioIds.Select(x => x.GetHashCode()).ToArray();

            return inventarioOutputDto;
        }

    }
}
