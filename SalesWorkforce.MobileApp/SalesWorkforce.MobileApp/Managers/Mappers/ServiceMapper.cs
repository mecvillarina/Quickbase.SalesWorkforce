using AutoMapper;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Mappers.Profiles;

namespace SalesWorkforce.MobileApp.Managers.Mappers
{
    public class ServiceMapper : IServiceMapper
    {
        private readonly Mapper _mapper;

        public ServiceMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityDataContractProfile>();
                cfg.AddProfile<EntityDataObjectProfile>();
                cfg.AddProfile<DataContractDataObjectProfile>();
            });

            _mapper = new Mapper(config);
        }

        public TDestination Map<TSource, TDestination>(TSource value) => _mapper.Map<TSource, TDestination>(value);

        public TDestination Map<TDestination>(object value) => _mapper.Map<TDestination>(value);
    }
}
