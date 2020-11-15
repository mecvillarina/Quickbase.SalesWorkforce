using SalesWorkforce.Common.DataContracts.Requests;
using SalesWorkforce.MobileApp.Common.Exceptions;
using SalesWorkforce.MobileApp.Managers.Abstractions;
using SalesWorkforce.MobileApp.Managers.Base;
using SalesWorkforce.MobileApp.Managers.Entities;
using SalesWorkforce.MobileApp.WebServices.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace SalesWorkforce.MobileApp.Managers
{
    public class PurchaseOrderManager : AuthenticatedManagerBase, IPurchaseOrderManager
    {
        private readonly IPurchaseOrderWebService _purchaseOrderWebService;

        public PurchaseOrderManager(IConnectivity connectivity,
            IServiceMapper mapper,
            IInternalAuthManager authManager,
            IPurchaseOrderWebService purchaseOrderWebService) : base(connectivity, mapper, authManager)
        {
            _purchaseOrderWebService = purchaseOrderWebService;
        }

        public async Task<List<PurchaseOrderEntity>> GetPurchaseOrders()
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _purchaseOrderWebService.GetAll(accessToken);
                return Mapper.Map<List<PurchaseOrderEntity>>(contract.ToList());
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public async Task<PurchaseOrderEntity> GetPurchaseOrder(long recordId)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();

            try
            {
                var accessToken = await GetAccessToken();
                var contract = await _purchaseOrderWebService.Get(recordId, accessToken);
                return Mapper.Map<PurchaseOrderEntity>(contract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public async Task<PurchaseOrderEntity> Create(PurchaseOrderCreateRequestEntity reqEntity)
        {
            EnsureInternetAvailable();
            await EnsureSessionIsValid();
            try
            {
                var reqContract = Mapper.Map<PurchaseOrderCreateRequestContract>(reqEntity);
                var accessToken = await GetAccessToken();
                var retContract = await _purchaseOrderWebService.Create(reqContract, accessToken);
                return Mapper.Map<PurchaseOrderEntity>(retContract);
            }
            catch (ApiException ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }
    }
}
