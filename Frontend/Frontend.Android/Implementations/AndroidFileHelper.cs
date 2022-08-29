using System.IO;
using Frontend.Data.Commons;
using Frontend.Droid.Implementations;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(AndroidFileHelper))]
namespace Frontend.Droid.Implementations
{
    public class AndroidFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}