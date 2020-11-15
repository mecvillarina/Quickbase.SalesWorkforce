using AutoMapper;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Repositories.DataObjects;

namespace SalesWorkforce.MobileApp.Managers.Mappers.Profiles
{
    public class EntityDataObjectProfile : Profile
    {
        public EntityDataObjectProfile()
        {
            CreateMap<AppUserDataObject, AppUserEntity>();
            CreateMap<CustomerDataObject, CustomerEntity>();
            CreateMap<ProductDataObject, ProductEntity>();
        }
    }
}
