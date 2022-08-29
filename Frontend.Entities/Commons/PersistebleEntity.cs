
using SQLite;
using System;

namespace Frontend.Business.Commons
{
    public abstract class PersistebleEntity
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
    }
}
