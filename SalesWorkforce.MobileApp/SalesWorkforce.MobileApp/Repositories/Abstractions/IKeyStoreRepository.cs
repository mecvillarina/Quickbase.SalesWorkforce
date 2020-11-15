using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.Repositories.Abstractions
{
    public interface IKeyStoreRepository
    {
        Task<string> GetStringAsync(string key);

        void RemoveString(string key);

        Task SetStringAsync(string key, string value);
    }
}
