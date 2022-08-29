using Frontend.Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Business.Materiales.Core
{
    public class MaterialDeleter
    {
        private readonly IRepository<Material> repository;

        public MaterialDeleter(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAll()
        {
            await repository.DeleteAll();
        }
    }
}
