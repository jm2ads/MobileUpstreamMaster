using Frontend.Business.Commons;
using SQLite;
using System;

namespace Frontend.Business.Synchronizer
{
    public abstract class SyncEntity : PersistebleEntity
    {
        public SyncState SyncState { get; set; }
        //[Indexed]
        //public int RemoteId { get; set; } //TO REMOVE
        public DateTime Uploaded { get; set; }
        public DateTime Downloaded { get; set; }
    }
}
