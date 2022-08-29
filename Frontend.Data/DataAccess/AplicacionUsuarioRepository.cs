using Frontend.Business.IRepositories;
using Frontend.Business.Users.Devices;
using Frontend.Data.Commons;
using Frontend.Data.Database;

namespace Frontend.Data.DataAccess
{
    public class AplicacionUsuarioRepository : Repository<AplicacionUsuario>, IAplicacionUsuarioRepository
    {
        public AplicacionUsuarioRepository(Database<AplicacionUsuario> database) : base(database)
        {

        }
    }
}
