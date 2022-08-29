using Frontend.Business.Commons;
using System;

namespace Frontend.Business.Synchronizer
{
    public abstract class SyncLocalEntity : LocalEntity
    {
        public SyncState SyncState { get; set; }
        public DateTime Uploaded { get; set; }
    }
}
