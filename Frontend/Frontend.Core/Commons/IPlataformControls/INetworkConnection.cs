

namespace Frontend.Core.Commons.IPlataformControls
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }

        void CheckConnection();
    }
}
