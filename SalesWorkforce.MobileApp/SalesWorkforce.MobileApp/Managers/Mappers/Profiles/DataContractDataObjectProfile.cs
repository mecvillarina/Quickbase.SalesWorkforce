using AutoMapper;
using SalesWorkforce.Common.DataContracts;
using SalesWorkforce.MobileApp.Repositories.DataObjects;

namespace SalesWorkforce.MobileApp.Managers.Mappers.Profiles
{
    public class DataContractDataObjectProfile : Profile
    {
        public DataContractDataObjectProfile()
        {
            CreateMap<SalesAgentContract, AppUserDataObject>();
            CreateMap<CustomerContract, CustomerDataObject>();
            CreateMap<ProductContract, ProductDataObject>();
        }
    }
}
