using Frontend.Business.Almacenes;
using Frontend.Business.Attributes;
using Frontend.Business.Commons;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.CambiosUbicacion
{
    [IgnoreDbReset]
    public class CambioUbicacionAlmacen : LocalEntity
    {
        [ForeignKey(typeof(CambioUbicacion))]
        public int CambioUbicacionId { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int AlmacenId { get; set; }
    }
}
