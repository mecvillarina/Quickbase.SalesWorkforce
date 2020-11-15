using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SalesWorkforce.MobileApp.Repositories.DataObjects;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class ProductManager : AuthenticatedManagerBase, IProductManager
    {
        private readonly IProductWebService _productWebService;
        private readonly IProductRepository _productRepository;

        public ProductManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager authManager,
            IProductWebService productWebService,
            IProductRepository productRepository) : base(connectivity, mapper, authManager)
        {
            _productWebService = productWebService;
            _productRepository = productRepository;
        }

        public async Task GetProducts()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _productWebService.Search("", accessToken);
                var dataObject = Mapper.Map<List<ProductDataObject>>(contract.ToList());
                _productRepository.Clear();
                _productRepository.AddRange(dataObject);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public List<ProductEntity> GetCustomersLocally()
        {
            var dataObject = _productRepository.ToList();
            return Mapper.Map<List<ProductEntity>>(dataObject);
        }

        public async Task<ProductEntity> GetProduct(long recordId)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _productWebService.Get(recordId, accessToken);
                return Mapper.Map<ProductEntity>(contract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }
    }
}
