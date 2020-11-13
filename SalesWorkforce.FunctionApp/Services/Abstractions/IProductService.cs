using SalesWorkforce.Common.DataContracts;
using System.Collections.Generic;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface IProductService
    {
        List<ProductContract> GetProducts(string query);
        ProductContract GetProduct(long recordId);
    }
}