using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Settings.Core
{
    public class SettingUpdater
    {
        private readonly IRepository<Setting> repository;

        public SettingUpdater(IRepository<Setting> repository)
        {
            this.repository = repository;
        }

        public async Task Update(Setting setting)
        {
            await repository.UpdateWithChildren(setting);
        }
    }
}
