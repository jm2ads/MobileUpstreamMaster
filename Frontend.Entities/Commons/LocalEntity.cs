using SQLite;

namespace Frontend.Business.Commons
{
    public abstract class LocalEntity : PersistebleEntity
    {
        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }
    }
}
