using Frontend.Commons.Commons;
using Frontend.Data.Commons;
using SQLite;
using Xamarin.Forms;

namespace Frontend.Data.DataAccess
{
    public class ConnectionFactory : IConnectionFactory
    {
        private SQLiteAsyncConnection connection;
        public SQLiteAsyncConnection GetConnection
        {
            get
            {
                if (connection == null)
                {
                    var fileHelper = DependencyService.Get<IFileHelper>();

                    var path = fileHelper.GetLocalFilePath(ApplicationConstants.DatabaseName);
                    connection = new SQLiteAsyncConnection(path,
                        SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache,
                        true,
                        key: ApplicationConstants.DatabaseKey);
                    connection.ExecuteAsync(string.Format("PRAGMA key = '{0}'", ApplicationConstants.DatabaseKey)).Wait();
                }
                return connection;
            }
        }
    }
}
