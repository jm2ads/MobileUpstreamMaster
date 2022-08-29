using Frontend.Business.GruposDeArticulos;
using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Materiales
{
    public class Material : SyncEntity
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string UnidadDeMedidaBase { get; set; }
        public string UnidadDeMedidaAlternativa1 { get; set; }
        public string UnidadDeMedidaAlternativa2 { get; set; }
        public string UnidadDeMedidaAlternativa3 { get; set; }
        public string UnidadDeMedidaAlternativa4 { get; set; }

        [ForeignKey(typeof(GrupoDeArticulo))]
        public int IdGrupoArticulo { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public GrupoDeArticulo GrupoDeArticulo { get; set; }


        public override bool Equals(object obj)
        {
            return this.Codigo == (obj as Material).Codigo;
        }
    }
}
