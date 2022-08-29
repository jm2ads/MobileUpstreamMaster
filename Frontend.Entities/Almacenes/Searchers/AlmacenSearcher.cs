using Frontend.Business.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Almacenes.Searchers
{
    public class AlmacenSearcher
    {
        private readonly IRepository<Almacen> repository;

        public AlmacenSearcher(IRepository<Almacen> repository)
        {
            this.repository = repository;
        }

        public async Task<Almacen> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }

        public async Task<IList<Almacen>> GetAll()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<IList<Almacen>> GetByIdCentro(int IdCentro)
        {
            return await repository.Where(x=>x.IdCentro == IdCentro);
        }

        public async Task<Almacen> GetByNombre(string nombre)
        {
            return await repository.FindFirstWithChildren(x => x.Nombre == nombre);
        }

        public async Task<Almacen> GetByCodigo(string codigo)
        {
            return await repository.FindFirstWithChildren(x => x.Codigo == codigo);
        }

        public async Task<IList<string>> GetAllNombreAutocomplete()
        {
            var list = await repository.GetAll();
            return list.Select(x => x.Nombre).Distinct().ToList();
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            var list = await repository.GetAllWithChildren();
            return list.Select(x => x.Codigo).Distinct().ToList();
        }
    }
}
