using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Azure.Mappers.Inventarios;
using Frontend.Business.IData;
using Frontend.Business.Inventarios;
using Frontend.Commons.Enums;
using Frontend.IServices.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class InventarioAzureRestService : ISyncRestService<Inventario>
    {
        #region Private Properties

        private readonly InventarioMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly IInventarioService inventarioService;
        private readonly InventarioLogMapper inventarioLogMapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        #region Public Methods

        public InventarioAzureRestService(YpfAzureHttpClient client, InventarioMapper mapper, ISettingsService settingsService, IInventarioService inventarioService, InventarioLogMapper inventarioLogMapper)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
            this.inventarioService = inventarioService;
            this.inventarioLogMapper = inventarioLogMapper;
        }

        public async Task<IList<Inventario>> GetAllInventariosByCentroEstados()
        {
            var setting = await settingsService.Get();
            var json = JsonConvert.SerializeObject(await mapper.MapToDto(setting, EstadoInventario.PendienteAprobacion, EstadoInventario.Rechazado, EstadoInventario.PendienteAprobacionSap, EstadoInventario.RechazadoSAP, EstadoInventario.Recuento));
            var inventarios = await client.CallWithHeaders<List<InventarioInputDto>>(UrlConstants.InventariosGetByCentroEstados, await mapper.MapToDto(setting, EstadoInventario.PendienteAprobacion, EstadoInventario.Rechazado, EstadoInventario.PendienteAprobacionSap, EstadoInventario.RechazadoSAP, EstadoInventario.Recuento), HttpMethod.Post, null);
            return await mapper.MapFromDto(inventarios, setting.CentroActivoId);
        }

        public Task<IList<Inventario>> DoGet(object parameters)
        {
            return GetAllInventariosByCentroEstados();
        }

        public Task<Inventario> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Inventario>> DoPost(object body)
        {
            var entities = (IList<Inventario>)body;
            await SendInventariosAprobados(entities);
            await SendInventariosRechazados(entities);
            await SendInventariosConRecuento(entities);
            await SendInventariosActualizados(entities);

            return null;
        }

        private async Task SendInventariosAprobados(IList<Inventario> inventarios)
        {
            var inventariosAprobados = inventarios.Where(x => x.Estado == EstadoInventario.Aprobado).ToList();
            if (inventariosAprobados.Count == 0)
            {
                return;
            }
            var inventariosAprobadosInput = await mapper.MapToDto(inventariosAprobados);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosEnviarSAP, inventariosAprobadosInput, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());
        }

        private async Task SendInventariosRechazados(IList<Inventario> inventarios)
        {
            var inventariosRechazados = inventarios.Where(x => x.Estado == EstadoInventario.Rechazado).ToList();
            if (inventariosRechazados.Count == 0)
            {
                return;
            }
            var inventariosRechazadosInput = await mapper.MapToDto(inventariosRechazados);
            var json = JsonConvert.SerializeObject(inventariosRechazadosInput);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosRechazar, inventariosRechazadosInput, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());
        }

        private async Task SendInventariosConRecuento(IList<Inventario> inventarios)
        {
            var inventariosConRecuento = inventarios.Where(x => x.Estado == EstadoInventario.PendienteAprobacionSap).ToList();
            if (inventariosConRecuento.Count == 0)
            {
                return;
            }
            var inventariosConRecuentoInput = await mapper.MapToDto(inventariosConRecuento);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosRecontar, inventariosConRecuentoInput, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());
        }

        private async Task SendInventariosActualizados(IList<Inventario> inventarios)
        {
            var inventariosCreados = inventarios.Where(x => x.Estado == EstadoInventario.PendienteAprobacion).ToList();
            if (inventariosCreados.Count == 0)
            {
                return;
            }
            var inventariosCreadosInput = await mapper.MapToDto(inventariosCreados);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.InventariosActualizar, inventariosCreadosInput, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            await inventarioService.Generate(await inventarioLogMapper.MapFromDto(listRespuestaDtoErrors));
            var delete = inventarioService.DeleteLog(listRespuestaDtoSuccess.Select(x => x.Id).ToList());
        }

        #endregion
    }
}
