using Microsoft.AspNetCore.Http;
using SalesWorkforce.Common.Models;

namespace SalesWorkforce.FunctionApp.Providers.Abstractions
{
    public interface IAccessTokenProvider
    {
        AuthToken GenerateToken(string id);
        AccessTokenResult ValidateToken(HttpRequest request);
    }
}
