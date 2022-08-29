using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers.Movimientos;
using Frontend.Azure.Mappers.Movimientos.Reservas;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.NotasDeReservas;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class NotaDeReservaAzureRestService : ISyncRestService<NotaDeReserva>
    {

        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly NotaDeReservaMapper notaDeReservaMapper;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly MovimientoLogMapper movimientoLogMapper;
        private readonly ISettingsService settingsService;

        #endregion

        #region Public Methods

        public NotaDeReservaAzureRestService(YpfAzureHttpClient client, NotaDeReservaMapper notaDeReservaMapper, IMovimientoLogService movimientoLogService,
            MovimientoLogMapper movimientoLogMapper, ISettingsService settingsService)
        {
            this.client = client;
            this.notaDeReservaMapper = notaDeReservaMapper;
            this.movimientoLogService = movimientoLogService;
            this.movimientoLogMapper = movimientoLogMapper;
            this.settingsService = settingsService;
        }
        
        public Task<IList<NotaDeReserva>> DoGet(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<NotaDeReserva> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<NotaDeReserva>> DoPost(object body)
        {
            var entities = (IList<NotaDeReserva>)body;
            return await SendReservas(entities);
        }

        private async Task<IList<NotaDeReserva>> SendReservas(IList<NotaDeReserva> notaDeReservas)
        {
            var notasDeReservasOutputDto = notaDeReservaMapper.MapToDto(notaDeReservas);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.ReservasActualizar, notasDeReservasOutputDto, HttpMethod.Post, null);

            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            var movimientoLogList = await movimientoLogService.Generate(await movimientoLogMapper.MapFromDto(listRespuestaDtoErrors, TipoMovimiento.Reserva));
            var delete = movimientoLogService.Delete(listRespuestaDtoSuccess.Select(x => x.Id).ToList());

            return notaDeReservas.Where(notaDeReserva => listRespuestaDtoSuccess.Select(x => x.Id).Contains(notaDeReserva.ReservaId)).ToList();
        }

        #endregion
    }
}
