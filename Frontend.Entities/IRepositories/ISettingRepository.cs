
using System;
using System.Threading.Tasks;
using Frontend.Business.Commons;
using Frontend.Business.Settings;

namespace Frontend.Business.IRepositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Task<bool> ExistsToken();

        Task<ISetting> Get();

        Task<string> GetUserLoginId();

        Task<DateTime> GetLastSynchronization();

        Task<DateTime> UpdateLastSync();
    }
}
