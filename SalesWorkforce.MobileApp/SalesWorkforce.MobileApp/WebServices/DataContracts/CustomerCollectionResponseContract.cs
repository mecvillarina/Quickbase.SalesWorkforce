using SalesWorkforce.Common.Abstractions;
using SalesWorkforce.Common.DataContracts;
using System.Collections.Generic;

namespace SalesWorkforce.MobileApp.WebServices.DataContracts
{
    public class CustomerCollectionResponseContract : List<CustomerContract>, IJsonDataContract
    {
    }
}
