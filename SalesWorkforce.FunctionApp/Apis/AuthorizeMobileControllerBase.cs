using Microsoft.AspNetCore.Http;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.FunctionApp.Apis
{
    public class AuthorizeMobileControllerBase
    {
        //public readonly IServiceMapper Mapper;
        //public readonly IAppUserService AppUserService;
        public readonly IAccessTokenProvider AccessTokenProvider;

        public AuthorizeMobileControllerBase(IAccessTokenProvider accessTokenProvider)
        {
            AccessTokenProvider = accessTokenProvider;
        }

        public AccessTokenResult ValidateToken(HttpRequest req) => AccessTokenProvider.ValidateToken(req);

        //public AppUserServiceModel GetAppUser(AccessTokenResult tokenResult)
        //{
        //    if (tokenResult.Status == AccessTokenStatus.Valid)
        //    {
        //        var userId = long.Parse(tokenResult.Principal.Claims.First(x => x.Type == "id").Value);
        //        return AppUserService.Get(userId);
        //    }

        //    return null;
        //}
    }
}
