using System;
namespace Frontend.Core.Commons.IPlataformControls
{
    public interface IUrlEmailConverter
    {
        string GetUrl(string email, string appname, string version, string release, string enviroment);
    }
}
