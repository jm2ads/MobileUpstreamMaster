
using System.Threading.Tasks;

namespace Frontend.Core.Commons.Alerts
{
    public interface IDisplayAlertService
    {
        void Show(string title, string message, string accept);
        Task<bool> Show(string title, string message, string accept, string cancel);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}
