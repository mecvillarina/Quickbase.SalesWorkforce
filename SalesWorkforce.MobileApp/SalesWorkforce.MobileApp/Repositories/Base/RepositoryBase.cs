using SalesWorkforce.MobileApp.Repositories.Abstractions;

namespace SalesWorkforce.MobileApp.Repositories.Base
{
    public class RepositoryBase
    {
        protected IMobileDatabase DB { get; }

        public RepositoryBase(IMobileDatabase db)
        {
            DB = db;
        }
    }
}
