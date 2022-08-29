using Frontend.Business.DetallesInventario;
using Frontend.Business.DetallesInventario.Validators;
using Frontend.Commons.Commons.Errors;
using System.Linq;

namespace Frontend.Business.Inventarios.Validations
{
    public class InventarioValidator
    {
        private readonly DetalleInventarioValidator detalleInventarioValidator;

        public InventarioValidator(DetalleInventarioValidator detalleInventarioValidator)
        {
            this.detalleInventarioValidator = detalleInventarioValidator;
        }

        public bool IsDuplicatedDetalleInventario(Inventario inventario, DetalleInventario detalleInventario)
        {
            return inventario.DetallesInventario.Any(x => detalleInventarioValidator.IsEqual(x, detalleInventario));
        }

        public bool IsValidToFinish(Inventario inventario)
        {
            return inventario.FechaCreacion != null
                && inventario.FechaRecuento != null;
        }

        private bool IsValidToPendienteAprobacion(Inventario inventario)
        {
            return inventario.FechaCreacion != null
                && inventario.FechaRecuento != null;
        }

        private bool IsValidToPendienteAprobacionSap(Inventario inventario)
        {
            var detallesInventario = inventario.DetallesInventario.Where(x => x.EsContado);
            return detallesInventario.Count() > 0 && !detallesInventario.Any(x => x.CantidadContada < 0);
        }

        private bool IsValidToAprobado(Inventario inventario)
        {
            return inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacion 
                || inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacionSap
                || inventario.Estado == Frontend.Commons.Enums.EstadoInventario.RechazadoSAP;
        }

        private bool IsValidToRechazado(Inventario inventario)
        {
            return inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacion 
                ||  inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacionSap
                || inventario.Estado == Frontend.Commons.Enums.EstadoInventario.RechazadoSAP;
        }

        private bool IsValidToAprobadoSap(Inventario inventario)
        {
            return inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacionSap;
        }

        private bool IsValidToRechazadoSap(Inventario inventario)
        {
            return inventario.Estado == Frontend.Commons.Enums.EstadoInventario.PendienteAprobacionSap;
        }

        public void ValidPendienteAprobacion(Inventario inventario)
        {
            var result = IsValidToPendienteAprobacion(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "No es válido para pasar a Pendiente de aprobación.");
            }
        }

        public void ValidPendienteAprobacionSap(Inventario inventario)
        {
            var result = IsValidToPendienteAprobacionSap(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "Por favor, seleccione al menos un material con cantidad mayor o igual a 0.");
            }
        }

        public void ValidAprobado(Inventario inventario)
        {
            var result = IsValidToAprobado(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "No es válido para pasar a Aprobado.");
            }
        }

        public void ValidRechazado(Inventario inventario)
        {
            var result = IsValidToRechazado(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "No es válido para pasar a Rechazado.");
            }
        }

        public void ValidAprobadoSap(Inventario inventario)
        {
            var result = IsValidToAprobadoSap(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "No es válido para pasar a Aprobado.");
            }
        }

        public void ValidRechazadoSap(Inventario inventario)
        {
            var result = IsValidToRechazadoSap(inventario);
            if (!result)
            {
                throw new BusinessException(BusinessErrorCode.ReglasNegocioNoCumplidas, "No es válido para pasar a Rechazado.");
            }
        }
    }
}
