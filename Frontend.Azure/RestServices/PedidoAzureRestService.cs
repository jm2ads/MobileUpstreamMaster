using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IData;
using Frontend.Business.Movimientos;
using Frontend.Business.Movimientos.Ingresos;
using Frontend.IServices.IServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class PedidoAzureRestService : ISyncRestService<Pedido>
    {
        #region Private Properties

        private readonly PedidoMapper mapper;
        private readonly ISettingsService settingsService;
        private readonly IPedidoService pedidoService;
        private readonly YpfAzureHttpClient client;

        #endregion

        public PedidoAzureRestService(YpfAzureHttpClient client, PedidoMapper mapper, ISettingsService settingsService, IPedidoService pedidoService)
        {
            this.client = client;
            this.mapper = mapper;
            this.settingsService = settingsService;
            this.pedidoService = pedidoService;
        }


        public Task<IList<Pedido>> DoGet(object parameters)
        {
            return GetAllPedidos();
        }
        public Task<Pedido> DoGetEntity(object parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Pedido>> DoPost(object body)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Pedido>> GetAllPedidos()
        {
            var setting = await settingsService.Get();
            var pedidoRequestDto = await mapper.MapToDto(setting, EstadoMovimiento.Recibir, EstadoMovimiento.RechazadoSap);
            var pedidos = await client.CallWithHeaders<List<PedidoInputDto>>(UrlConstants.PedidosGetAllByCentroEstados, pedidoRequestDto, HttpMethod.Post, null);
            var list = await mapper.MapFromDto(pedidos, setting.CentroActivoId);
            return list;
        }
    }
}
