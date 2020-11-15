using SalesWorkforce.MobileApp.Managers.Entities;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Managers.Abstractions
{
    public interface IAuthManager
    {
        void ClearAuthData();
        Task<bool> IsSessionValid();
        Task Login(AuthLoginRequestEntity reqEntity);
    }
}
