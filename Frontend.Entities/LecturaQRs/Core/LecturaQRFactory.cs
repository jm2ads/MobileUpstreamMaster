using Frontend.Commons.Enums;

namespace Frontend.Business.LecturaQRs.Core
{
    public class LecturaQRFactory
    {
        public LecturaQR Create(string lecturaQR)
        {
            var lectura = lecturaQR.Split(';');
            #region ASOSA ACOTAR Almacen a 18 caracteres
            LecturaQR _LecturaQR = new LecturaQR();
            _LecturaQR.Almacen = lectura.Length > PosicionesLecturaQR.Almacen.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Almacen.GetHashCode()) as string : string.Empty;
            _LecturaQR.Lote = lectura.Length > PosicionesLecturaQR.Lote.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Lote.GetHashCode()) as string : string.Empty;
            _LecturaQR.CodigoMaterial = lectura.Length > PosicionesLecturaQR.CodigoMaterial.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.CodigoMaterial.GetHashCode()) as string : string.Empty;
            //if (_LecturaQR.CodigoMaterial.Length > 18)
            //{
            //    _LecturaQR.CodigoMaterial = _LecturaQR.CodigoMaterial.Substring(0, 18);
            //}
            if (_LecturaQR.CodigoMaterial.IndexOf(";") >= 0)
            {
                _LecturaQR.CodigoMaterial = _LecturaQR.CodigoMaterial.Substring(0, _LecturaQR.CodigoMaterial.IndexOf(";")) ;
            }
           

            _LecturaQR.NumeroMovimiento = lectura.Length > PosicionesLecturaQR.NumeroMovimiento.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.NumeroMovimiento.GetHashCode()) as string : string.Empty;
            _LecturaQR.PEP = lectura.Length > PosicionesLecturaQR.PEP.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.PEP.GetHashCode()) as string : string.Empty;
            _LecturaQR.Ubicacion = lectura.Length > PosicionesLecturaQR.Ubicacion.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Ubicacion.GetHashCode()) as string : string.Empty;

            return _LecturaQR;
            #endregion
            //return new LecturaQR()
            //{
            //    Almacen = lectura.Length > PosicionesLecturaQR.Almacen.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Almacen.GetHashCode()) as string : string.Empty,
            //    Lote = lectura.Length > PosicionesLecturaQR.Lote.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Lote.GetHashCode()) as string : string.Empty,
            //    CodigoMaterial = lectura.Length > PosicionesLecturaQR.CodigoMaterial.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.CodigoMaterial.GetHashCode()) as string : string.Empty,
            //    NumeroMovimiento = lectura.Length > PosicionesLecturaQR.NumeroMovimiento.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.NumeroMovimiento.GetHashCode()) as string : string.Empty,
            //    PEP = lectura.Length > PosicionesLecturaQR.PEP.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.PEP.GetHashCode()) as string : string.Empty,
            //    Ubicacion = lectura.Length > PosicionesLecturaQR.Ubicacion.GetHashCode() ? lectura.GetValue(PosicionesLecturaQR.Ubicacion.GetHashCode()) as string : string.Empty
            //};
        }
    }
 
}
