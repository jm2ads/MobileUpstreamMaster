//using System;
//using System.Threading.Tasks;
//using Frontend.Business.IRepositories;
//using Frontend.Business.Settings;
//using Frontend.Data.Commons;
//using Frontend.Data.Database;

//namespace Frontend.Data.DataAccess
//{
//    public class SettingRepository : Repository<Setting>, ISettingRepository
//    {
//        public SettingRepository(Database<Setting> database) : base(database)
//        {

//        }

//        public async Task<bool> ExistsToken()
//        {
//            var settings = await Get();
//            return !String.IsNullOrEmpty(settings?.Token);
//        }

//        public async Task<Setting> Get()
//        {
//            var setting = await database.First();
//            if (setting == null)
//            {
//                return new NullSettings();
//            }
//            return setting;
//        }

//        public async Task<string> GetUserLoginId()
//        {
//            var setting = await Get();
//            return setting.UserLogin;
//        }

//        public async Task<DateTime> GetLastSynchronization()
//        {
//            var setting = await Get();
//            return setting.LastSync;
//        }

//        public async Task<DateTime> UpdateLastSync()
//        {
//            var setting = await database.First();
//            setting.LastSync = DateTime.Now;
//            await Update(setting);

//            return DateTime.Now;
//        }
//    }
//}
