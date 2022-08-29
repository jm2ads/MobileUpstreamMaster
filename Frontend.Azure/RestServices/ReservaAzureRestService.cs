using Frontend.Azure.Common;
using Frontend.Azure.DTOs.Movimientos.Reservas;
using Frontend.Azure.Mappers.Movimientos.Reservas;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Reservas;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class ReservaAzureRestService : ISyncRestService<Reserva>
    {

        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly ISettingsService settingsService;
        private readonly ReservaMapper reservaMapper;

        #endregion

        #region Public Methods

        public ReservaAzureRestService(YpfAzureHttpClient client, ISettingsService settingsService, ReservaMapper reservaMapper)
        {
            this.client = client;
            this.settingsService = settingsService;
            this.reservaMapper = reservaMapper;
        }

        public async Task<IList<Reserva>> GetAllByCentro()
        {
            var setting = await settingsService.Get();
            var reservaOutputDto = await reservaMapper.MapToDto(setting, EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);
            var reservas = await client.CallWithHeaders<List<ReservaInputDto>>(UrlConstants.ReservasGetByCentroEstados, reservaOutputDto, HttpMethod.Post, null);
            return await reservaMapper.MapFromDto(reservas, setting.CentroActivoId);
        }

        public Task<IList<Reserva>> DoGet(object parameters)
        {
            return GetAllByCentro();
        }

        public Task<Reserva> DoGetEntity(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Reserva>> DoPost(object body)
        {
            return null;
        }

        #endregion
    }
}
