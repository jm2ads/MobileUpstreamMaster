using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers.Movimientos;
using Frontend.Azure.Mappers.Movimientos.Traslados;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Traslados;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    class TrasladoAzureRestService : ISyncRestService<Traslado>
    {
        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly MovimientoLogMapper movimientoLogMapper;
        private readonly ISettingsService settingsService;
        private readonly TrasladoMapper trasladoMapper;

        #endregion

        #region Public Methods

        public TrasladoAzureRestService(YpfAzureHttpClient client, IMovimientoLogService movimientoLogService, 
            TrasladoMapper trasladoMapper, MovimientoLogMapper movimientoLogMapper, ISettingsService settingsService)
        {
            this.client = client;
            this.movimientoLogService = movimientoLogService;
            this.movimientoLogMapper = movimientoLogMapper;
            this.settingsService = settingsService;
            this.trasladoMapper = trasladoMapper;
        }

        public Task<IList<Traslado>> DoGet(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<Traslado> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Traslado>> DoPost(object body)
        {
            var entities = (IList<Traslado>)body;
            return await SendTraslados(entities);
        }

        private async Task<IList<Traslado>> SendTraslados(IList<Traslado> listaTraslados)
        {
            var traslados = await trasladoMapper.MapToDto(listaTraslados);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.TrasladosEnviar, traslados, HttpMethod.Post, null);

            return await HandlerRespuestaDto(listRespuestaDto, listaTraslados);
        }

        private async Task<IList<Traslado>>  HandlerRespuestaDto(IList<RespuestaDto> listRespuestaDto, IList<Traslado> listaTraslados)
        {
            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            var movimientoLogList = await movimientoLogService.Generate(await movimientoLogMapper.MapFromDto(listRespuestaDtoErrors, TipoMovimiento.Traslado));
            var delete = movimientoLogService.Delete(listRespuestaDtoSuccess.Select(x => x.Id).ToList());

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            return listaTraslados.Where(traslado => listRespuestaDtoSuccess.Select(x => x.Id).Contains(traslado.Id)).ToList();
        }

        #endregion
    }
}
