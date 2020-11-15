using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SQLite;

namespace SalesWorkforce.MobileApp.Repositories.Base
{
    public abstract class DataObjectBase : IDataObjectBase
    {
        [PrimaryKey, AutoIncrement]
        public virtual long RowId { get; set; }
    }
}
