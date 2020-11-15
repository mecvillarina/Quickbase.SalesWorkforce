using SalesWorkforce.MobileApp.Managers.Entities;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Managers.Abstractions
{
    public interface IAppUserManager
    {
        Task GetProfile();
        AppUserEntity GetProfileLocally();
    }
}