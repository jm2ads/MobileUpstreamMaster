using Frontend.Business.Settings;
using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ISettingsService
    {
        Task<Setting> GetWithChildren();
        Task<Setting> Get();
        Task<Setting> Generate(int usuarioId);
        Task Update(Setting setting);
        void DropData();
        bool ValidateInitialSync(Setting setting);
        Task<Setting> SetPendingToSync(bool isPendingToSync);
        Task SetHasSyncWithError(bool v);
        Task ResetHasSyncWithError();
    }
}
