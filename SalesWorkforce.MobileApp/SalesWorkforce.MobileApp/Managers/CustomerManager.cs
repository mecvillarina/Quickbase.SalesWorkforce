using SalesWorkforce.Common.DataContracts.Requests;
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
    public class CustomerManager : AuthenticatedManagerBase, ICustomerManager
    {
        private readonly ICustomerWebService _customerWebService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager authManager,
            ICustomerWebService customerWebService,
            ICustomerRepository customerRepository) : base(connectivity, mapper, authManager)
        {
            _customerWebService = customerWebService;
            _customerRepository = customerRepository;
        }

        public async Task GetCustomers()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _customerWebService.Search("", accessToken);
                var dataObject = Mapper.Map<List<CustomerDataObject>>(contract.ToList());
                _customerRepository.Clear();
                _customerRepository.AddRange(dataObject);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public List<CustomerEntity> GetCustomersLocally()
        {
            var dataObject = _customerRepository.ToList();
            return Mapper.Map<List<CustomerEntity>>(dataObject);
        }

        public async Task<CustomerEntity> GetCustomer(long recordId)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _customerWebService.Get(recordId, accessToken);
                return Mapper.Map<CustomerEntity>(contract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public async Task<CustomerEntity> Create(CustomerCreateRequestEntity reqEntity)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();
            try
            {
                var reqContract = Mapper.Map<CustomerCreateRequestContract>(reqEntity);
                var accessToken = await GetAccessToken();
                var retContract = await _customerWebService.Create(reqContract, accessToken);
                return Mapper.Map<CustomerEntity>(retContract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }
    }
}
