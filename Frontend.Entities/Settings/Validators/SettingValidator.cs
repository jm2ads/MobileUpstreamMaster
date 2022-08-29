using Frontend.Commons.Commons;

namespace Frontend.Business.Settings.Validators
{
    public class SettingValidator
    {
        public bool ValidateInitialSync(Setting setting)
        {
            return setting.LastSync == ApplicationConstants.DefaultDateSync;
        }
    }
}
