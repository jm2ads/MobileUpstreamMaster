
using System.Collections.Generic;

namespace Frontend.Commons.Commons.Errors
{
    public static class BusinessErrorCode
    {
        public const string RecursoNoEncontrado = "BE_404";
        public const string ReglasNegocioNoCumplidas = "BE_418";
        public const string ErrorInterno = "BE_500";
        public const string BadGateway = "BE_502";
        public const string ParametrosFaltantes = "BE_600";
        public const string ParametrosFormatoInvalido = "BE_601";
        public const string ParametrosDesconocidos = "BE_602";
        public const string ActivacionInvalida = "BE_603";
        public const string ErrorNegocioPorIntegracion = "BE_605";
        public const string ParametrosIncompatibles = "BE_606";
        public const string ParametrosVacios = "BE_607";
        public const string UsuarioContraseñaInvalidos = "BE_608";
        public const string Timeout = "BIE_001";
        public const string NoUserSettings = "BIE_002";
        public const string SolicitiudeUploadFail = "BIE_003";
        public const string ServicioSeguridadNoDisponible = "AE_500";

        public static Dictionary<string, string> BusinessErrorsMessages = new Dictionary<string, string>()
        {
            {SolicitiudeUploadFail, "No se pudo crear la solicitud, queda en pendiente de sincronización."}
        };

        public static bool IsResourceNotFound(string code)
        {
            return code.Equals(RecursoNoEncontrado);
        }
    }
}
