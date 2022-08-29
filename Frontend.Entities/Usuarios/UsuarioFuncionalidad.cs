using Frontend.Business.Attributes;
using Frontend.Business.Commons;
using Frontend.Business.Funcionalidades;
using SQLiteNetExtensions.Attributes;

namespace Frontend.Business.Usuarios
{
    [IgnoreDbReset]
    public class UsuarioFuncionalidad : LocalEntity
    {
        [ForeignKey(typeof(Usuario))]
        public int UsuarioId { get; set; }

        [ForeignKey(typeof(Funcionalidad))]
        public int FuncionalidadId { get; set; }
    }
}
