using SalesWorkforce.Common.DataContracts;

namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface ISalesAgentService
    {
        SalesAgentContract GetSalesAgent(long recordId);
    }
}