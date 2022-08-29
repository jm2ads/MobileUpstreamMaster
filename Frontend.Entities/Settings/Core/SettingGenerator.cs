using Frontend.Business.Commons;
using System.Threading.Tasks;

namespace Frontend.Business.Settings
{
    public class SettingGenerator
    {
        private readonly IRepository<Setting> repository;
        private readonly SettingFactory settingFactory;

        public SettingGenerator(IRepository<Setting> repository, SettingFactory settingFactory)
        {
            this.repository = repository;
            this.settingFactory = settingFactory;
        }

        public async Task<Setting> Generate(int usuarioId)
        {
            var newSetting = settingFactory.Create(usuarioId);
            var setting = await repository.First();
            if (setting == null)
            {
                await repository.SaveWithChildren(newSetting);
                setting = newSetting;
            }
            else
            {
                Merge(setting, newSetting);
                await repository.Update(setting);
            }

            return setting;
        }
        
        public async Task<Setting> Generate()
        {
            var newSetting = settingFactory.Create();
            await repository.Save(newSetting);
            return newSetting;
        }

        private void Merge(Setting oldSetting, Setting newSetting)
        {
            oldSetting.UsuarioActivoId = newSetting.UsuarioActivoId;
        }

        public async void Drop()
        {
            await repository.DropTableAsync();
        }
    }
}
