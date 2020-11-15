using AutoMapper;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.WebServices.DataContracts;

namespace SalesWorkforce.MobileApp.Managers.Mappers.Profiles
{
    public class EntityDataContractProfile : Profile
    {
        public EntityDataContractProfile()
        {
            CreateMap<AuthLoginRequestEntity, AuthLoginRequestContract>();

            CreateMap<AuthTokenDataContract, AuthTokenEntity>();
            CreateMap<SalesAgentContract, AppUserEntity>();
        }
    }
}
