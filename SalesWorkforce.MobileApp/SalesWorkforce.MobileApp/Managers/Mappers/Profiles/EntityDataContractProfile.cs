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
            CreateMap<CustomerCreateRequestEntity, CustomerCreateRequestContract>();
            CreateMap<PurchaseOrderCreateRequestEntity, PurchaseOrderCreateRequestContract>();
            CreateMap<PurchaseOrderProductCreateRequestEntity, PurchaseOrderProductCreateRequestContract>();

            CreateMap<AuthTokenDataContract, AuthTokenEntity>();
            CreateMap<SalesAgentContract, AppUserEntity>();
            CreateMap<CustomerContract, CustomerEntity>();
            CreateMap<ProductContract, ProductEntity>();
            CreateMap<PurchaseOrderContract, PurchaseOrderEntity>();
            CreateMap<PurchaseOrderProductContract, PurchaseOrderProductEntity>();

        }
    }
}
