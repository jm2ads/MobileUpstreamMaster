using System;
using System.IO;
using Frontend.Data.Commons;
using Frontend.iOS.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSFileHelper))]
namespace Frontend.iOS.Implementations
{
    public class IOSFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, filename);
        }
    }
}