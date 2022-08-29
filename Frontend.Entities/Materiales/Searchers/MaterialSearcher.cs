using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Frontend.Commons.Commons;

namespace Frontend.Business.Materiales.Searchers
{
    public class MaterialSearcher
    {
        private readonly IRepository<Material> repository;

        public MaterialSearcher(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public async Task<IList<Material>> GetAllWithChildren()
        {
            return await repository.GetAllWithChildren();
        }

        public async Task<IList<Material>> GetAllByIds(IList<int> idList)
        {
            return await repository.GetAllByIds(idList);
        }

        public async Task<IList<string>> GetAllCodigoAutocomplete()
        {
            var list = await repository.GetAll();
            return list.Select(x => x.Codigo.TrimStart('0')).Distinct().ToList();
        }

        public async Task<IList<string>> GetAllDescripcionAutocomplete()
        {
            var list = await repository.GetAll();
            return list.Select(x => x.Descripcion).Distinct().ToList();
        }

        public async Task<IList<Material>> GetAllByCodigo(string codigo)
        {
            return await repository.Where(x => x.Codigo.ToUpper().Contains(codigo.PadLeft(18,'0').ToUpper()));
        }

        public async Task<Material> GetByCodigo(string searchValue)
        {
            searchValue = searchValue.PadLeft(18, '0').ToUpper();
            return await repository.FindFirstWithChildren(x => x.Codigo.ToUpper() == searchValue);
        }

        public async Task<Material> GetByDescripcion(string searchValue)
        {
            return await repository.FindFirstWithChildren(x => x.Descripcion.ToUpper() == searchValue.ToUpper());
        }

        //public async Task<Material> GetByRemoteId(int remoteId)
        //{
        //    return await repository.FindFirstWithChildren(x => x.RemoteId == remoteId);
        //}

        public async Task<Material> GetById(int id)
        {
            return await repository.FindFirstWithChildren(x => x.Id == id);
        }
    }
}
