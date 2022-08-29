using Frontend.Business.Attributes;
using Frontend.Business.Synchronizer;
using SQLite;

namespace Frontend.Business.Centros
{
    [IgnoreDbReset]
    public class Centro : SyncEntity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [Ignore]
        public string DisplayDescription => string.Format("{0} - {1}", Codigo, Nombre);

    }
}
