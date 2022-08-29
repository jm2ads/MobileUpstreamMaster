using Frontend.Business.Synchronizer;
using SQLite;

namespace Frontend.Business.ClasesDeValoracion
{
    public class ClaseDeValoracion : SyncEntity
    {
        public string Codigo { get; set; }
        public bool EsUsado { get; set; }

        public override bool Equals(object obj)
        {
            return this.Codigo == (obj as ClaseDeValoracion).Codigo;
        }
    }
}
