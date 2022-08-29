using Frontend.Business.Settings;
using Frontend.Business.Settings.Core;
using Frontend.Business.Settings.Searchers;
using Frontend.Business.Settings.Validators;
using Frontend.IServices.IServices;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class SettingService : ISettingsService
    {
        private readonly SettingSearcher settingSearcher;
        private readonly SettingGenerator settingGenerator;
        private readonly SettingUpdater settingUpdater;
        private readonly SettingValidator settingValidator;

        public SettingService(SettingSearcher settingSearcher, SettingGenerator settingGenerator, SettingUpdater settingUpdater,
            SettingValidator settingValidator)
        {
            this.settingSearcher = settingSearcher;
            this.settingGenerator = settingGenerator;
            this.settingUpdater = settingUpdater;
            this.settingValidator = settingValidator;
        }

        public async Task<Setting> GetWithChildren()
        {
            return await settingSearcher.GetWithChildren();
        }

        public async Task<Setting> Get()
        {
            return await settingSearcher.Get();
        }

        public async Task<Setting> Generate(int usuarioId)
        {
            return await settingGenerator.Generate(usuarioId);
        }

        public async Task Update(Setting setting)
        {
            await settingUpdater.Update(setting);
        }

        public void DropData()
        {
            settingGenerator.Drop();
        }

        public bool ValidateInitialSync(Setting setting)
        {
            return settingValidator.ValidateInitialSync(setting);
        }

        public async Task<Setting> SetPendingToSync(bool isPendingToSync)
        {
            var settings = await this.GetWithChildren();
            settings.IsPendingToSync = isPendingToSync;
            await this.Update(settings);
            return settings;
        }

        public async Task SetHasSyncWithError(bool hasSyncWithError)
        {
            if (hasSyncWithError)
            {
                var setting = await GetWithChildren();
                setting.HasSyncWithError = hasSyncWithError;
                await Update(setting);
            }
        }

        public async Task ResetHasSyncWithError()
        {
            var setting = await GetWithChildren();
            setting.HasSyncWithError = false;
            await Update(setting);
        }
    }
}