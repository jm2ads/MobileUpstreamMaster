using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.DTOs.Movimientos.SalidasInternas;
using Frontend.Azure.Mappers;
using Frontend.Azure.Mappers.Movimientos;
using Frontend.Azure.Mappers.Movimientos.SalidasInternas;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.Business.Movimientos.SalidasInternas;
using Frontend.Business.Synchronizer;
using Frontend.IServices.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    class SalidaInternaAzureRestService : ISyncRestService<SalidaInterna>
    {
        #region Private Properties

        private readonly SalidaInternaMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly ISalidaInternaService salidaInternaService;
        private readonly IMovimientoLogService movimientoLogService;
        private readonly MovimientoLogMapper movimientoLogMapper;
        private readonly YpfAzureHttpClient client;

        #endregion

        public SalidaInternaAzureRestService(YpfAzureHttpClient client, SalidaInternaMapper mapper, ISettingsService settingsService, ISalidaInternaService salidaInternaService,
            IMovimientoLogService movimientoLogService, MovimientoLogMapper movimientoLogMapper)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
            this.salidaInternaService = salidaInternaService;
            this.movimientoLogService = movimientoLogService;
            this.movimientoLogMapper = movimientoLogMapper;
        }


        public Task<IList<SalidaInterna>> DoGet(object parameters)
        {
            return GetAllSalidasInternas();
        }

        public Task<SalidaInterna> DoGetEntity(object parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<SalidaInterna>> DoPost(object body)
        {
            var salidasInternas = (IList<SalidaInterna>)body;
            var salidasInternasFinalizadas = salidasInternas.Where(x => x.Estado == EstadoMovimiento.PendienteAprobacionSap).ToList();
            if (salidasInternasFinalizadas.Count == 0)
            {
                return null;
            }
            var salidasInternasFinalizadasOutputDto = mapper.MapToDto(salidasInternasFinalizadas);
            var listRespuestaDto = await client.CallWithHeaders<IList<RespuestaDto>>(UrlConstants.SalidasInternasEnviar, salidasInternasFinalizadasOutputDto, HttpMethod.Post, null);
            
            var listRespuestaDtoErrors = listRespuestaDto.Where(repuesta => !repuesta.Success).ToList();
            var listRespuestaDtoSuccess = listRespuestaDto.Where(repuesta => repuesta.Success).ToList();

            await settingsService.SetHasSyncWithError(listRespuestaDtoErrors.Count > 0);

            var movimientoLogList = await movimientoLogService.Generate(await movimientoLogMapper.MapFromDto(listRespuestaDtoErrors, TipoMovimiento.SalidaInterna));
            var delete = movimientoLogService.Delete(listRespuestaDtoSuccess.Select(x => x.Id).ToList());

            return salidasInternasFinalizadas.Where(salidaInterna => listRespuestaDtoSuccess.Select(x => x.Id).Contains(salidaInterna.Id)).ToList();
        }

        public async Task<IList<SalidaInterna>> GetAllSalidasInternas()
        {
            var setting = await settingsService.Get();
            var selidaInternaRequestDto = await mapper.MapToDto(setting, EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);
            var salidasInternas = await client.CallWithHeaders<List<SalidaInternaInputDto>>(UrlConstants.SalidasInternasGetByCentroEstados, selidaInternaRequestDto, HttpMethod.Post, null);
            var list = mapper.MapFromDto(salidasInternas);
            return list;
        }
    }
}
