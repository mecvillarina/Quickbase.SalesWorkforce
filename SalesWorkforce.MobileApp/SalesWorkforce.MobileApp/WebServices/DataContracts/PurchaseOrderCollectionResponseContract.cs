using SalesWorkforce.Common.Abstractions;
using SalesWorkforce.Common.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.MobileApp.WebServices.DataContracts
{
    public class PurchaseOrderCollectionResponseContract : List<PurchaseOrderContract>, IJsonDataContract
    {
    }
}
