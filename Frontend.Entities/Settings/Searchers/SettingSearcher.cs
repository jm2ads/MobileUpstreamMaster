using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Settings.Searchers
{
    public class SettingSearcher
    {
        private readonly IRepository<Setting> repository;
        private readonly SettingGenerator settingGenerator;

        public SettingSearcher(IRepository<Setting> repository, SettingGenerator settingGenerator)
        {
            this.repository = repository;
            this.settingGenerator = settingGenerator;
        }

        public async Task<Setting> GetWithChildren()
        {
            var setting = await repository.FirstWithChildren();

            if (setting == null)
            {
                setting = await settingGenerator.Generate();
            }

            return setting;
        }

        public async Task<Setting> Get()
        {
            var setting = await repository.First();

            if (setting == null)
            {
                setting = await settingGenerator.Generate();
            }

            return setting;
        }
    }
}
