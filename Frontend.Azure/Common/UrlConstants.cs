using Frontend.Commons.Commons;

namespace Frontend.Azure.Common
{
    public class UrlConstants
    {
        public const string ApiRestUrl = ApplicationConstants.BaseApiRestAzure;

        public const string CentroApi = "Centros/GetList";

        public const string CentrosByIdRed = "Centros/GetCentrosByIdRed";

        public const string FuncionalidadApi = "Permisos/GetList";

        public const string FuncionalidadByIdRedCentroApi = "Permisos/GetPermisosByIdRedCentro";

        public const string AlmacenApi = "Almacenes/GetList";

        public const string ClaseDeValoracionApi = "ClasesDeValoracion/GetList";

        public const string StockEspecialApi = "StocksEspecial/GetAll";

        public const string MaterialApi = "Materiales/GetByCentro";

        public const string StockApi = "Stocks/GetByCentro";

        public const string CredencialApi = "Credentials/GetList";

        public const string LoginUser = "Login/LoginUser";

        public const string RegisterUser = "Login/RegisterUser";

        public const string ValidateToken = "Login/ValidateUser";

        public const string GruposDeArticulosApi = "GruposDeArticulo/GetList";

        public const string DetalleStockEspecialApi = "Stocks/GetDetalleStockEspecial";

        public const string MovimientoApi = "Movimientos/GetByCentro";

        //Inventarios
        public const string InventariosGetByCentro = "Inventarios/GetByCentro";
        public const string InventariosGetByCentroEstado = "Inventarios/GetByCentroEstado";
        public const string InventariosGetByCentroEstados = "Inventarios/GetByCentroEstados";
        public const string InventariosActualizar = "Inventarios/Actualizar";
        public const string InventariosCreate = "Inventarios/Create";
        public const string InventariosRecontar = "Inventarios/Recontar";
        public const string InventariosEnviarSAP = "Inventarios/EnviarSAP"; // REVISAR
        public const string InventariosRechazar = "Inventarios/Rechazar";
        public const string EstadosInventarioApi = "EstadosInventario/GetList";
        public const string InventariosMasivosOrdenGetByCentro = "InventariosMasivos/GetOrdenByCentro";
        public const string InventariosMasivosCreate = "InventariosMasivos/Create";
        public const string InventariosCambioUbicacion = "Inventarios/CambioUbicacion"; // REVISAR

        //Movimientos
        public const string PedidosGetAllByCentro = "Pedidos/GetByCentro";
        public const string PedidosGetAllByCentroEstados = "Pedidos/GetByCentroEstados";
        public const string ReservasGetByCentro = "Reservas/GetByCentro";
        public const string ReservasGetByCentroEstados = "Reservas/GetByCentroEstados";
        public const string ReservasActualizar = "Reservas/Actualizar"; // REVISAR
        public const string EntregasActualizar = "Pedidos/Actualizar"; // REVISAR
        public const string TrasladosEnviar = "Traslados/Enviar";
        public const string SalidasInternasGetByCentro = "SalidasInternas/GetByCentroEstados";
        public const string SalidasInternasGetByCentroEstados = "SalidasInternas/GetByCentroEstados";
        public const string SalidasInternasEnviar = "SalidasInternas/Enviar"; // REVISAR

    }
}
