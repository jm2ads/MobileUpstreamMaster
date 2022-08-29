
namespace Frontend.Business.Synchronizer
{
    public enum SyncState
    {
        New,
        Synchronized,
        Updated, // se modifico en el dispositivo
        PendingToSync
    }
}
