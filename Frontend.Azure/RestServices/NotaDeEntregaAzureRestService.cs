using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Azure.Mappers.Movimientos;
using Frontend.Azure.Mappers.Movimientos.Ingresos;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class NotaDeEntregaAzureRestService : ISyncRestService<NotaDeEntrega>
    {
        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly NotaDeEntregaMapper notaDeEntregaMapper;
        private readonly PedidoMapper pedidoMapper;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly MovimientoLogMapper movimientoLogMapper;
        private readonly ISettingsService settingsService;

        #endregion

        #region Public Methods

        public NotaDeEntregaAzureRestService(YpfAzureHttpClient client, NotaDeEntregaMapper notaDeEntregaMapper, PedidoMapper pedidoMapper, IMovimientoLogService movimientoLogService,
            MovimientoLogMapper movimientoLogMapper, ISettingsService settingsService)
        {
            this.client = client;
            this.notaDeEntregaMapper = notaDeEntregaMapper;
            this.pedidoMapper = pedidoMapper;
            this.movimientoLogService = movimientoLogService;
            this.movimientoLogMapper = movimientoLogMapper;
            this.settingsService = settingsService;
        }

        public Task<IList<NotaDeEntrega>> DoGet(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<NotaDeEntrega> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<NotaDeEntrega>> DoPost(object body)
        {
            var entities = (IList<NotaDeEntrega>)body;
            return await SendEntregas(entities);
        }

        private async Task<IList<NotaDeEntrega>> SendEntregas(IList<NotaDeEntrega> notaDeEntregas)
        {
            var notasDeEntrega = notaDeEntregaMapper.MapToDto(notaDeEntregas);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.EntregasActualizar, notasDeEntrega, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            var movimientoLogList = await movimientoLogService.Generate(await movimientoLogMapper.MapFromDto(listRespuestaDtoErrors, TipoMovimiento.Pedido));
            var delete = movimientoLogService.Delete(listRespuestaDtoSuccess.Select(x=>x.Id).ToList());

            return notaDeEntregas.Where(notaDeEntrega => listRespuestaDtoSuccess.Select(x => x.Id).Contains(notaDeEntrega.PedidoId)).ToList();
        }

        #endregion
    }
}
