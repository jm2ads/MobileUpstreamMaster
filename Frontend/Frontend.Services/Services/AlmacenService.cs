using Frontend.Business.Almacenes;
using Frontend.Business.Almacenes.Searchers;
using Frontend.IServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class AlmacenService: IAlmacenService
    {
        private readonly AlmacenSearcher almacenSearcher;

        public AlmacenService(AlmacenSearcher almacenSearcher)
        {
            this.almacenSearcher = almacenSearcher;
        }

        public async Task<IList<Almacen>> GetAll()
        {
            return await almacenSearcher.GetAll();
        }

        public async Task<IList<Almacen>> GetByIdCentro(int IdCentro)
        {
            return await almacenSearcher.GetByIdCentro(IdCentro);
        }
        

        public async Task<Almacen> GetByNombre(string nombre)
        {
            return await almacenSearcher.GetByNombre(nombre);
        }

        public async Task<Almacen> GetByCodigo(string codigo)
        {
            return await almacenSearcher.GetByCodigo(codigo);
        }

        public async Task<IList<string>> GetAllNombreAutocomplete()
        {
            return await almacenSearcher.GetAllNombreAutocomplete();
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            return await almacenSearcher.GetAllCodigoAutocomplete();
        }
    }
}
