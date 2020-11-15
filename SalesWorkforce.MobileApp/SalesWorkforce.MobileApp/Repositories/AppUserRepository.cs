using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.Base;
using SalesWorkforce.MobileApp.Repositories.DataObjects;

namespace SalesWorkforce.MobileApp.Repositories
{
    public class AppUserRepository : Repository<AppUserDataObject>, IAppUserRepository
    {
        public AppUserRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
