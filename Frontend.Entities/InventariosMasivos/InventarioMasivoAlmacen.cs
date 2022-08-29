using Frontend.Business.Almacenes;
using Frontend.Business.Commons;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.InventariosMasivos
{
    public class InventarioMasivoAlmacen : LocalEntity
    {
        [ForeignKey(typeof(InventarioMasivo))]
        public int InventarioMasivoId { get; set; }
        [ForeignKey(typeof(Almacen))]
        public int AlmacenId { get; set; }
    }
}
