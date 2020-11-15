using SalesWorkforce.Common.Abstractions;

namespace SalesWorkforce.MobileApp.WebServices.DataContracts
{
    public class AuthTokenDataContract : IJsonDataContract
    {
        public string AccessToken { get; set; }
        public long ExpireAt { get; set; }
    }
}
