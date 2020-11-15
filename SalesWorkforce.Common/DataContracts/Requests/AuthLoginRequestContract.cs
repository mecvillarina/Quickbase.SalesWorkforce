using SalesWorkforce.Common.Abstractions;

namespace SalesWorkforce.Common.DataContracts.Requests
{
    public class AuthLoginRequestContract : IJsonDataContract
    {
        public string AgentId { get; set; }
        public string BadgeCode { get; set; }
    }
}
